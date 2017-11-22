using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostawNaMilion.Models
{
     public class Question
    {
        public string Content { get; set; }

        public IEnumerable<Answer> Answers { get; set; }
    }
}
