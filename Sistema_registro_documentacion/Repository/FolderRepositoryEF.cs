using Microsoft.EntityFrameworkCore;
using Sistema_registro_documentacion.Data;
using Sistema_registro_documentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Repository
{
    public class FolderRepositoryEF : IGenericRepository<Formulario_folder>
    {
        private readonly ApplicationDBContext _db;
        public FolderRepositoryEF(ApplicationDBContext db)
        {
            _db = db;
        }

        public Formulario_folder Add(Formulario_folder item)
        {
            _db.formulario_folder.Add(item);
            _db.SaveChanges();
            return item;
        }

        public List<Formulario_folder> Filter(List<string> param)
        {
            throw new NotImplementedException();
        }

        public Formulario_folder Find(int id)
        {
            return _db.formulario_folder.FirstOrDefault(t => t.id == id);
        }

        public List<Formulario_folder> GetAll()
        {
            List<Formulario_folder> folder = new List<Formulario_folder>();
            folder = _db.formulario_folder.ToList();
            return folder;
        }

        public void Remove(int id)
        {
            Formulario_folder folder = _db.formulario_folder.FirstOrDefault(u => u.id == id);
            _db.formulario_folder.Remove(folder);
            _db.SaveChanges();
            return;
        }

        public Formulario_folder Update(Formulario_folder item)
        {
            _db.formulario_folder.Update(item);
            _db.SaveChanges();
            return item;
        }

        public bool Validate(Formulario_folder item)
        {
            Formulario_folder doc = new Formulario_folder();
            doc = _db.formulario_folder.AsNoTracking().FirstOrDefault(u => u.id == item.id);
            if (doc != null)
            {
                if (!doc.nombre_folder.Equals(item.nombre_folder))
                {
                    Formulario_folder doc2 = _db.formulario_folder.AsNoTracking().FirstOrDefault(u => u.nombre_folder == item.nombre_folder);
                    if (doc2 != null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                Formulario_folder doc2 = _db.formulario_folder.AsNoTracking().FirstOrDefault(u => u.nombre_folder == item.nombre_folder);
                if (doc2 != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
