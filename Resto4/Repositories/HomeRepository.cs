

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Resto4.Repositories
{
    public class HomeRepository:IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        

        public async Task<IEnumerable<Category>> Categorys()
        {
            return await _db.Ctegories.ToListAsync();

        }
        public async Task<IEnumerable<Plat>> GetPlats(string sterm = "", int categoryId = 0)
        {
            sterm = sterm.ToLower();
            IEnumerable<Plat> plats = await (from plat in _db.Plats
                                             join category in _db.Ctegories
                                             on plat.CategoryId equals category.CategoryId
                                             where string.IsNullOrWhiteSpace(sterm) || (plat != null && plat.PlatName.ToLower().StartsWith(sterm))
                                             select new Plat
                                             {
                                                 PlatId = plat.PlatId,
                                                 Image = plat.Image,
                                                 PlatName = plat.PlatName,
                                                 CategoryId = plat.CategoryId,
                                                 Price = plat.Price,
                                                 CategoryName = category.CategoryName,
                                                 chefName = plat.chefName
                                             }
                            ).ToListAsync();

            if (categoryId > 0)
            {
                plats = plats.Where(a => a.CategoryId == categoryId).ToList();
            }

            return plats;
        }

    }
}