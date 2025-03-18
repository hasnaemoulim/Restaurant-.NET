using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace Resto4.Models
{
    [Table("Category")]
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(40)]
        public string? CategoryName { get; set; }
        public List<Plat>? Plats { get; set; }
        

    }
}