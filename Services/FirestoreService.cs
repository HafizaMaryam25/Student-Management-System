using Google.Cloud.Firestore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Services
{
    public class FirestoreService
    {
        private readonly FirestoreDb _db;
        private readonly CollectionReference _students;
        private readonly CollectionReference _users;

        public FirestoreService(FirestoreDb db)
        {
            _db = db;
            _students = _db.Collection("students");
            _users = _db.Collection("users");
        }

        public async Task<List<StudentDetail>> GetStudentsAsync()
        {
            var snapshot = await _students.GetSnapshotAsync();
            return snapshot.Documents
                .Where(d => d.Exists)
                .Select(MapStudent)
                .ToList();
        }

        public async Task<StudentDetail?> GetStudentByIdAsync(string id)
        {
            var snapshot = await _students.Document(id).GetSnapshotAsync();
            return snapshot.Exists ? MapStudent(snapshot) : null;
        }

        public async Task<StudentDetail?> GetStudentByUserIdAsync(string userId)
        {
            return await GetStudentByIdAsync(userId);
        }

        public async Task CreateStudentAsync(string userId, StudentDetail student)
        {
            var data = ToDictionary(student, userId);
            await _students.Document(userId).SetAsync(data);
        }

        public async Task UpdateStudentAsync(string userId, StudentDetail student)
        {
            var data = ToDictionary(student, userId);
            await _students.Document(userId).SetAsync(data, SetOptions.MergeAll);
        }

        public async Task DeleteStudentAsync(string userId)
        {
            await _students.Document(userId).DeleteAsync();
        }

        public async Task SetUserAsync(string uid, string email, string role)
        {
            await _users.Document(uid).SetAsync(new Dictionary<string, object>
            {
                ["Email"] = email,
                ["Role"] = role,
                ["CreatedAt"] = Timestamp.GetCurrentTimestamp()
            }, SetOptions.MergeAll);
        }

        public async Task<string?> GetUserRoleAsync(string uid)
        {
            var snapshot = await _users.Document(uid).GetSnapshotAsync();
            if (!snapshot.Exists || !snapshot.ContainsField("Role"))
                return null;

            return snapshot.GetValue<string>("Role");
        }

        private static Dictionary<string, object> ToDictionary(StudentDetail student, string userId)
        {
            return new Dictionary<string, object>
            {
                ["UserId"] = userId,
                ["FullName"] = student.FullName ?? string.Empty,
                ["Email"] = student.Email ?? string.Empty,
                ["Address"] = student.Address ?? string.Empty,
                ["FatherName"] = student.FatherName ?? string.Empty,
                ["Contact"] = student.Contact ?? string.Empty,
                ["Age"] = student.Age
            };
        }

        private static StudentDetail MapStudent(DocumentSnapshot document)
        {
            return new StudentDetail
            {
                Id = document.Id,
                UserId = document.ContainsField("UserId") ? document.GetValue<string>("UserId") : document.Id,
                FullName = document.ContainsField("FullName") ? document.GetValue<string>("FullName") : string.Empty,
                Email = document.ContainsField("Email") ? document.GetValue<string>("Email") : string.Empty,
                Address = document.ContainsField("Address") ? document.GetValue<string>("Address") : string.Empty,
                FatherName = document.ContainsField("FatherName") ? document.GetValue<string>("FatherName") : string.Empty,
                Contact = document.ContainsField("Contact") ? document.GetValue<string>("Contact") : string.Empty,
                Age = document.ContainsField("Age") ? document.GetValue<int>("Age") : 0
            };
        }

        // Kept for your existing Firebase test route.
        public async Task AddStudentAsync(string documentId, Dictionary<string, object> studentData)
        {
            await _students.Document(documentId).SetAsync(studentData, SetOptions.MergeAll);
        }
    }
}
