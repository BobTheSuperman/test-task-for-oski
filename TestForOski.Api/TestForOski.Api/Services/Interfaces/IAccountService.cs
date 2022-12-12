using System.Threading.Tasks;
using TestForOski.Api.Entities;
using TestForOski.Api.Models.DTO;
using TestForOski.Api.Models.ResultModel;

namespace TestForOski.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Result<Account>> CreateAccountAsync(CreateAccountDto accountModel);

        Task<Result<Account>> GetAccountCredentialsAsync(AuthenticationDto authenticationModel);
    }
}
