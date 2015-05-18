<Query Kind="Program">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

/*
  - Modelar las entidades Libro, Venta e ItemVenta 
  - Permitir agregar libros a la venta
  - Permitir calcular el precio total de la Venta
*/

#define VERSION_3

#if VERSION_1
  const string Dump_Msg = "Calculo Costo mediante el uso de foreach";
#else
  #if VERSION_2
    const string Dump_Msg = "Calculo Costo usando un delegado y un metodo concreto";
  #else
    #if VERSION_3
      const string Dump_Msg = "Calculo Costo usando una expresion lambda";
    #endif
  #endif
#endif

class Libro
{
  public string ISBN { get; set; }
  public string Titulo { get; set; }
  public string Editorial { get; set; }
  public string Autor { get; set; }
  public decimal Precio { get; set; }
  
  public Libro()  {}
  
  public Libro(string titulo) 
  {
    Titulo = titulo;
  }
}

class ItemVenta
{
  public Libro Item { get; set; }
  public int Cantidad {get; set; }
}

class Venta
{
  private List<ItemVenta> _detalle ;
  private ReadOnlyCollection<ItemVenta> _detalleCache;

  public DateTime Fecha { get; set; }

  public ReadOnlyCollection<ItemVenta> Detalle
  { 
    get 
    { 
      //  Usamos un cache para no tener que estar creando la coleccion RO cada vez
      //  que necesitamos un Detalle
      if (_detalleCache == null)
        _detalleCache = new ReadOnlyCollection<ItemVenta>(_detalle); 
        
      return _detalleCache;
    }
  }
  
  public Venta() 
  {
    _detalle = new List<ItemVenta>();
    Fecha = DateTime.Now;
  }
  
  public void AgregarLibro(Libro book, int cantidad)
  {
    _detalle.Add(new ItemVenta { Item = book, Cantidad = cantidad});
    
    //  Si agregamos un libro a la coleccion, tenemos que invalidar el cache
    _detalleCache = null;
  }
  
  public decimal Costo 
  { 
    get 
    {
      //  Version 1: modo tradicional
#if VERSION_1
      decimal result = 0.0M;
      
      foreach (ItemVenta iv in _detalle)
        result += (iv.Cantidad * iv.Item.Precio);
        
      return result;
#endif

      //  Version 2: con un metodo con nombre (delegado) Observar que necesito casting
#if VERSION_2
      return _detalle.Sum((Func<ItemVenta, decimal>)CalcularPrecioItemVenta);
#endif

      //  Version 3: expresion lambda (observar que es lo mismo que hay dentro del metodo
      //  CalcularPrecioItemVenta, solo que "no tiene nombre"
#if VERSION_3
      return _detalle.Sum (iv => iv.Cantidad * iv.Item.Precio);
#endif
    } 
  }
  
#if VERSION_2
  private decimal CalcularPrecioItemVenta(ItemVenta iv) 
  {
    return iv.Cantidad * iv.Item.Precio;
  }
#endif
}

void Main()
{
  Venta ventaActual = new Venta();

  Libro lib = new Libro("El Aleph") { Autor = "Jorge Luis Borges", Precio = 180 };
  ventaActual.AgregarLibro(lib, 3);
  
  lib = new Libro("No habra mas penas ni olvidos") { Autor = "Osvaldo Soriano", Precio = 240 };
  ventaActual.AgregarLibro(lib, 10);

  ventaActual.Dump(Dump_Msg);
}