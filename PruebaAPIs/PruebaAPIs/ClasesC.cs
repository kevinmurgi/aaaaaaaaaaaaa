using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAPIs
{
    public class ClasesC
    {
        public async static Task<List<User>> getUsers()
        {
            HttpClient http = new HttpClient();
            var respuesta = await http.GetAsync("http://jsonplaceholder.typicode.com/users");
            var resultado = await respuesta.Content.ReadAsStringAsync();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<User>));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resultado));
            List<User> usuarios = (List<User>) serializer.ReadObject(ms);
            return usuarios;
        }
    }

    [DataContract]
    public class Geo
    {
        [DataMember]
        public string lat { get; set; }

        [DataMember]
        public string lng { get; set; }
    }

    [DataContract]
    public class Address
    {
        [DataMember]
        public string street { get; set; }

        [DataMember]
        public string suite { get; set; }

        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string zipcode { get; set; }

        [DataMember]
        public Geo geo { get; set; }
    }

    [DataContract]
    public class Company
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string catchPhrase { get; set; }

        [DataMember]
        public string bs { get; set; }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string username { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public Address address { get; set; }

        [DataMember]
        public string phone { get; set; }

        [DataMember]
        public string website { get; set; }

        [DataMember]
        public Company company { get; set; }
    }
}
