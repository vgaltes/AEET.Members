using CsvHelper.Configuration.Attributes;

namespace AEET.Members
{
    public class Member
    {
        public string Fecha { get;set; }
        public string Docidentidad { get;set; }
        public string Email { get;set; }
        public string Codsocio { get;set; }
        public string Fechaalta { get;set; }
        public string Tipopersona { get;set; }
        public string Tiposocio { get;set; }
        public string Tipotarifa { get;set; }
        public string Importetarifa { get;set; }
        public string Formapago { get;set; }
        public string Fecgabaja { get;set; }
        public string Idioma { get;set; }
        public string Navegador { get;set; }
        public string IP { get;set; }
        [Name("Usuario de Abcore")]
        public string UsuarioDeAbcore { get;set; }
        [Name("ID relacionada")]
        public string IDRelacionada { get;set; }
    }
}
