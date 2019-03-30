using TestTask.DataAccess.Models;

namespace TestTask.DataAccess.Repositories
{
    public class UserLikedPageRepository : GenericRepository<UserLikedPage>
    {
        public UserLikedPageRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
