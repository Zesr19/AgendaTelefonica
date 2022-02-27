using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.Models.ViewModels
{
    // View Model para registrar un contacto
    public class NewContactsViewModel
    {
        public int Id_contacto { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El teléfono debe ser de 10 dígitos.")]
        [StringLength(10)]
        [MinLength(10)]
        [MaxLength(10)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }
}