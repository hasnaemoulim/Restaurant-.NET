namespace Resto4.Models.DTOs
{
    public class PlatDisplayModel
    {
        public IEnumerable<Plat> Plats { get; set; }
        public IEnumerable<Category> Categories{ get; set;}
        public String STerm { get; set; } = "";
        public int CategoryId { get; set; } = 0;
    }
}
