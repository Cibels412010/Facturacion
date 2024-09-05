using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace fact.Dominio
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }

        public override string ToString()
        {
            return "\nId de Articulo: " + IdArticulo + " - Nombre: " + Nombre + " - Precio: " + PrecioUnitario;
        }
    }
}
