using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fact.Dominio
{
    public class FormaPago
    {
        public int IdFormaPago { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return "\nId: " + IdFormaPago + " - Forma de pago: " + Descripcion;
        }
    }
}
