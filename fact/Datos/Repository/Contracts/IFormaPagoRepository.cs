using fact.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fact.Datos.Repository.Contracts
{
    internal interface IFormaPagoRepository
    {
        List<FormaPago> GetAll();
    }
}
