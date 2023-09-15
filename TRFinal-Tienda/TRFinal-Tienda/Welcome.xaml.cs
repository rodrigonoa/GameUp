using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRFinal_Tienda.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRFinal_Tienda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Welcome : ContentPage
    {
        public Welcome()
        {
            InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                Navigation.PushAsync(new Home());
            };

            lblLink.GestureRecognizers.Add(tapGestureRecognizer);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarAgendas();
        }
        private async void CargarAgendas()
        {
            var reg_game = await App.contexto.GetGames();
            if (reg_game.Count > 0)
            {
                var reg_contac = await App.contexto.GetUsuarios();
                if (reg_contac.Count > 0)
                {
                    var usuario = reg_contac.FirstOrDefault(u => u.sesion == 1);
                    if (usuario != null)
                    {
                        await Navigation.PushAsync(new Home());
                    }
                }
            }
            else
            {
                var miseeder = SeedersData.GetInitialData();
                foreach (var misedeers in miseeder)
                {
                    var reg = new Games
                    {
                        Nombre = misedeers.Nombre,
                        Descripcion = misedeers.Descripcion,
                        Categorias = misedeers.Categorias,
                        Imagen = misedeers.Imagen,
                        ImgCompañia = misedeers.ImgCompañia,
                        Precio = misedeers.Precio,
                    };
                    var respta = await App.contexto.ingresarGames(reg);
                    if (respta == 1)
                    {
                        var reg_contac = await App.contexto.GetUsuarios();
                        if (reg_contac.Count > 0)
                        {
                            var usuario = reg_contac.FirstOrDefault(u => u.sesion == 1);
                            if (usuario != null)
                            {
                                await Navigation.PushAsync(new Home());
                            }
                        }
                    }
                }
            }
        }
            
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }
    }
}