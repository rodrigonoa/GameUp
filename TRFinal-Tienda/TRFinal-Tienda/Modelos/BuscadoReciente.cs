using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TRFinal_Tienda.Modelos
{
    public class BuscadoReciente
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        [NotNull]
        public string Nombre { get; set; }
        [NotNull]
        public decimal Precio { get; set; }
        [NotNull]
        public string Imagen { get; set; }
        [NotNull]
        public int Game_id { get; set; }
        [NotNull]
        public int User_id { get; set; }
    }
}
