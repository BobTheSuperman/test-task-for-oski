using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForOski.Api.Entities;
using TestForOski.Api.Models.ResultModel;
using TestForOski.Api.Services.Interfaces;

namespace TestForOski.Api.Services
{
    public class TestService : ITestService
    {
        private readonly ApplicationContext _applicationContext;

        public TestService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IList<Test>> GetUserTestsAsync()
        {
            return await _applicationContext.Tests
                .Where(t => t.Id == 1
                || t.Id == 2
                || t.Id == 3
                || t.Id == 4
                || t.Id == 5).ToListAsync();
        }
    }
}
