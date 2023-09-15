using SQLite;
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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btnEnviar_Clicked(object sender, EventArgs e)
        {
            string Correo = txtEmail.Text;
            string Contra = txtContra.Text;

            string dbPath = App.contexto.cnx.DatabasePath;
            using (SQLiteConnection conn = new SQLiteConnection(dbPath))
            {
                conn.CreateTable<Usuarios>();
                var user = conn.Table<Usuarios>().FirstOrDefault(u => u.correo.ToLower() == Correo.ToLower() && u.contra == Contra);
                if (user != null)
                {
                    user.sesion = 1;
                    var nreg = await App.contexto.modificar(user);
                    if (nreg == 1)
                    {
                        await DisplayAlert("AVISO", "Inicio de sesión exitoso", "OK");
                        await Navigation.PushAsync(new Home());
                    }
                }
            }
        }
    }
}
