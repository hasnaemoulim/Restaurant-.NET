
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resto4.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int CartDetailId { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int PlatId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public Plat? Plat { get; set; }
        public Cart? Cart { get; set; }
    }
}