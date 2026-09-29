using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class StudentDetail
    {
        // Firestore document ID / Firebase Authentication UID.
        public string Id { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string FatherName { get; set; } = string.Empty;

        [Required]
        public string Contact { get; set; } = string.Empty;

        [Range(1, 100)]
        public int Age { get; set; }
    }
}
