using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Models
{
    public class Formulario_gestion
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El campo gestion es obligatorio")]
        [Display(Name = "Gestion")]
        public string nombre_gestion
        { get; set; }
    }
}
