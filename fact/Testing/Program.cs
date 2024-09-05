
using fact.Dominio;
using fact.Servicios;

TiendaManager manager = new TiendaManager();

////TRAIGO LOS DETALLES

//List<DetalleFactura> det = manager.GetDetalle();

//foreach (var dets in det)
//{
//    Console.WriteLine(dets.ToString());
//}
////TRAIGO UN DETALLE    

//DetalleFactura detalleFacturaq = manager.GetDetalleByID(2);

//Console.WriteLine(detalleFacturaq.ToString());


////-----------------------------------------------------------------------------------------------------------------------


////TRAIGO UN articulo      -------------------------------------   

//Articulo articulo = manager.GetArticuloByID(2);

//Console.WriteLine(articulo.ToString());


//TRAIGO LOS ARTICULOS    --------------------------------------

List<Articulo> ART = manager.GetArticulos();

foreach (var A in ART)
{
    Console.WriteLine(A.ToString());
}

////CREAR UN ARTICULO       --------------------------------------

//var art1 = new Articulo
//{
//    Nombre = "Polenta",
//    PrecioUnitario = 333,
//};

//var articuloNuevo = manager.New_Articulo(art1);
//if (articuloNuevo)
//{
//    Console.WriteLine(art1.ToString());
//}


////-----------------------------------------------------------------------------------------------------------------------


//Formas de pago
//Traigo las formas de pago

List<FormaPago> forms = manager.GetFormasPago();

foreach (FormaPago form in forms)
{
    Console.WriteLine(form.ToString());
}


////-----------------------------------------------------------------------------------------------------------------------


////Cargar 1 Facturas

////traer ls fcturas
//Factura factura = manager.GetFacturaByID(8);

//Console.WriteLine(factura.ToString());



////Cargar Facturas
////traer ls fcturas
//List<Factura> facturas = manager.GetFacturas();
//foreach (Factura factura2 in facturas)
//{
//    Console.WriteLine(factura2.ToString());
//}

//////Crear una factura

var fac2 = new Factura
{
    Fecha = DateTime.Now,
    FormaPago = forms[2],
    Cliente = "Cruz Franco",
    DetalleFacturas = new List<DetalleFactura>
    {
        new DetalleFactura(ART[4], 1, 104),
        new DetalleFactura(ART[4], 3, 104),
        new DetalleFactura(ART[6], 1, 170),
        new DetalleFactura(ART[2], 1, 10),
        new DetalleFactura(ART[3], 8, 100)
    }

};

var facnueva = manager.SaveFactura(fac2);
if (facnueva)
{
    Console.WriteLine(fac2.ToString());
}
