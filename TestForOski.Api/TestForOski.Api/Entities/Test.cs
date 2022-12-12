using System.Collections.Generic;

namespace TestForOski.Api.Entities
{
    public partial class Test
    {
        public Test()
        {
            Questions = new HashSet<Question>();
        }

        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
