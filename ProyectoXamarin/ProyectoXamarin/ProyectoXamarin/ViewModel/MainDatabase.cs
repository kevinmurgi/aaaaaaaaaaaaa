using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using ProyectoXamarin.DataModel;
using System.Threading.Tasks;

namespace ProyectoXamarin.ViewModel
{
    public class MainDatabase
    {
        readonly SQLiteAsyncConnection database;

        public MainDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Autor>().Wait();
            database.CreateTableAsync<Libro>().Wait();
        }

        // Metodos que devuelven una lista con los autores
        public Task<List<Autor>> GetAutores()
        {
            return database.Table<Autor>().ToListAsync();
        }
        public Task<List<Libro>> GetLibros()
        {
            return database.Table<Libro>().ToListAsync();
        }
        
        // Metodos que añaden objetos
        public Task<int> AddAutor(Autor item)
        {
            return database.InsertAsync(item);
        }
        public Task<int> AddLibro(Libro item)
        {
            return database.InsertAsync(item);
        }

        // Metodos para borrar objetos
        public Task<int> DeleteAutor(Autor item)
        {
            return database.DeleteAsync(item);
        }
        public Task<int> DeleteLibro(Libro item)
        {
            return database.DeleteAsync(item);
        }
    }
}
