using System.Collections.Generic;

namespace PostawNaMilion.Models
{
    public class Category
    {
        public string Name { get; set; }

        public IEnumerable<Question> Questions { get; set; }

        public bool NotUsed { get; set; }
    }
}