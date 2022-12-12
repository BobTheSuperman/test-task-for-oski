using System.Collections.Generic;
using System.Threading.Tasks;
using TestForOski.Api.Entities;
using TestForOski.Api.Models.ResultModel;

namespace TestForOski.Api.Services.Interfaces
{
    public interface ITestService
    {
        Task<IList<Test>> GetUserTestsAsync();
    }
}
