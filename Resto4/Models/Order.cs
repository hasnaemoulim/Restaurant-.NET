
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resto4.Models
{
    [Table("Order")]
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        public string? UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        [Required]
        public int OrderStatusId { get; set; }
        public bool? IsDeleted { get; set; } = false;

        [NotMapped] // This property is not mapped to the database
        public string? UserName { get; set; }

        public OrderStatus? OrderStatus { get; set; }
        public List<OrderDetail>? OrderDetail { get; set; }
    }
}