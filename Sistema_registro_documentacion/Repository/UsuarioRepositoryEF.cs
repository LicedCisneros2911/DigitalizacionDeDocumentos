using Microsoft.EntityFrameworkCore;
using Sistema_registro_documentacion.Data;
using Sistema_registro_documentacion.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sistema_registro_documentacion.Repository
{
    public class UsuarioRepositoryEF : IGenericRepository<Usuario>
    {
        private readonly ApplicationDBContext _db;

        public UsuarioRepositoryEF(ApplicationDBContext db)
        {
            _db = db;
        }


        public Usuario Add(Usuario item)
        {
            _db.usuario.Add(item);
            _db.SaveChanges();
            return item;
        }

        public List<Usuario> Filter(List<string> param)
        {
            return null;
        }
        public Usuario Find(int id)
        {
            return _db.usuario.FirstOrDefault(u => u.id == id);
        }

        public List<Usuario> GetAll()
        {
            return _db.usuario.ToList();
        }

        public void Remove(int id)
        {
            Usuario usuario = _db.usuario.FirstOrDefault(u => u.id == id);
            _db.usuario.Remove(usuario);
            _db.SaveChanges();
            return;
        }

        public Usuario Update(Usuario usuario)
        {
            _db.usuario.Update(usuario);
            _db.SaveChanges();
            return usuario;
        }
        public bool Validate(Usuario usr)
        {
            Usuario usu = new Usuario();
            usu = _db.usuario.AsNoTracking().FirstOrDefault(u => u.id == usr.id);
            if (usu != null)
            {
                if (!usu.usuario.Equals(usr.usuario))
                {
                    Usuario usu2 = _db.usuario.AsNoTracking().FirstOrDefault(u => u.usuario == usr.usuario);
                    if (usu2 != null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                Usuario usu2 = _db.usuario.AsNoTracking().FirstOrDefault(u => u.usuario == usr.usuario);
                if (usu2 != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
