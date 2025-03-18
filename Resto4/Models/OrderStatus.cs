using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Resto4.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required, MaxLength(20)]
        public string? StatusName { get; set; }

    }
}
