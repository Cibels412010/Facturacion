using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fact.Dominio
{
    public class DetalleFactura
    {
        public int IdDetalle { get; set; }
        public int NroFactura { get; set; }
        public Articulo IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        internal decimal SubTotal()
        {

        return Cantidad * Precio;
        }

        public DetalleFactura(Articulo art, int cant, decimal precio)
        {
            this.IdArticulo = art;
            this.Cantidad = cant;
            this.Precio = precio;
        }

        public DetalleFactura()
        {
                
        }
        public override string ToString()
        {
            return "\nId Detalle: " + IdDetalle + "\nNro Factura: " + NroFactura;
        }
    }

}
