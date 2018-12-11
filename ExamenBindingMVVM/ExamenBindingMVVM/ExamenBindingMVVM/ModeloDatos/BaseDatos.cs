using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExamenBindingMVVM.ModeloDatos
{
    public class BaseDatos
    {
        readonly SQLiteAsyncConnection database;

        public BaseDatos(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Cita>().Wait();
        }

        // Devuelve las citas ordenadas por la fecha
        public Task<List<Cita>> GetCitas()
        {
            return database.Table<Cita>().OrderByDescending(x => x.FechaCita).ToListAsync();
        }

        public void SaveCita(Cita item)
        {
                //database.UpdateAsync(item);
                database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Cita item)
        {
            return database.DeleteAsync(item);
        }
    }
}
