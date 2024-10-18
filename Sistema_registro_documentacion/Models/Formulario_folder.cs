using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Models
{
    public class Formulario_folder
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El campo FOLDER es obligatorio")]
        [Display(Name = "Folder")]
        [DataType(DataType.Text)]
        public string nombre_folder { get; set; }
        [ForeignKey("folder")]
        public ICollection<Documento> Documentos { get; set; }
    }
}
