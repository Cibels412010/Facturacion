using fact.Datos.Repository.Contracts;
using fact.Datos.Utils;
using fact.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fact.Datos.Repository.Implementation
{
    internal class FormaPagoRepository : IFormaPagoRepository
    {
        public List<FormaPago> GetAll()
        {
            string sp = "SP_RECUPERAR_FORMAS_PAGO";
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery(sp, null);
            return ReadFormaPago(dt);
        }


        private List<FormaPago> ReadFormaPago(DataTable? dt)
        {
            List<FormaPago> FormasPago = new List<FormaPago>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    FormaPago forma = new FormaPago
                    {
                        IdFormaPago = (int)dr[0],
                        Descripcion = dr[1].ToString(),
                        
                    };
                    FormasPago.Add(forma);
                }
            }
            return FormasPago;
           
        }
    }
}
