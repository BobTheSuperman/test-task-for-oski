namespace TestForOski.Api.Entities
{
    public partial class Answer
    {
        public ulong Id { get; set; }
        public ulong QuestionId { get; set; }
        public string Text { get; set; }
        public ulong? IsCorrect { get; set; }

        public virtual Question Question { get; set; }
    }
}
