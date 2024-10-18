using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Models
{
    public class Formulario_tipo
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El campo Tipo de documento es obligatorio")]
        [Display(Name = "Tipo de Documento o Informe")]
        [DataType(DataType.Text)]
        public string nombre_tipo { get; set; }
        [ForeignKey("tipo")]
        public ICollection<Documento> Documentos { get; set; }
    }
}
