using fact.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fact.Datos.Repository.Contracts
{
    internal interface IFacturaRepository
    {
        List<Factura> GetAll();
        Factura GetById(int id);
        bool Delete(int id);
        bool Save(Factura factura);
    }
}
