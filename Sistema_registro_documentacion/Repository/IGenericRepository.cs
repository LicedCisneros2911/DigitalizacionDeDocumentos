using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_registro_documentacion.Repository
{
    public interface IGenericRepository <T>
    {
        T Find(int id); //encontrar
        List<T> GetAll(); //listar
        T Update(T item); //actualizar
        void Remove(int id); //borrar
        T Add(T item); //añadir
        List<T> Filter(List <string> param ); //filtro
        bool Validate (T item); //Validar si ya existe en la base de datos
    }
}
