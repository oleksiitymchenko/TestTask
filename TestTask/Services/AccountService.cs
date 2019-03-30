using System.Threading.Tasks;
using TestTask.DataAccess;
using TestTask.DataAccess.Models;
using TestTask.DataAccess.Repositories;
using TestTask.ViewModels;

namespace TestTask.Services
{
    public class AccountService
    {
        private UserRepository repository;

        public AccountService(ApplicationContext context)
        {
            this.repository = new UserRepository(context);
        }

        public async Task<User> FindUserByCredentialsAsync(LoginModel model)
        {
           return await repository.GetEntityAsync(predicate: 
               u => u.Email == model.Email && u.Password == model.Password);
        }

        public async Task<bool> FindAndAddAsync(RegisterModel model)
        {
            var user = await repository.GetEntityAsync(u => u.Email == model.Email);
            if (user == null)
            {
                await repository.CreateAsync(new User { Email = model.Email, Password = model.Password });
                return true;
            }
            return false;
        }
    }
}
