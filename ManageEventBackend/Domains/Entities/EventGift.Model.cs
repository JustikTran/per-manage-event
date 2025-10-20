using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManageEventBackend.Domains.Entities
{
    public class EventGift
    {
        public EventGift()
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
        [Column("information", TypeName = "text")]
        public string Information { get; set; } = default!;
    }
}
