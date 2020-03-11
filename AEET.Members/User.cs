using CsvHelper.Configuration.Attributes;

namespace AEET.Members
{
    public class User
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RealUID { get; set; }
        public string Teléfono { get; set; }
        public string Móvil { get; set; }
        public string Calle { get; set; }
        [Name("Postal code")]
        public string PostalCode { get; set; }
        public string País { get; set; }
        public string Ciudad { get; set; }
        [Name("Other email")]
        public string OtherEmail { get; set; }
        [Name("Nombre de pantalla")]
        public string NombreDePantalla { get; set; }
        public string screename { get; set; }
        [Name("centro-institucion")]
        public string CentroInstitucion { get; set; }
        public string departamento { get; set; }
        public string lineastrabajo { get; set; }
        public string orcidid { get; set; }
        public string problematicas { get; set; }
        public string tareas { get; set; }
        public string ambito { get; set; }
        public string redes_sociales { get; set; }
        public string nacionalidad { get; set; }
        public string docidentidad { get; set; }
        public string direccion_provincia { get; set; }
        public string direccion_estadoccaa { get; set; }
        public string cc { get; set; }
        public string localidad { get; set; }
        public string pais { get; set; }
        public string titularcc { get; set; }
    }
}
