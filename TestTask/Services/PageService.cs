using Microsoft.Extensions.Logging;
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
        private ILogger logger;
        public PageService(ApplicationContext context, ILogger<AccountService> logger)
        {
            this.logger = logger;
            this.pageRepository = new PageRepository(context);
            this.userPageRepository = new UserLikedPageRepository(context);
        }

        public async Task<Page> CreateIfNotExists(string name)
        {
            logger.LogInformation($"Searching for {name} page");
            var page = await pageRepository.GetEntityAsync(item => item.PageName == name);

            if (page != null) return page;

            logger.LogInformation($"Page {name} not found, creating new");
            var newPage = new Page()
            {
                Likes = 0,
                PageName = name
            };
            return await pageRepository.CreateAsync(newPage);
        }

        public async Task<bool> CheckIsLiked(string pagename, string username)
        {
            logger.LogInformation($"Checking if the user {username} liked {pagename}");
            bool isLiked = !((await userPageRepository.GetEntityAsync(
                e => e.Pagename == pagename && e.Username == username)) == null);
            logger.LogInformation($"{username} liked {pagename} = {isLiked}");
            return isLiked;
        }

        public async Task<int> LikePage(string PageName, string UserName, int likes)
        {
            logger.LogInformation($"User {UserName} liked page {PageName}");
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
