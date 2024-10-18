using Sistema_registro_documentacion.Data;
using Sistema_registro_documentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Repository
{
    public class TipoRepositoryEF : IGenericRepository<Formulario_tipo>
    {
        private readonly ApplicationDBContext _db;
        public TipoRepositoryEF(ApplicationDBContext db)
        {
            _db = db;
        }
        public Formulario_tipo Add(Formulario_tipo item)
        {
            _db.formulario_tipo.Add(item);
            _db.SaveChanges();
            return item;
        }

        public List<Formulario_tipo> Filter(List<string> param)
        {
            throw new NotImplementedException();
        }

        public Formulario_tipo Find(int id)
        {
            return _db.formulario_tipo.FirstOrDefault(t => t.id == id);
        }

        public List<Formulario_tipo> GetAll()
        {
            List<Formulario_tipo> tipos = new List<Formulario_tipo>();
            tipos = _db.formulario_tipo.ToList();
            return tipos;
        }

        public void Remove(int id)
        {
            Formulario_tipo tipo = _db.formulario_tipo.FirstOrDefault(u => u.id == id);
            _db.formulario_tipo.Remove(tipo);
            _db.SaveChanges();
            return;
        }

        public Formulario_tipo Update(Formulario_tipo item)
        {
            _db.formulario_tipo.Update(item);
            _db.SaveChanges();
            return item;
        }

        public bool Validate(Formulario_tipo item)
        {
            throw new NotImplementedException();
        }
    }
}
