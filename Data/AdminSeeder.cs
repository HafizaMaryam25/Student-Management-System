using FirebaseAdmin;
using FirebaseAdmin.Auth;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Data
{
    public static class AdminSeeder
    {
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminPassword = "Admin@123";

        public static async Task SeedAdmin(FirebaseApp app, FirestoreService firestoreService)
        {
            var auth = FirebaseAuth.GetAuth(app);
            UserRecord? adminUser = null;

            try
            {
                adminUser = await auth.GetUserByEmailAsync(AdminEmail);
            }
            catch (FirebaseAuthException ex) when (ex.AuthErrorCode == AuthErrorCode.UserNotFound)
            {
                // User does not exist; it will be created below.
            }

            if (adminUser == null)
            {
                adminUser = await auth.CreateUserAsync(new UserRecordArgs
                {
                    Email = AdminEmail,
                    Password = AdminPassword,
                    EmailVerified = true,
                    Disabled = false
                });
            }

            await firestoreService.SetUserAsync(adminUser.Uid, AdminEmail, "Admin");
        }
    }
}
