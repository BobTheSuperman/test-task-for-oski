using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestForOski.Api.Entities;
using TestForOski.Api.Services.Interfaces;

namespace TestForOski.Api.Controllers
{
    [Route("test")]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestsController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        [Authorize(Roles = ("User"))]
        public async Task<IList<Test>> GetTestsForUser()
        {
            return await _testService.GetUserTestsAsync();
        }
    }
}
