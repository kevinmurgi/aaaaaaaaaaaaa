using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace JsonPosts.Objetos
{
    public class PostsProxy
    {
        public async static Task<List<Post>> getPosts()
        {
            HttpClient http = new HttpClient();
            var respuesta = await http.GetAsync("https://jsonplaceholder.typicode.com/posts");
            var resultado = await respuesta.Content.ReadAsStringAsync();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Post>));

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resultado));
            List<Post> datos = (List<Post>) serializer.ReadObject(ms);

            return datos;
        }

        public async static Task<List<Comment>> getComments(int postId)
        {
            HttpClient http = new HttpClient();
            var respuesta = await http.GetAsync("https://jsonplaceholder.typicode.com/comments?postId=" + postId);
            var resultado = await respuesta.Content.ReadAsStringAsync();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Comment>));

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resultado));
            List<Comment> datos = (List<Comment>)serializer.ReadObject(ms);

            return datos;
        }
    }

    [DataContract]
    public class Post
    {
        [DataMember]
        public int userId { get; set; }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember]
        public string body { get; set; }
    }

    [DataContract]
    public class Comment
    {
        [DataMember]
        public int postId { get; set; }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string body { get; set; }
    }
}
