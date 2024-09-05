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
    internal class ArticuloRepository : IArticuloRepository

    {

        public List<Articulo> GetAll()
        {
            string sp = "SP_RECUPERAR_ARTICULOS";
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery(sp, null);
            List<Articulo> articulos = new List<Articulo>();

            if(dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    Articulo articulo = new Articulo
                    {
                        IdArticulo = (int)dr[0],
                        Nombre = dr[1].ToString(),
                        PrecioUnitario = Convert.ToDecimal(dr[2]),
                    };
                    articulos.Add(articulo);
                }
            }
            return articulos;

        }

        public Articulo GetById(int id)
        {
            string sp = "SP_RECUPERAR_ARTICULO_POR_CODIGO";
            List<ParameterSQL> parameters = new List<ParameterSQL>
            {
                new ParameterSQL("@ID_ARTICULO", id),
            };

            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery(sp, parameters);
           

            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                int Id = (int)dr[0];
                string nom = dr[1].ToString();
                decimal pre = Convert.ToDecimal(dr[2]);

                Articulo articulo = new Articulo
                {
                    IdArticulo = Id,
                    Nombre = nom,
                    PrecioUnitario = pre,
                };
                     return articulo;
                
            }

            return null;
        }

        public bool Save(Articulo articulo)
        {
            string sp = "SP_INSERTAR_ARTICULO";

            List<ParameterSQL> parameters = new List<ParameterSQL>
            {
                new ParameterSQL ("@NOMBRE", articulo.Nombre),
                new ParameterSQL ("@PRECIO_UNITARIO", articulo.PrecioUnitario),
            };

            int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML(sp, parameters);

            return filasAfectadas > 0;
        }
    }
}
