using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TRFinal_Tienda.Modelos
{
    public class Games
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        [NotNull]
        public string Nombre { get; set; }
        [NotNull]
        public string Descripcion { get; set; }
        [NotNull]
        public decimal Precio { get; set; }
        [NotNull]
        public string Imagen { get; set; }
        [NotNull]
        public int Categorias { get; set; }
        // Aventura = 1
        // Carreras = 2
        // Accion = 3
        [NotNull]
        public string ImgCompañia { get; set; }
    }
}
