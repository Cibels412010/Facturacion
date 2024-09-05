using fact.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fact.Datos.Repository.Contracts
{
    internal interface IArticuloRepository
    {
        List<Articulo> GetAll();
        Articulo GetById(int id);
       
        bool Save(Articulo articulo);
    }
}
