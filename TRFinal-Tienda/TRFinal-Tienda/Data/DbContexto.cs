using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TRFinal_Tienda.Modelos;

namespace TRFinal_Tienda.Data
{
    public class DbContexto
    {
        public readonly SQLiteAsyncConnection cnx;

        public DbContexto(string data)
        {
            cnx = new SQLiteAsyncConnection(data);
            cnx.CreateTableAsync<Usuarios>().Wait();
            cnx.CreateTableAsync<Games>().Wait();
            cnx.CreateTableAsync<BuscadoReciente>().Wait();
            cnx.CreateTableAsync<Carrito>().Wait();
            cnx.CreateTableAsync<JuegosAdquiridos>().Wait();
        }
        public async Task<int> ingresar(Usuarios registro)
        {
            return await cnx.InsertAsync(registro);
        }
        public async Task<int> eliminar(Usuarios registro)
        {
            return await cnx.DeleteAsync(registro);
        }
        public async Task<List<Usuarios>> GetUsuarios()
        {
            return await cnx.Table<Usuarios>().ToListAsync();
        }
        public async Task<int> modificar(Usuarios registro)
        {
            return await cnx.UpdateAsync(registro);

        }

        /* ----------------------------- Games ----------------------------*/
        public async Task<int> ingresarGames(Games juegos)
        {
            return await cnx.InsertAsync(juegos);
        }
        public async Task<int> eliminarGames(Games juegos)
        {
            return await cnx.DeleteAsync(juegos);
        }
        public async Task<List<Games>> GetGames()
        {
            return await cnx.Table<Games>().ToListAsync();
        }
        public async Task<int> modificarGames(Games juegos)
        {
            return await cnx.UpdateAsync(juegos);

        }

        /* ----------------------------- Buscado Reciente ----------------------------*/

        public async Task<int> ingresarReciente(BuscadoReciente reciente)
        {
            return await cnx.InsertAsync(reciente);
        }
        public async Task<int> eliminarReciente(BuscadoReciente reciente)
        {
            return await cnx.DeleteAsync(reciente);
        }
        public async Task<List<BuscadoReciente>> GetReciente()
        {
            return await cnx.Table<BuscadoReciente>().ToListAsync();
        }
        public async Task<int> modificarReciente(BuscadoReciente reciente)
        {
            return await cnx.UpdateAsync(reciente);

        }

        /* ----------------------------- Carrito ----------------------------*/

        public async Task<int> ingresarCarrito(Carrito carrito)
        {
            return await cnx.InsertAsync(carrito);
        }
        public async Task<int> eliminarCarrito(Carrito carrito)
        {
            return await cnx.DeleteAsync(carrito);
        }
        public async Task<int> BorrarTodosLosCarritos()
        {
            return await cnx.ExecuteAsync("DELETE FROM Carrito");
        }
        public async Task<List<Carrito>> GetCarrito()
        {
            return await cnx.Table<Carrito>().ToListAsync();
        }
        public async Task<int> modificarCarrito(Carrito carrito)
        {
            return await cnx.UpdateAsync(carrito);

        }

        /* ----------------------------- Juegos Adquiridos ----------------------------*/

        public async Task<int> ingresarAdquirido(JuegosAdquiridos adquirido)
        {
            return await cnx.InsertAsync(adquirido);
        }
        public async Task<int> ingresarAdquiridos(List<JuegosAdquiridos> adquiridos)
        {
            return await cnx.InsertAllAsync(adquiridos);
        }

        public async Task<int> eliminarAdquirido(JuegosAdquiridos adquirido)
        {
            return await cnx.DeleteAsync(adquirido);
        }
        public async Task<List<JuegosAdquiridos>> GetAdquirido()
        {
            return await cnx.Table<JuegosAdquiridos>().ToListAsync();
        }
        public async Task<int> modificarAdquirido(JuegosAdquiridos adquirido)
        {
            return await cnx.UpdateAsync(adquirido);

        }
    }
}
