using fact.Datos.Repository.Contracts;
using fact.Datos.Repository.Implementation;
using fact.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fact.Servicios
{
    internal class TiendaManager
    {
        IArticuloRepository articuloRepository;
        IDetalleRepository detalleRepository;
        IFacturaRepository facturaRepository;
        IFormaPagoRepository formaPagoRepository;

        public TiendaManager()
        {
            articuloRepository = new ArticuloRepository();
            facturaRepository = new FacturaRepository();
            detalleRepository = new DetalleRepository();
            formaPagoRepository = new FormaPagoRepository();

        }

        //articulos
        public List<Articulo> GetArticulos()
        {
            return articuloRepository.GetAll();
        }
        public Articulo GetArticuloByID(int id)
        {
            return articuloRepository.GetById(id);
        }
        public bool New_Articulo(Articulo art)
        {
            return articuloRepository.Save(art);
        }

        //formas de pgo
        public List<FormaPago> GetFormasPago() { return  formaPagoRepository.GetAll(); }

        //detalles
        public List<DetalleFactura> GetDetalle() { return detalleRepository.GetAll(); }
        public DetalleFactura GetDetalleByID(int id) {return detalleRepository.GetById(id);}

        //facturas

        public List<Factura> GetFacturas() { return facturaRepository.GetAll(); }
        public Factura GetFacturaByID(int id) { return facturaRepository.GetById(id); }
        public bool SaveFactura(Factura factura) {  return facturaRepository.Save(factura);}

    }
}
