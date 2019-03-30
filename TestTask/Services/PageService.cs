using System;
using System.Collections.Generic;
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
            var entity = await pageRepository.GetEntitiesAsync();
            var page = entity.FirstOrDefault(item => item.PageName == name);

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
    }
}
