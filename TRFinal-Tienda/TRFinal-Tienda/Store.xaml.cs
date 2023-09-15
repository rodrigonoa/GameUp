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
    public partial class Store : ContentPage
    {
        public Store()
        {
            InitializeComponent();
            btnMenor.Text = "<".ToString();
            btnMayor.Text = ">".ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarAgendas();
        }
        private async void CargarAgendas()
        {
            var reg_contac = await App.contexto.GetUsuarios();
            if (reg_contac.Count > 0)
            {
                var usuario = reg_contac.FirstOrDefault(u => u.sesion == 1);
                if (usuario != null)
                {
                    //lblName.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase("Bienvenid@ " + usuario.usuario);
                    imgBtnProfile.IsVisible = true;
                    lblPresentacion.Text = "Bienvenid@ " + usuario.usuario;
                }
                else
                {
                    imgBtnProfile.IsVisible = false;
                    lblPresentacion.Text = "Bienvenid@ Gamer";
                }
            }
        }

        private async void btnArma_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso","Arma","OK");
        }

        private async void btnMochila_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Mochila", "OK");
        }

        private async void btnCarrera_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Carrera", "OK");
        }
        private async void OnGameTapped1(object sender, EventArgs e)
        {
            if (sender is StackLayout stackLayout && stackLayout.GestureRecognizers[0] is TapGestureRecognizer tapGesture)
            {
                string gameName = tapGesture.CommandParameter.ToString();
                await DisplayAlert("Aviso", "Terraria", "OK");
  
            }
        }
        private async void OnGameTapped2(object sender, EventArgs e)
        {
            if (sender is StackLayout stackLayout && stackLayout.GestureRecognizers[0] is TapGestureRecognizer tapGesture)
            {
                string gameName = tapGesture.CommandParameter.ToString();
                await DisplayAlert("Aviso", "Garry´s Mod", "OK");

            }
        }
        private async void OnGameTapped3(object sender, EventArgs e)
        {
            if (sender is StackLayout stackLayout && stackLayout.GestureRecognizers[0] is TapGestureRecognizer tapGesture)
            {
                string gameName = tapGesture.CommandParameter.ToString();
                await DisplayAlert("Aviso", "Left 4 Dead 2", "OK");

            }
        }

        private void imgBtnProfile_Clicked(object sender, EventArgs e)
        {
            if(fondoPerfil.IsVisible == false)
            {
                fondoPerfil.IsVisible = true;
                btnCerrarSesion.IsVisible = true;
                btnChatBot.IsVisible = true;
            }
            else
            {
                fondoPerfil.IsVisible = false;
                btnCerrarSesion.IsVisible = false;
                btnChatBot.IsVisible = false;
            }
            
        }

        private async void imgBtnOfertas_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Ofertas", "OK");
        }

        private async void btnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            string dbPath = App.contexto.cnx.DatabasePath;
            using (SQLiteConnection conn = new SQLiteConnection(dbPath))
            {
                conn.CreateTable<Usuarios>();
                var user = conn.Table<Usuarios>().FirstOrDefault(u => u.sesion == 1);
                if (user != null)
                {
                    user.sesion = 0;
                    var nreg = await App.contexto.modificar(user);
                    if (nreg == 1)
                    {
                        await DisplayAlert("Success", "Su sesión termino", "OK");
                        await Navigation.PushAsync(new Welcome());
                    }
                }
            }
        }

        private async void btnChatBot_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChatBot());
        }
    }
}