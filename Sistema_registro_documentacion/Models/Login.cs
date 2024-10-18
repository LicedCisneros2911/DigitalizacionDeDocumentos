using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Models
{
    public class Login
    {
        [PersonalData]
        [Required(ErrorMessage = "El usuario es obligatorio")]
        [DataType(DataType.Text)]
        [Display(Name = "Usuario")]
        public string usuario { get; set; }

        [PersonalData]
        [Required(ErrorMessage = "El password es obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [NotMapped]//No lo toma encuenta
        public string rol { get; set; }
    }
}
