<Query Kind="Program">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

/*
  - Incorporamos el ordenamiento de ItemVenta ascendente
  - Agregamos un metodo de clase (static) que nos permite construir nuevos libros en base a 
  una cadena, que deberia respetar un formato
*/

string DB_Libros = @"
  El Aleph | 169 | Jorge Luis Borges | La Urraca | 123-7897888979-88
  Rayuela | 250 | Julio Cortazar | Critica | 123-7897888979-88
  Ficciones | 220E34 | Jorge Luis Borges | Losada | 123-7897884623-99
  Sobre Heroes y Tumbas | 350 | Ernesto Sabato | Alfaguara | 123-7897888979-88
  The C Programming Language | 350 | Brian Kernighan, Ritchie | Addison Wesley | 123-7897888979-88
  Beginning Programming for Dummies | 280 | Wallace Wang | Addison Wesley | 123-7897888979-88
";

string DB_Editoriales = @"
  La Urraca | Calle XXX 123 
  Critica | Calle YYY 444
  Alfaguara | Calle ZZZ 8888
  Addison Wesley | Calle NNN 321
";

string DB_Autores = @"
  Jorge Luis Borges | Argentino | Loren ipsum | --
  Julio Cortazar | Argentino | Loren ipsum | --
  Ernesto Sabato | Argentino | Loren ipsum | --
  Brian Kernighan | Canada | dolor sit | bkerni@bell.com
  Dennis Ritchie | USA | dolor sit | --
  Wallace Wang | USA | dolor sit | --
";

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
  
  //  Nos retorna un nuevo libro a partir de una cadena con formato determinado
  //  Si la cadena no guarda dicho formato, retorna un valor null
  //
  public static Libro CrearDesdeString(string linea) 
  {
    string[] prod = linea.Split('|');
    decimal tmpPrecio;
    
    Libro newLibro = new Libro(prod[0].Trim());   //  titulo
    
    if (!decimal.TryParse(prod[1].Trim(), out tmpPrecio))
      return null;
      
    newLibro.Precio = tmpPrecio;  
    newLibro.Autor = prod[2].Trim();
    newLibro.Editorial = prod[3].Trim();
    newLibro.ISBN = prod[4].Trim();
    
    return newLibro;
  }
}

class ItemVenta : IComparable<ItemVenta>
{
  public Libro Item { get; set; }
  public int Cantidad {get; set; }
  
  public int CompareTo(ItemVenta otro)
  {
    //  primero calculamos los precios totales
    decimal precioThis = this.Cantidad * this.Item.Precio;
    decimal precioOtro = otro.Cantidad * otro.Item.Precio;
    
    if (precioThis < precioOtro)
      return -1;
    else
    {
      if (precioThis == precioOtro)
        return 0;
      else
        return +1;
    }
  }
}

class Editorial
{
  public string Nombre { get; set; }
  public string Direccion { get; set; }
}

class Venta
{
  private List<ItemVenta> _detalle ;

  public DateTime Fecha { get; set; }

  public ReadOnlyCollection<ItemVenta> Detalle
  { 
    get { return new ReadOnlyCollection<ItemVenta>(_detalle); }
  }
  
  public Venta() 
  {
    Fecha = DateTime.Now;
    _detalle = new List<ItemVenta>();
  }
  
  public void AgregarLibro(Libro book, int cantidad)
  {
    _detalle.Add(new ItemVenta { Item = book, Cantidad = cantidad});
  }
  
  public decimal Costo 
  { 
    get 
    {
      decimal result = 0.0M;
      
      foreach (ItemVenta iv in _detalle)
        result += (iv.Cantidad * iv.Item.Precio);
        
      return result;
    } 
  }

  /*
    Imprime la venta con los items ordenados de forma ascendente de precio total
    Usamos StringBuilder que es mas eficiente cuando se trata de concatenar cadenas
  */
  public void Mostrar()
  {
    StringBuilder ticket = new StringBuilder();

    ticket
      .AppendLine("===================================================================")
      .AppendFormat("   Venta del dia {0:dd/MM/yyyy HH:mm}", Fecha)
      .AppendLine()
      .AppendLine("===================================================================")
      .AppendLine("                       Detalle de la Compra")
      .AppendLine("-------------------------------------------------------------------")
      .AppendLine("Producto                          Cantidad  Precio Unit    Total")
      .AppendLine("-------------------------------------------------------------------");

    //  ordena el detalle por costo total (de menor a mayor)
    _detalle.Sort();
    
    foreach (ItemVenta iv in _detalle)
    {
      ticket.AppendFormat("{0,-35} {1, 5} {2,10:C} {3,14:C}", iv.Item.Titulo, 
        iv.Cantidad, iv.Item.Precio, iv.Cantidad * iv.Item.Precio).AppendLine();
    }
    
    ticket
      .AppendLine("-------------------------------------------------------------------")
      .AppendFormat("  TOTAL: {0,58:C}", Costo)
      .AppendLine()
      .AppendLine("-------------------------------------------------------------------");
    Console.WriteLine (ticket.ToString());
  }
}

void Main()
{
  Venta ventaActual = new Venta();

  string[] lineas = DB_Libros.Split(new char[] {'\n','\r'}, 
    StringSplitOptions.RemoveEmptyEntries);
  
  foreach (string linea in lineas) 
  {
    Libro lib = Libro.CrearDesdeString(linea);
    
    if (lib != null )
      ventaActual.AgregarLibro(lib, 4);
  }
  //  observar que no se considera el libro Ficciones
  ventaActual.Mostrar();
}