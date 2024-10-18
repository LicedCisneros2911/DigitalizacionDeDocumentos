using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Sistema_registro_documentacion.Models
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        [PersonalData]
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [DataType(DataType.Text)]
        [Display(Name ="Nombre")]
        public string nombre { get; set; }

        [PersonalData]
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [DataType(DataType.Text)]
        [Display(Name = "Paterno")]
        public string apppaterno { get; set; }

        [PersonalData]
        [DataType(DataType.Text)]
        [Display(Name = "Materno")]
        public string appmaterno { get; set; }

        [PersonalData]
        [Required(ErrorMessage = "El Nº de telefono es obligatorio")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefono")]
        public int telefono { get; set; }

        [PersonalData]
        [DataType(DataType.Text)]
        [Display(Name = "Estado")]
        public string estado { get; set; }

        [PersonalData]
        [Required(ErrorMessage = "Debe elegir el rol")]
        [Display(Name = "Rol")]
        public string rol { get; set; }

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
    }
}
