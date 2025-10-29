using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEventBackend.Domains.Entities
{
    public class Event
    {
        public Event()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        [Key]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }

        [Required]
        [Column("author_id", TypeName = "uuid")]
        public Guid AuthorId { get; set; } = default!;

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; } = default!;

        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; } = default!;

        [Column("description", TypeName = "text")]
        public string? Description { get; set; }

        [Required]
        [Column("status", TypeName = "varchar(20)")]
        public string Status { get; set; } = default!;

        [Required]
        [Column("start_date", TypeName = "timestamptz")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("location", TypeName = "varchar(255)")]
        public string Location { get; set; } = default!;

        [Required]
        [Column("end_date", TypeName = "timestamptz")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column("updated_at", TypeName = "timestamptz")]
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<EventGift> EventGifts { get; set; } = default!;
        public virtual ICollection<EventMember> EventMembers { get; set; } = default!;
        public virtual ICollection<EventProcess> EventProcesses { get; set; } = default!;
    }
}
