using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRFinal_Tienda.Modelos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TRFinal_Tienda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }
        private async void btnEnviar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (txtContra.Text == txtConfirContra.Text)
                {
                    var reg = new Usuarios
                    {
                        usuario = txtUsuario.Text,
                        correo = txtEmail.Text,
                        contra = txtContra.Text,
                        sesion = 0
                    };
                    var respta = await App.contexto.ingresar(reg);
                    if (respta == 1)
                    {
                        string dbPath = App.contexto.cnx.DatabasePath;
                        using (SQLiteConnection conn = new SQLiteConnection(dbPath))
                        {
                            conn.CreateTable<Usuarios>();
                            var user = conn.Table<Usuarios>().FirstOrDefault(u => u.correo.ToLower() == txtEmail.Text.ToLower());
                            if (user != null)
                            {
                                user.sesion = 1;
                                var nreg = await App.contexto.modificar(user);
                                if (nreg == 1)
                                {
                                    await DisplayAlert("Aviso", "Usuario registrado correctamente", "Aceptar");
                                    await Navigation.PushAsync(new Home());
                                }
                            }
                        }
                    }
                    else
                    {
                        await DisplayAlert("Aviso", "No se registraron los datos correctamente", "Aceptar");
                    }
                }
                else
                {
                    await DisplayAlert("Aviso", "Las contraseñas no coinciden", "Aceptar");
                }
                
            }
            catch (Exception er)
            {
                await DisplayAlert("Aviso", er.Message, "Aceptar");
            }
        }
    }
}