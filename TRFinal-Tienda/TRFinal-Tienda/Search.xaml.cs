using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRFinal_Tienda.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRFinal_Tienda
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Search : ContentPage
	{
        private ObservableCollection<Games> listaProductosMostradas;
        private List<BuscadoReciente> registrosSeleccionados = new List<BuscadoReciente>();
        private int estado = 0;
		public Search ()
		{
			InitializeComponent ();
            OnAppearing();
			
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtBuscar.Text = "";
            cargar();
        }

        public async void cargar()
		{
            var juegos = await App.contexto.GetReciente();
            juegos = juegos.OrderByDescending(juego => juego.Id).ToList();
            if (juegos.Count > 0)
            {
                if(juegos.Count > 6)
                {
                    if (estado == 0)
                    {
                        listaProductos.ItemsSource = juegos.Take(6);
                        
                        stBuscado.IsVisible = false;
                        stGames.IsVisible = true;
                        btnMostrarTodo.IsVisible = true;
                    }
                    else if (estado == 1)
                    {
                        btnMostrarTodo.IsVisible = true;
                        listaProductos.ItemsSource = juegos;
                        stGames.IsVisible=true;
                        stBuscado.IsVisible = false;
                    }
                }
                else
                {
                    btnMostrarTodo.IsVisible = false;
                    listaProductos.ItemsSource = juegos;
                    stGames.IsVisible = true;
                    stBuscado.IsVisible = false;
                }
            }
        }

        private async void btnMostrarTodo_Clicked(object sender, EventArgs e)
        {
            estado++;
            OnAppearing();
        }

        private async void btnBuscar_Clicked(object sender, EventArgs e)
        {
            string terminoBusqueda = txtBuscar.Text.Trim().ToLower();

            var registros = await App.contexto.GetGames();
            var registrosFiltrados = registros.Where(registro => registro.Nombre.ToLower().Contains(terminoBusqueda)).ToList();

            stGames.IsVisible = false;
            stBuscado.IsVisible = true;
            listaBuscar.ItemsSource = registrosFiltrados;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var GameId = (int)((Button)sender).CommandParameter;

            // Hacer lo que sea necesario con el Id del producto seleccionado
            // Por ejemplo, buscar el producto correspondiente en la lista de productos
            var reg_contac = await App.contexto.GetGames();
            var productoSeleccionado = reg_contac.FirstOrDefault(p => p.Id == GameId);
            if (productoSeleccionado != null)
            {
                var miusuario = await App.contexto.GetUsuarios();
                if (miusuario.Count > 0)
                {
                    var usuario = miusuario.FirstOrDefault(usuarios => usuarios.sesion == 1);
                    if (usuario != null)
                    {
                        var producto = new ViewGame();
                        var juego = new BuscadoReciente()
                        {
                            Nombre = productoSeleccionado.Nombre,
                            Imagen = productoSeleccionado.Imagen,
                            Precio = productoSeleccionado.Precio,
                            Game_id = productoSeleccionado.Id,
                            User_id = usuario.id_usuario
                        };
                        producto.IdSele = productoSeleccionado.Id;

                        var agregar = await App.contexto.ingresarReciente(juego);
                        await Navigation.PushAsync(producto);
                    }
                }      
            }
        }
    }
}