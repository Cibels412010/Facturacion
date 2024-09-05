using fact.Datos.Repository.Contracts;
using fact.Datos.Utils;
using fact.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fact.Datos.Repository.Implementation
{
    public class DetalleRepository : IDetalleRepository
    {
        public List<DetalleFactura> GetAll()
        {
            string sp = "SP_RECUPERAR_DETALLES_PRESUPUESTOS";

            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery(sp, null);


            return GetDetails(dt);

        }
        public List<DetalleFactura> GetAllByFactura(int nro)
        {
            string sp = "SP_RECUPERAR_DETALLES_POR_NRO_FACTURA";
            List<ParameterSQL> param = new List<ParameterSQL>()
            {
                new ParameterSQL("@NRO_FACTURA", nro),
            };

            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery(sp, param);

            return GetDetails(dt);

        }

        private List<DetalleFactura> GetDetails(DataTable dt)
        {
            
                List<DetalleFactura> detalleFacturas = new List<DetalleFactura>();
                DetalleFactura detalle = new DetalleFactura();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                                detalle.IdDetalle = (int) dr[0];
                                detalle.NroFactura = (int) dr[1];

                                detalle.IdArticulo = new Articulo
                                {
                                IdArticulo = (int) dr[2],

                                };
                                detalle.Cantidad = (int) dr[3];
                                detalle.Precio = (decimal) dr[4];

                                detalleFacturas.Add(detalle);
                        }
                    }

                    return detalleFacturas;
        }

        public DetalleFactura GetById(int id)
        {
            string sp = "SP_RECUPERAR_DETALLES_POR_CODIGO";
            List<ParameterSQL> param = new List<ParameterSQL>()
            {
                new ParameterSQL("@ID_DETALLE", id),
            };

            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery(sp, param);


           return ReadDetail(dt);
        }

        

        public DetalleFactura ReadDetail(DataTable dt)
        {
           
            DetalleFactura detalle = new DetalleFactura();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    detalle.IdDetalle = (int)dr[0];
                    detalle.NroFactura = (int)dr[1];

                    detalle.IdArticulo = new Articulo
                    {
                        IdArticulo = (int)dr[2],

                    };
                    detalle.Cantidad = (int)dr[3];
                    detalle.Precio = (decimal)dr[4];
                }
            }
            return detalle;
        }

        
    }

}
