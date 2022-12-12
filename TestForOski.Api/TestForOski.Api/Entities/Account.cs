namespace TestForOski.Api.Entities
{
    public partial class Account
    {
        public ulong Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
