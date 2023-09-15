using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using TRFinal_Tienda.Modelos;
using Xamarin.Forms.Xaml;

namespace TRFinal_Tienda
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewGame : ContentPage
	{
        public int IdSele { get; set; }
        decimal precio = 0;
        public ViewGame ()
		{
			InitializeComponent ();
            BindingContext = this;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarProducto();
        }
        private async void CargarProducto()
        {
            var reg_contac = await App.contexto.GetGames();
            if (reg_contac.Count > 0)
            {
                var game = reg_contac.FirstOrDefault(u => u.Id == IdSele);
                if (game != null)
                {
                    lblNombre.Text = game.Nombre;
                    imgImagen.Source = game.Imagen;
                    lblDescripcion.Text = game.Descripcion;
                    imgPlataforma.Source = game.ImgCompañia;
                    lblPrecio.Text = string.Format("S/. {0:#,0.00}", game.Precio.ToString());
                    precio = game.Precio;
                    var migame = await App.contexto.GetAdquirido();
                    if (migame.Count > 0)
                    {
                        var games = migame.FirstOrDefault(gamer => gamer.Nombre == game.Nombre);
                        if (games != null)
                        {
                            if(games.Nombre == game.Nombre)
                            {
                                btnAgregar.Text = "COMPRADO";
                                btnAgregar.IsEnabled = false;
                            }
                            else
                            {
                                btnAgregar.Text = "AGREGAR AL CARRITO";
                                btnAgregar.IsEnabled = true;
                            }
                        }
                    }
                    

                }
            }
        }

        private async void btnVolver_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void btnRegalar_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Regalado", "OK");
        }

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            var reg_contac = await App.contexto.GetGames();
            if (reg_contac.Count > 0)
            {
                var game = reg_contac.FirstOrDefault(u => u.Id == IdSele);
                if (game != null)
                {
                    lblNombre.Text = game.Nombre;
                    imgImagen.Source = game.Imagen;
                    lblDescripcion.Text = game.Descripcion;
                    lblPrecio.Text = string.Format("S/. {0:#,0.00}", game.Precio.ToString());
                    precio = game.Precio;
                    var miusuario = await App.contexto.GetUsuarios();
                    if (miusuario.Count > 0)
                    {
                        var usuario = miusuario.FirstOrDefault(usuarios => usuarios.sesion == 1);
                        if (usuario != null)
                        {
                            var carrito = new Carrito
                            {
                                Nombre = game.Nombre,
                                Descripcion = game.Descripcion,
                                Precio = game.Precio,
                                Imagen = game.Imagen,
                                User_id = usuario.id_usuario
                            };
                            var agregar = await App.contexto.ingresarCarrito(carrito);
                            await DisplayAlert("Aviso", "El juego fue agregado a tu carrito de compras", "OK");
                        }
                    }


                }
            }
        }
    }
}