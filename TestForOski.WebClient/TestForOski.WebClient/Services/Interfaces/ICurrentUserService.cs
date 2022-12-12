namespace TestForOski.WebClient.Services.Interfaces
{
    public interface ICurrentUserService
    {
        public long AccountId { get; }
        public string Email { get; }
        public string Name { get; }
        public string Role { get; }
    }
}
