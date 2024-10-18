using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sistema_registro_documentacion.Models;
namespace Sistema_registro_documentacion.Repository
{
    public interface IDocumentoRepository
    {
        Documento Find(int id); //encontrar
        List<Documento> GetDocumentos(); //listar
        Documento Update(Documento documento); //actualizar
        void Remove(int id); //borrar
        Documento Add(Documento documento); //añadir
        List<Documento> Filter(string tipo, string fecha); //filtro
    }
}
