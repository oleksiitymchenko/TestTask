using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestTask.DataAccess.Models;

namespace TestTask.DataAccess.Repositories
{
    public class PageRepository : GenericRepository<Page>
    {
        public PageRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<Page> GetEntityByNameAsync(string pageName)
        {
            return await DbSet.FirstOrDefaultAsync(ent => ent.PageName == pageName);
        }
    }
}
