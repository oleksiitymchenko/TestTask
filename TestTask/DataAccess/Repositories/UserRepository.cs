using TestTask.DataAccess.Models;

namespace TestTask.DataAccess.Repositories
{
    public class UserRepository:GenericRepository<User>
    {
        public UserRepository(ApplicationContext context):base(context)
        {
        }
    }
}
