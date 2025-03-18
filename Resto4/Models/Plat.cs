using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resto4.Models
{
    [Table("Plat")]
    public class Plat
    {
        public int PlatId { get; set; }

        [Required]
        [MaxLength(40)]
        public string? PlatName { get; set; }
        [Required]
        [MaxLength(40)]
        public string? chefName { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<OrderDetail>? OrderDetail { get; set; }
        public List<CartDetail>? CartDetail { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }
    }
}