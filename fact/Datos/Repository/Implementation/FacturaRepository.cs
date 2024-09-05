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
    public class FacturaRepository : IFacturaRepository
    {
        private DetalleRepository detRep = new DetalleRepository();
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Factura> GetAll()
        {
            string sp = "SP_RECUPERAR_FACTURAS";
            List<ParameterSQL> param = null;
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery(sp, param);
            

            return ReadFacturas(dt);
        }

        List<Factura> ReadFacturas(DataTable dt)
        {
            List<Factura> facturas = new List<Factura>();
            Factura? factura = null;
            int? currentFacturaId = null;

            foreach (DataRow dr in dt.Rows)
            {
                int nroFactura = (int)dr[0];  

                if (factura == null || currentFacturaId != nroFactura)
                {
                    if (factura != null)
                    {
                        facturas.Add(factura);
                    }

                    FormaPago formaPago = new FormaPago
                    {
                        IdFormaPago = (int)dr[2]  
                    };

                    factura = new Factura()
                    {
                        NroFactura = nroFactura,
                        Fecha = Convert.ToDateTime(dr[1]),  
                        FormaPago = formaPago,
                        Cliente = dr[3].ToString(),  
                    };
                    currentFacturaId = nroFactura;

                    //no me trae los detalles por el nro de factura

                    List<DetalleFactura> detalles = detRep.GetAllByFactura(nroFactura);
                    foreach (var det in detalles)
                    {
                        factura.AddDetail(det);
                    }
                }

            }

            // Añado la última factura si no se agregó ya
            if (factura != null)
            {
                facturas.Add(factura);
            }

            return facturas;
        }


        



        public Factura GetById(int id)
        {
            string sp = "SP_RECUPERAR_FACTURAS_POR_CODIGO";
            List<ParameterSQL> param = new List<ParameterSQL>() { new ParameterSQL("@NRO_FACTURA", id) };
            DataTable dt = DataHelper.GetInstance().ExecuteSPQuery(sp, param);
            
            List<Factura> facturas = ReadFacturas(dt);
            return facturas.FirstOrDefault();
        }

        

        public bool Save(Factura factura)
        {
            bool guardado = true;
            SqlTransaction? transaction = null;
            SqlConnection? connection = null;
            string spFactura = "SP_INSERTAR_FACTURA";

            try
            {
                connection = DataHelper.GetInstance().GetConnection();
                connection.Open();
                transaction = connection.BeginTransaction();

                var command = new SqlCommand(spFactura, connection, transaction);
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@fecha", factura.Fecha);
                command.Parameters.AddWithValue("@id_forma_pago", factura.FormaPago.IdFormaPago);
                command.Parameters.AddWithValue("@cliente", factura.Cliente);

                SqlParameter param = new SqlParameter("@nro_factura", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                command.Parameters.Add(param);
                command.ExecuteNonQuery();

                int FacturaId = (int)param.Value;

                int nroDetail = 1;
                foreach (var detail in factura.GetDetails())
                {
                    string spVerificar = "SP_VERIFICAR_DETALLE_EXISTENTE";
                    var cmdVerificar = new SqlCommand(spVerificar, connection, transaction);
                    cmdVerificar.CommandType = CommandType.StoredProcedure;
                    cmdVerificar.Parameters.AddWithValue("@NRO_FACTURA", FacturaId);
                    cmdVerificar.Parameters.AddWithValue("@ID_ARTICULO", detail.IdArticulo.IdArticulo);

                    SqlParameter paramVerificar = new SqlParameter("@EXISTS", SqlDbType.Bit);
                    paramVerificar.Direction = ParameterDirection.Output;
                    cmdVerificar.Parameters.Add(paramVerificar);
                    cmdVerificar.ExecuteNonQuery();

                    bool exist = (bool)paramVerificar.Value;

                    if (exist)
                    {
                        string spActualizarCantidad = "SP_ACTUALIZAR_CANTIDAD_DETALLE";

                        var cmdActualizar = new SqlCommand(spActualizarCantidad, connection, transaction);
                        cmdActualizar.CommandType = CommandType.StoredProcedure;
                        cmdActualizar.Parameters.AddWithValue("@NRO_FACTURA", FacturaId);
                        cmdActualizar.Parameters.AddWithValue("@ID_ARTICULO", detail.IdArticulo.IdArticulo);
                        cmdActualizar.Parameters.AddWithValue("@CANTIDAD", detail.Cantidad);
                        cmdActualizar.ExecuteNonQuery();
                    } else
                    {
                        string spAgregarDetalle = "SP_INSERTAR_DETALLE";
                        var cmdDetail = new SqlCommand(spAgregarDetalle, connection, transaction);
                        cmdDetail.CommandType = CommandType.StoredProcedure;
                        cmdDetail.Parameters.AddWithValue("@NRO_FACTURA", FacturaId);
                        cmdDetail.Parameters.AddWithValue("@id_detalle", nroDetail);
                        cmdDetail.Parameters.AddWithValue("@ID_ARTICULO", detail.IdArticulo.IdArticulo);
                        cmdDetail.Parameters.AddWithValue("@cantidad", detail.Cantidad);
                        cmdDetail.Parameters.AddWithValue("@precio", detail.Precio);
                        cmdDetail.ExecuteNonQuery();
                        nroDetail++;

                    }


                }

                transaction.Commit();
            }
            catch (SqlException)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                guardado = false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return guardado;
        }
    }
}
