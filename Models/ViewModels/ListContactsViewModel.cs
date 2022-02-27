using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.Models.ViewModels
{
    // View Model para listar los registros
    public class ListContactsViewModel
    {
        public int Id_contacto { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

    }
}