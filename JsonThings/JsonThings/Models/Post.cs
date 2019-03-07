using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JsonThings.Models
{
    [DataContract]
    public class Post
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string author { get; set; }
    }
}
