using System.Linq;
using System.Threading.Tasks;
using TestTask.DataAccess;
using TestTask.DataAccess.Models;
using TestTask.DataAccess.Repositories;

namespace TestTask.Services
{
    public class PageService
    {
        private PageRepository pageRepository;
        private UserLikedPageRepository userPageRepository;

        public PageService(ApplicationContext context)
        {
            this.pageRepository = new PageRepository(context);
            this.userPageRepository = new UserLikedPageRepository(context);
        }

        public async Task<Page> CreateIfNotExists(string name)
        {
            var page = await pageRepository.GetEntityAsync(item => item.PageName == name);

            if (page != null) return page;

            var newPage = new Page()
            {
                Likes = 0,
                PageName = name
            };
            return await pageRepository.CreateAsync(newPage);
        }

        public async Task<bool> CheckIsLiked(string pagename, string username)
        {
            var entities = await userPageRepository.GetEntitiesAsync();
            bool isLiked = !(entities.FirstOrDefault(e => e.Pagename == pagename && e.Username == username) == null);

            return isLiked;
        }

        public async Task<int> LikePage(string PageName, string UserName, int likes)
        {
            await userPageRepository.CreateAsync(new UserLikedPage()
            {
                Pagename = PageName,
                Username = UserName
            });
            var page = await pageRepository.GetEntityAsync(p => p.PageName == PageName);

            page.Likes += likes;
            await pageRepository.UpdateAsync(page, page.Id);
            return page.Likes;
        }
    }
}
