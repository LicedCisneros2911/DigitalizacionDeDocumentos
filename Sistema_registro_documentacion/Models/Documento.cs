using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Models
{
    public class Documento
    {
        [Key]
        public int id { get; set; }
        public Formulario_tipo formulario_Tipo { get; set; }
        public int tipo { get; set; }

        public Formulario_folder formulario_Folder { get; set; }
        public int folder { get; set; }
       
        [Required(ErrorMessage = "El titulo es obligatorio")]
        [Display(Name = "Titulo")]
        public string titulo { get; set; }

        [Required(ErrorMessage = "El CITE es obligatorio")]
        [Display(Name = "Cite")]
        public string cite { get; set; }

        [Required(ErrorMessage = "El llenado de este campo es obligatorio")]
        [Display(Name = "De:")]
        public string de { get; set; }
        public string via { get; set; }

        [Required(ErrorMessage = "El llenado de este campo es obligatorio")]
        [Display(Name = "Dirigido a")]
        public string a { get; set; }

        [Required(ErrorMessage = "La referencia es obligatorio")]
        [Display(Name = "Referencia")]
        public string Ref { get; set; }

        [Required(ErrorMessage = "La fecha es obligatorio")]
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatorio")]
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }

        [Display(Name = "Documento")]
        public byte[] foto { get; set; }

        [Display(Name = "Documento")]
        [NotMapped]
        public IFormFile filefoto { get; set; }
    }
}
