using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PieShop.Models
{
    public class PieRepository: IPieRepository
    {

        private readonly AppDbContext _appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            // Because we registered the AppDbContext in startup.cs it is managed in our DI container so we now have access to it here through construction injection
            // register services
            // services.AddDbContext<AppDbContext>(options =>
            // {  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); });
            _appDbContext = appDbContext; // Add to our backingfield
        }

        public IEnumerable<Pie> AllPies
        {
            get { return _appDbContext.Pies.Include(c => c.Category); }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get { return _appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek); }
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}