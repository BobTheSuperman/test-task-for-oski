using TestForOski.Api.Entities;

namespace TestForOski.Api.Helpers
{
    public interface IJwtGenerator
    {
        public string GenerateEncodedJwt(Account account);
    }
}
