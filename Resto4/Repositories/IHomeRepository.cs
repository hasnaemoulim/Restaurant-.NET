namespace Resto4
{
    public interface IHomeRepository

    {
        Task<IEnumerable<Plat>> GetPlats(string sterm = "", int categoryId = 0);
         Task<IEnumerable<Category>> Categorys();

    }
}