using Microsoft.Extensions.Logging;
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
        private ILogger logger;
        public AccountService(ApplicationContext context, ILogger<AccountService> logger)
        {
            this.logger = logger;
            this.repository = new UserRepository(context);
        }

        public async Task<User> FindUserByCredentialsAsync(LoginModel model)
        {
            logger.LogInformation($"Searching for user {model.Email}");
            var entity =  await repository.GetEntityAsync(
                u => u.Email == model.Email && u.Password == model.Password);
            if (entity == null) logger.LogWarning($"User {model.Email} not found");
            return entity;
        }
        
        public async Task<bool> FindAndAddAsync(RegisterModel model)
        {
            logger.LogInformation($"Searching for user {model.Email}");
            var user = await repository.GetEntityAsync(u => u.Email == model.Email);
            if (user == null)
            {
                logger.LogWarning($"User {model.Email} not found");
                logger.LogInformation($"Creating new user {model.Email}");
                await repository.CreateAsync(new User { Email = model.Email, Password = model.Password });
                return true;
            }
            logger.LogInformation($"User {model.Email} is found, no need to register");
            return false;
        }
    }
}
