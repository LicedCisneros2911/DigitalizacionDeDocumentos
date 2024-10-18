using Microsoft.EntityFrameworkCore;
using Sistema_registro_documentacion.Data;
using Sistema_registro_documentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Repository
{
    public class DocumentoRepositoryEF : IGenericRepository<Documento>
    {
        private readonly ApplicationDBContext _db; //coneccion

        public DocumentoRepositoryEF(ApplicationDBContext db)
        {
            _db = db;
        }
        public Documento Add(Documento documento)
        {
            _db.documento.Add(documento);
            _db.SaveChanges();
            return documento;
        }

        public List<Documento> Filter(List <string> param)
        {
            if (int.Parse(param[0]) == 0 && int.Parse(param[1]) == 0)
            {
                return _db.documento.Where(x => x.fecha.ToString().Contains(param[2])).ToList();
            }
            else if (int.Parse(param[0]) == 0 && int.Parse(param[1]) != 0)
            {
                return _db.documento.Where(x => x.tipo == int.Parse(param[1]) && x.fecha.ToString().Contains(param[2])).ToList() ;
            }
            else if (int.Parse(param[0]) != 0 && int.Parse(param[1]) == 0)
            {
                return _db.documento.Where(x => x.folder == int.Parse(param[0]) && x.fecha.ToString().Contains(param[2])).ToList();
            }
            else 
            {
                return _db.documento.Where(x => x.folder == int.Parse(param[0]) && x.tipo == int.Parse(param[1]) && x.fecha.ToString().Contains(param[2])).ToList();
            }
        }

        public Documento Find(int id)
        {
           return _db.documento.Include(d=>d.formulario_Tipo).Include(f=>f.formulario_Folder).FirstOrDefault(u => u.id == id);
        }

        public List<Documento> GetAll()
        {
            return _db.documento.Include(d => d.formulario_Tipo).Include(f => f.formulario_Folder).ToList();
        }

        public List<Documento> GetDocumentos()
        {
            return _db.documento.ToList();
        }

        public void Remove(int id)
        {
            Documento documento = _db.documento.FirstOrDefault(u => u.id == id);
            _db.documento.Remove(documento);
            _db.SaveChanges();
            return;
        }

        public Documento Update(Documento documento)
        {          
            _db.documento.Update(documento);
            if (documento.foto == null)
            {
                _db.Entry(documento).Property(x => x.foto).IsModified = false;
            }
            _db.SaveChanges();
            return documento;
        }

        public bool Validate(Documento documento)
        {
            Documento doc = new Documento();
            doc = _db.documento.AsNoTracking().FirstOrDefault(u => u.id == documento.id);
            if (doc != null)
            {
                if (!doc.cite.Equals(documento.cite))
                {
                    Documento doc2 = _db.documento.AsNoTracking().FirstOrDefault(u => u.cite == documento.cite);
                    if (doc2 != null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                Documento doc2 = _db.documento.AsNoTracking().FirstOrDefault(u => u.cite == documento.cite);
                if (doc2 != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
