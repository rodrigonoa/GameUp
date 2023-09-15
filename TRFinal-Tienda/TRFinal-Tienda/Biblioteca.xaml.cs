using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRFinal_Tienda
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Biblioteca : ContentPage
	{
		public Biblioteca ()
		{
			InitializeComponent ();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            cargar();
        }

        public async void cargar()
        {
            var juegos = await App.contexto.GetAdquirido();
            if (juegos.Count > 0)
            {
                listaProductos.ItemsSource = juegos;
            }
        }


        private async void btnver_Clicked(object sender, EventArgs e)
        {
            var GameName = (string)((ImageButton)sender).CommandParameter;

            // Hacer lo que sea necesario con el Id del producto seleccionado
            // Por ejemplo, buscar el producto correspondiente en la lista de productos
            var reg_contac = await App.contexto.GetGames();
            var productoSeleccionado = reg_contac.FirstOrDefault(p => p.Nombre == GameName);
            if (productoSeleccionado != null)
            {
                var miusuario = await App.contexto.GetUsuarios();
                if (miusuario.Count > 0)
                {
                    var usuario = miusuario.FirstOrDefault(usuarios => usuarios.sesion == 1);
                    if (usuario != null)
                    {
                        var producto = new JuegoBiblioteca();

                        producto.IdSele = productoSeleccionado.Id;

                        await Navigation.PushAsync(producto);
                    }
                }
            }
        }
    }
}