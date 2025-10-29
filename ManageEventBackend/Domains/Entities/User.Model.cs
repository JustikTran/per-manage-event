using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEventBackend.Domains.Entities
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            IsDelete = false;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        [Key]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }

        [Required]
        [Column("username", TypeName = "varchar(50)")]
        public string Username { get; set; } = default!;

        [Required]
        [Column("password", TypeName = "varchar(80)")]
        public string Password { get; set; } = default!;

        [Required]
        [Column("email", TypeName = "varchar(100)")]
        public string Email { get; set; } = default!;

        [Required]
        [Column("first_name", TypeName = "varchar(50)")]
        public string FirstName { get; set; } = default!;

        [Required]
        [Column("last_name", TypeName = "varchar(50)")]
        public string LastName { get; set; } = default!;

        [Required]
        [Column("role", TypeName = "varchar(20)")]
        public string Role { get; set; } = default!;

        [Required]
        [Column("avatar", TypeName = "varchar(255)")]
        public string Avatar { get; set; } = default!;

        [Column("refresh_token", TypeName = "varchar(255)")]
        public string RefreshToken { get; set; } = default!;

        public bool IsDelete { get; set; }

        [Required]
        [Column("created_at", TypeName = "timestamptz")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column("updated_at", TypeName = "timestamptz")]
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Event> Events { get; set; } = default!;
    }
}
