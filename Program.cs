using System.Security.Claims;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authentication.Cookies;
using StudentManagementSystem.Data;
using StudentManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

var firebaseFilePath = builder.Configuration["Firebase:CredentialFilePath"];
var projectId = builder.Configuration["Firebase:ProjectId"];

if (string.IsNullOrWhiteSpace(firebaseFilePath))
    throw new InvalidOperationException("Firebase:CredentialFilePath is missing from appsettings.json.");

if (string.IsNullOrWhiteSpace(projectId))
    throw new InvalidOperationException("Firebase:ProjectId is missing from appsettings.json.");

var fullPath = Path.Combine(builder.Environment.ContentRootPath, firebaseFilePath);
if (!File.Exists(fullPath))
    throw new FileNotFoundException("Firebase service account file was not found.", fullPath);

var credential = GoogleCredential.FromFile(fullPath);

FirebaseApp.Create(new AppOptions
{
    Credential = credential,
    ProjectId = projectId
});

// Firebase Firestore
builder.Services.AddSingleton(_ => new FirestoreDbBuilder
{
    ProjectId = projectId,
    Credential = credential
}.Build());

builder.Services.AddSingleton<FirestoreService>();
builder.Services.AddHttpClient<FirebaseAuthService>();

// Cookie authentication replaces ASP.NET Identity + SQL Server.
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Account/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Create/update the Admin account in Firebase Auth + Firestore.
using (var scope = app.Services.CreateScope())
{
    var firestoreService = scope.ServiceProvider.GetRequiredService<FirestoreService>();
    await AdminSeeder.SeedAdmin(FirebaseApp.DefaultInstance, firestoreService);
}

app.Run();
