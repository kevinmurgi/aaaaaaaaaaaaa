using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using JsonThings.Models;

namespace JsonThings.Data
{
    public class JsonData
    {
        /**
         * Metodo que devuelve el post enviado como parametro.
         */
        public async static Task<Post> GetPost(int post)
        {
            // Intancia de un objeto HttpClient
            HttpClient http = new HttpClient();

            // Recuperamos los datos que nos devuelve la API
            var respuesta = await http.GetAsync("http://localhost:3000/posts/" + post);

            // Recogemos ese resultado como un String
            var resultado = await respuesta.Content.ReadAsStringAsync();

            // Instanciamos un serializador que convierta los bytes en un objeto ConversionEuros
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Post));

            // Creamos un stream al que le pasamos el resultado de la consulta en bytes
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resultado));

            // Generamos el objeto ConverisonEuros utilizando el serializador
            Post datos = (Post)serializer.ReadObject(ms);

            // Devolvemos el objeto obtenido
            return datos;
        }

        /**
         * Metodo que devuelve todos los posts.
         */
        public async static Task<List<Post>> GetPosts()
        {
            // Intancia de un objeto HttpClient
            HttpClient http = new HttpClient();

            // Recuperamos los datos que nos devuelve la API
            var respuesta = await http.GetAsync("http://localhost:3000/posts");

            // Recogemos ese resultado como un String
            var resultado = await respuesta.Content.ReadAsStringAsync();

            // Instanciamos un serializador que convierta los bytes en un objeto ConversionEuros
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Post>));

            // Creamos un stream al que le pasamos el resultado de la consulta en bytes
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resultado));

            // Generamos el objeto ConverisonEuros utilizando el serializador
            List<Post> datos = (List<Post>)serializer.ReadObject(ms);

            // Devolvemos el objeto obtenido
            return datos;
        }
    }
}
