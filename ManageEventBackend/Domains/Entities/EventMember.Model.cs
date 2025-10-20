using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEventBackend.Domains.Entities
{
    public class EventMember
    {
        public EventMember()
        {
            Id = Guid.NewGuid();
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
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; } = default!;

        [Column("nickname", TypeName = "varchar(50)")]
        public string Nickname { get; set; } = default!;

        [Column("email", TypeName = "varchar(100)")]
        public string Email { get; set; } = default!;

        [Column("phone", TypeName = "varchar(20)")]
        public string Phone { get; set; } = default!;
    }
}
