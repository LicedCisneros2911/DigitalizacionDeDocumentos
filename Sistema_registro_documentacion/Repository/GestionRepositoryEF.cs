using Microsoft.EntityFrameworkCore;
using Sistema_registro_documentacion.Data;
using Sistema_registro_documentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Repository
{
   
    public class GestionRepositoryEF : IGenericRepository<Formulario_gestion>
    {
        private readonly ApplicationDBContext _db;
        public GestionRepositoryEF(ApplicationDBContext db)
        {
            _db = db;
        }
        public Formulario_gestion Add(Formulario_gestion item)
        {
            _db.formulario_gestion.Add(item);
            _db.SaveChanges();
            return item;
        }

        public List<Formulario_gestion> Filter(List<string> param)
        {
            throw new NotImplementedException();
        }

        public Formulario_gestion Find(int id)
        {
            return _db.formulario_gestion.FirstOrDefault(t => t.id == id);
        }

        public List<Formulario_gestion> GetAll()
        {
            return _db.formulario_gestion.ToList();
        }

        public void Remove(int id)
        {
            Formulario_gestion gestion = _db.formulario_gestion.FirstOrDefault(u => u.id == id);
            _db.formulario_gestion.Remove(gestion);
            _db.SaveChanges();
            return;
        }

        public Formulario_gestion Update(Formulario_gestion item)
        {
            _db.formulario_gestion.Update(item);
            _db.SaveChanges();
            return item;
        }

        public bool Validate(Formulario_gestion item)
        {
            throw new NotImplementedException();
        }
    }
}
