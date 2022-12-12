using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestForOski.Api.Entities;
using TestForOski.Api.Helpers;
using TestForOski.Api.Models.DTO;
using TestForOski.Api.Models.ResultModel;
using TestForOski.Api.Services.Interfaces;

namespace TestForOski.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _applicationContext;

        public AccountService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
    
        public Task<Result<Account>> CreateAccountAsync(CreateAccountDto accountModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result<Account>> GetAccountCredentialsAsync(AuthenticationDto authenticationModel)
        {
            var account = await _applicationContext.Accounts
                        .FirstOrDefaultAsync(account => account.Email == authenticationModel.Email);

            if (account != null)
            {
                string password = PasswordHelper.HashPassword(authenticationModel.Password, account.Salt);

                if (password != account.Password)
                {
                    return Result<Account>.GetError(ErrorCode.Unauthorized, "Email or password is incorrect.");
                }
                else
                {
                    return Result<Account>.GetSuccess(account);
                }
            }

            return Result<Account>.GetError(ErrorCode.NotFound, "User Not Found");
        }
    }
}
