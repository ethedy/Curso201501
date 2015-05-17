<Query Kind="Program">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

string DB_Libros = @"
  El Aleph | 169 | Jorge Luis Borges | La Urraca | 123-7897888979-88
  Rayuela | 250 | Julio Cortazar | Critica | 123-7897888979-88
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
  //  public Editorial Editorial { get; set; }
  
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
}

void Main()
{
  Venta ventaActual = new Venta();
  string[] lineas = DB_Libros.Split(new char[] {'\n','\r'}, 
    StringSplitOptions.RemoveEmptyEntries);
  
  //  lineas.Dump();
  
  foreach (var linea in lineas)
  {
    string[] prod = linea.Split('|');
    
    Libro newLibro = new Libro(prod[0].Trim());   //  titulo
    
    newLibro.Precio = decimal.Parse(prod[1]);
    newLibro.Autor = prod[2].Trim();
    newLibro.Editorial = prod[3].Trim();
    newLibro.ISBN = prod[4].Trim();
    
    ventaActual.AgregarLibro(newLibro, 3);    //  siempre 3 libros de cada uno...
  }
  ventaActual.Dump();
}