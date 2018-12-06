using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using ProyectoXamarin.DataModel;
using System.Collections.ObjectModel;

/*
 * Clase DataAccess que implementa un serie de métodos para poder trabajar con la BBDD SQLite
 */
namespace ProyectoXamarin.ViewModel
{
    public static class DataAccess
    {
        // Metodo que crea las tablas de la base de datos
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS Autores( " +
                    "nombre_autor varchar(20)," +
                    "apellidos_autor varchar(40)," +
                    "fecha_nacimiento date," +
                    "telefono varchar(12)," +
                    "sexo varchar(10)," +
                    "constraint pk_autores primary key (nombre_autor)" + 
                    ")";

                String tableCommand2 = "CREATE TABLE IF NOT EXISTS Libros( " +
                    "nombre_libro varchar(30)," +
                    "autor varchar(20)," +
                    "fecha_lanzamiento date," +
                    "paginas integer," +
                    "genero varchar(60)," +
                    "constraint pk_libros primary key(nombre_libro)," + 
                    "constraint fk_autores foreign key(autor) references Autores(nombre_autor) on update cascade on delete restrict" +
                    ")";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                SqliteCommand createTable2 = new SqliteCommand(tableCommand2, db);

                createTable.ExecuteReader();
                createTable2.ExecuteReader();
            }
        }

        // Metodo que vacia las tablas de la base de datos y las elimina
        public static void BorrarTablas()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                String tableCommand = "drop table Libros";
                String tableCommand2 = "drop table Autores";

                SqliteCommand deleteTable = new SqliteCommand(tableCommand, db);
                SqliteCommand deleteTable2 = new SqliteCommand(tableCommand2, db);

                deleteTable.ExecuteReader();
                deleteTable2.ExecuteReader();

                db.Close();
            }

        }

        // Metodo utilizado para añadir un nuevo autor a la base de datos
        public static void AddAutor(string nombre, string apellidos, string fecha, string telefono, string sexo)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Autores VALUES (@Entry1, @Entry2, @Entry3, @Entry4, @Entry5);";
                insertCommand.Parameters.AddWithValue("@Entry1", nombre);
                insertCommand.Parameters.AddWithValue("@Entry2", apellidos);
                insertCommand.Parameters.AddWithValue("@Entry3", fecha);
                insertCommand.Parameters.AddWithValue("@Entry4", telefono);
                insertCommand.Parameters.AddWithValue("@Entry5", sexo);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        // Metodo utilizado para añadir un nuevo libro a la base de datos
        public static void AddLibro(string nombre, string autor, string fecha, int paginas, string genero)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Libros VALUES (@Entry1, @Entry2, @Entry3, @Entry4, @Entry5);";
                insertCommand.Parameters.AddWithValue("@Entry1", nombre);
                insertCommand.Parameters.AddWithValue("@Entry2", autor);
                insertCommand.Parameters.AddWithValue("@Entry3", fecha);
                insertCommand.Parameters.AddWithValue("@Entry4", paginas);
                insertCommand.Parameters.AddWithValue("@Entry5", genero);

                insertCommand.ExecuteReader();

                db.Close();
            }

        }

        // Metodo que devuelve todos los autores almacenados en la base de datos con una variable List<>
        public static ObservableCollection<Autor> GetAutores()
        {
            ObservableCollection<Autor> autores = new ObservableCollection<Autor>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Autores order by nombre_autor", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    autores.Add(new Autor
                    {
                        Nombre = query.GetString(0),
                        Apellidos = query.GetString(1),
                        Nacimiento = query.GetString(2),
                        Telefono = query.GetString(3),
                        Sexo = query.GetString(4)
                    });
                }
                db.Close();
            }

            return autores;
        }

        // Metodo que devuelve todos los nombres de autores almacenados en la base de datos con una variable List<>
        public static ObservableCollection<string> GetNombreAutores()
        {
            ObservableCollection<string> nomAutores = new ObservableCollection<string>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT nombre_autor from Autores", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    nomAutores.Add(query.GetString(0));
                }
                db.Close();
            }

            return nomAutores;
        }

        // Metodo que devuelve todos los libros almacenados en la base de datos con una variable List<>
        public static ObservableCollection<Libro> GetLibros()
        {
            ObservableCollection<Libro> libros = new ObservableCollection<Libro>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Libros order by nombre_libro", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    libros.Add(new Libro
                    {
                        Nombre = query.GetString(0),
                        AutorLibro = query.GetString(1),
                        Lanzamiento = query.GetString(2),
                        Paginas = Int32.Parse(query.GetString(3)),
                        Genero = query.GetString(4)
                    });
                }
                db.Close();
            }
            return libros;
        }

        // Metodo para eliminar una entrada de la tabla autores
        public static void BorrarAutor(string autor)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                String tableCommand = "delete from Autores where nombre_autor = '" + autor + "'";

                SqliteCommand deleteTable = new SqliteCommand(tableCommand, db);

                deleteTable.ExecuteReader();

                db.Close();
            }

        }

        // Metodo para eliminar una entrada de la tabla libros
        public static void BorrarLibro(string libro)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();

                String tableCommand = "delete from Libros where nombre_libro = '" + libro + "'";

                SqliteCommand deleteTable = new SqliteCommand(tableCommand, db);

                deleteTable.ExecuteReader();

                db.Close();
            }

        }
    }
}
