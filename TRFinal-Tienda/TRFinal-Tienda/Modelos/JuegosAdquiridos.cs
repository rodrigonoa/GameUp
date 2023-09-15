using SQLite;

namespace TRFinal_Tienda.Modelos
{
    public class JuegosAdquiridos
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Nombre { get; set; }
        [NotNull]
        public string Descripcion { get; set; }
        [NotNull]
        public string Imagen { get; set; }
        [NotNull]
        public decimal Precio { get; set; }
        [NotNull]
        public int User_id { get; set; }
    }
}
