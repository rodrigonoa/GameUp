using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TRFinal_Tienda.Modelos
{
    public class Carrito
    {
        [PrimaryKey,AutoIncrement]
        public int Id { set; get; }
        [NotNull]
        public string Nombre { set; get; }
        [NotNull]
        public string Descripcion { set; get; }
        [NotNull]
        public string Imagen { get; set; }
        [NotNull]
        public decimal Precio { get; set; }
        [NotNull]
        public int User_id { get; set; }
    }
}
