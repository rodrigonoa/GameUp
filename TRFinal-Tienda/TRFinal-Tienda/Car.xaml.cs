using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRFinal_Tienda.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRFinal_Tienda
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Car : ContentPage
	{
        private int estado = 0;
		public Car ()
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
            var juegos = await App.contexto.GetCarrito();
            if (juegos.Count > 0)
            {
                if(estado == 0)
                {
                    listaProductos.ItemsSource = juegos.Take(4);

                    lblTotal.Text = juegos.Sum(p => p.Precio).ToString();
                }
                else if (estado == 1)
                {
                    btnMostrarTodo.IsVisible = false;
                    listaProductos.ItemsSource = juegos;
                    lblTotal.Text = juegos.Sum(p => p.Precio).ToString();
                }
            }
            else
            {
                lblTotal.Text = "S/.00.00";
                listaProductos.ItemsSource = null;
            }
        }

        private void btnMostrarTodo_Clicked(object sender, EventArgs e)
        {
            estado++;
            OnAppearing();
        }

        private async void btnComprar_Clicked(object sender, EventArgs e)
        {
            var carrito = await App.contexto.GetCarrito();

            List<JuegosAdquiridos> juegosAdquiridos = new List<JuegosAdquiridos>();
            var miusuario = await App.contexto.GetUsuarios();
            if (miusuario.Count > 0)
            {
                var usuario = miusuario.FirstOrDefault(usuarios => usuarios.sesion == 1);
                if (usuario != null)
                {
                    foreach (var item in carrito)
                    {
                        var juegoAdquirido = new JuegosAdquiridos
                        {
                            Nombre = item.Nombre,
                            Precio = item.Precio,
                            Descripcion = item.Descripcion,
                            Imagen = item.Imagen,
                            User_id = usuario.id_usuario,
                        };
                        juegosAdquiridos.Add(juegoAdquirido);
                    }
                    await App.contexto.ingresarAdquiridos(juegosAdquiridos);
                    await App.contexto.BorrarTodosLosCarritos();

                    await DisplayAlert("Aviso", "Juegos Agregados a mi biblioteca", "OK");
                    OnAppearing();
                }
            }
        }
    }
}