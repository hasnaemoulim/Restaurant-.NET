using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Resto4.Models
{
    [Table("Cart")]
    public class Cart
    {
        public int CartId { get; set; }
        [Required]
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<CartDetail>? CartDetails { get; set; }
    }
}