using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PM2E13977.Models;
using SQLite;

namespace PM2E13977.Controllers
{
    public class DBProc
    {
        readonly SQLiteAsyncConnection _connection;
        public DBProc() { }
        public DBProc(string dbpath) 
        {
            _connection = new SQLiteAsyncConnection(dbpath);
            /* Crear todos los objetos de base de datos : tablas */
            _connection.CreateTableAsync<Models.Sitio>().Wait();
        }

        /*Crear el CRUD de BD */

        //Create
        public Task<int> AddSitio(Sitio sitio) 
        {
            if (sitio.Id == 0)
            {
                return _connection.InsertAsync(sitio);
            }
            else
            {
                return _connection.UpdateAsync(sitio);
            }
        }

        //Read
        public Task<List<Sitio>> GetAll()
        {
            return _connection.Table<Sitio>().ToListAsync();
        }

        public Task<Sitio> Get(int id) 
        {
            return _connection.Table<Sitio>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        //Delete
        public Task<int> DeleteSitio(Sitio sitio)
        {
            return _connection.DeleteAsync(sitio);
        }


    }
}
