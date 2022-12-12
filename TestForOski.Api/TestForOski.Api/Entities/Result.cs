namespace TestForOski.Api.Entities
{
    public class Result
    {
        public ulong Id { get; set; }
        public ulong TestId { get; set; }
        public ulong UserId { get; set; }
        public int CorrectAnswerAmount { get; set; }

        public virtual Test Test { get; set; }
        public virtual Account User { get; set; }
    }
}
