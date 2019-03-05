using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonThings.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string body { get; set; }
        public int postId { get; set; }
    }
}
