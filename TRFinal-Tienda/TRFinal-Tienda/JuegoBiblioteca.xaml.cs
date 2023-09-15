using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRFinal_Tienda.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRFinal_Tienda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JuegoBiblioteca : ContentPage
    {
        string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            string randomString = new string(Enumerable.Repeat(chars, length)
                                                     .Select(s => s[random.Next(s.Length)])
                                                     .ToArray());

            return randomString;
        }

        public int IdSele { get; set; }
        decimal precio = 0;
        string keyrandom;
        public JuegoBiblioteca()
        {
            InitializeComponent();
            string randomCharsAndNumbers = GenerateRandomString(12);
            keyrandom = randomCharsAndNumbers;
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
                }
            }
        }


        private void btnVer_Clicked(object sender, EventArgs e)
        {
            if (lblKey.IsVisible == false)
            {
                btnVer.Text = "OCULTAR KEY";
                lblKey.Text = keyrandom;

                lblKey.IsVisible = true;
            }
            else
            {
                btnVer.Text = "VER KEY";
                lblKey.Text = "";
                lblKey.IsVisible = false;
            }
        }
    }
}