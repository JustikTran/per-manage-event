using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEventBackend.Domains.Entities
{
    public class EventProcess
    {
        public EventProcess()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        [Key]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; set; }

        [Required]
        [Column("event_id", TypeName = "uuid")]
        public Guid EventId { get; set; }
        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; } = default!;

        [Required]
        [Column("content", TypeName = "text")]
        public string Content { get; set; } = default!;

        [Required]
        [Column("start_time", TypeName = "timestamptz")]
        public DateTime StartTime { get; set; }

        [Required]
        [Column("status", TypeName = "varchar(20)")]
        public string Status{ get; set; } = default!;

        [Required]
        [Column("extended_time", TypeName = "integer")]
        public int ExtendedTime { get; set; }

        [Required]
        [Column("created_at", TypeName = "timestamptz")]
        public DateTime CreatedAt { get; set; }
        [Required]
        [Column("updated_at", TypeName = "timestamptz")]
        public DateTime UpdatedAt { get; set; }
    }
}
