using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fact.Dominio
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public string Cliente { get; set; }

        public List<DetalleFactura> DetalleFacturas { get; set; }

        public List<DetalleFactura> GetDetails()
        {

        return DetalleFacturas; }

        public Factura()
        {
               DetalleFacturas = new List<DetalleFactura>();
        }

        public void AddDetail(DetalleFactura detail)
        {
            if (detail != null)
                DetalleFacturas.Add(detail);
        }

        public void Remove(int indice)
        {
            DetalleFacturas.RemoveAt(indice);
        }

        public decimal Total()
        {
            decimal total = 0;
            foreach (var detail in DetalleFacturas)
                total += detail.SubTotal();

            return total;
        }


        public override string ToString()
        {
            return "\nNúmero de factura: " + NroFactura + " - Cliente: " + Cliente + " \n- Cantidad de Detalles: " + DetalleFacturas.Count + "\n Total Gastado: " + Total();
        }
    }
}
