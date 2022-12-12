using System.Collections.Generic;

namespace TestForOski.Api.Entities
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public ulong Id { get; set; }
        public ulong TestId { get; set; }
        public string Text { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
