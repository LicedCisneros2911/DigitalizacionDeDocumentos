using Sistema_registro_documentacion.Data;
using Sistema_registro_documentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Repository
{
    public class LoginRepositoryEF : IGenericRepository<Login>
    {
        private readonly ApplicationDBContext _db; //coneccion

        public LoginRepositoryEF(ApplicationDBContext db)
        {
            _db = db;
        }
        public List<Login> Filter(List<string> param)
        {
            Usuario usuarioList = new Usuario();
            usuarioList = _db.usuario.SingleOrDefault(x => x.usuario.Equals(param[0]) && x.password.Equals(param[1]));
            List<Login> loginList = new List<Login>();
            if (usuarioList != null)
            {
                loginList.Add(new Login
                {
                    usuario = usuarioList.usuario,
                    password = usuarioList.password,
                    rol = usuarioList.rol
                });
            }      
            
            return loginList;
        }
        public Login Add(Login item)
        {
            return null;
        }
     
        public Login Find(int id)
        {
            return null;
        }

        public List<Login> GetAll()
        {
            return null;
        }

        public void Remove(int id)
        {          
            return;
        }

        public Login Update(Login item)
        {
           
            return null;
        }

        public bool Validate(Login item)
        {
            throw new NotImplementedException();
        }
    }
}
