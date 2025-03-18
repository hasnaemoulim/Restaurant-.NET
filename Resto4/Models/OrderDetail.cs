
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resto4.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int PlatId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public Order? Order { get; set; }
        public Plat? Plat { get; set; }
    }
}