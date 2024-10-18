using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sistema_registro_documentacion.Models;
namespace Sistema_registro_documentacion.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Documento> documento { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Formulario_tipo> formulario_tipo { get; set; }
        public DbSet<Formulario_gestion> formulario_gestion { get; set; }
        public DbSet<Formulario_folder> formulario_folder { get; set; }
    }
  

}
