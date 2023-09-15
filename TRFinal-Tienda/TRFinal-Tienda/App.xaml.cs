using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TRFinal_Tienda.Data;

namespace TRFinal_Tienda
{
    public partial class App : Application
    {
        public static MasterDetailPage MAsterDet { get; set; }
        public static DbContexto contexto { get; set; }
        public App()
        {
            InitializeComponent();
            crearDB();
            MainPage = new NavigationPage(new Welcome());
        }

        private void crearDB()
        {
            var carpeta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var data = System.IO.Path.Combine(carpeta, "DB_TRFinal3.db3");
            SQLitePCL.Batteries_V2.Init();
            contexto = new DbContexto(data);
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
