<Query Kind="Program">
  <Namespace>System.Collections.ObjectModel</Namespace>
</Query>

/*
  Posible solucion para el desarrollo de una sencilla interface de usuario que permita
  generar nuevas ventas para la tienda Oh My Book!
*/

static string DB_Libros = @"
  El Aleph | 169 | Jorge Luis Borges | La Urraca | 123-7897888979-88
  Rayuela | 250 | Julio Cortazar | Critica | 123-7897888979-88
  Ficciones | 220,45 | Jorge Luis Borges | La Urraca | 123-7897884623-99
  Sobre Heroes y Tumbas | 350 | Ernesto Sabato | Alfaguara | 123-7897888979-88
  The C Programming Language | 350 | Brian Kernighan, Ritchie | Addison Wesley | 123-7897888979-88
  Beginning Programming for Dummies | 280 | Wallace Wang | Addison Wesley | 123-7897888979-88
";

static public string DB_Editoriales = @"
  La Urraca | Calle XXX 123 | Argentina
  Critica | Calle YYY 444 | Argentina
  Alfaguara | Calle ZZZ 8888 | España
  Addison Wesley | Calle NNN 321 | USA
";

static string DB_Autores = @"
  Jorge Luis Borges | Argentino | Loren ipsum | 
  Julio Cortazar | Argentino | Loren ipsum | 
  Ernesto Sabato | Argentino | Loren ipsum | 
  Brian Kernighan | Canada | dolor sit | bkerni@bell.com
  Dennis Ritchie | USA | dolor sit | 
  Wallace Wang | USA | dolor sit |
";

class Libro
{
  public string ISBN { get; set; }
  public string Titulo { get; set; }
  public Editorial Editorial { get; set; }
  public List<Autor> Autor { get; set; }
  public decimal Precio { get; set; }
  
  public Libro(string titulo) 
  {
    Autor = new List<Autor>();
    Titulo = titulo;
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
  public string Pais { get; set; }
  
  public Editorial(string nombre)
  {
    Nombre = nombre;
  }
  
  public static Editorial CrearDesdeString(string linea)
  {
    string[] prod = linea.Split('|');
    
    Editorial newEdit = new Editorial(prod[0].Trim());   //   nombre
    
    newEdit.Direccion = prod[1].Trim();
    newEdit.Pais = prod[2].Trim();
    
    return newEdit;
  }
}

class Autor
{
  public string Nombre { get; set; }
  public string Nacionalidad { get; set; }
  public string Biografia { get; set; }
  public string Correo { get; set; }
  
  public Autor(string nombre) 
  {
    Nombre = nombre;
  }
  
  public static Autor CrearDesdeString(string linea)
  {
    string[] prod = linea.Split('|');
    
    Autor newAut = new Autor(prod[0].Trim());   //   nombre
    
    newAut.Nacionalidad = prod[1].Trim();
    newAut.Biografia = prod[2].Trim();
    newAut.Correo = prod[3] == null ? null : prod[3].Trim();
    
    return newAut;
  }
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

/*
  <<SINGLETON>>

  Almacena una unica instancia de DB, cada vez que se requiere se retorna esa instancia
  Esto es para que no tengamos que estar continuamente creando las listas de libros, 
  editoriales y autores (lo cual es logico si pensamos que en una base de datos real no se
  van a estar generando los datos cada vez que consultamos una tabla)
  
*/
class DB
{
  //  los hago public para que podamos verlos con Dump() pero deberian ser private...
  public List<Editorial> _Editoriales { get; set; }
  public List<Autor> _Autores { get; set; }
  public List<Libro> _Stock { get; set; }
  
  //  cadena de criterio, reutilizada para varios predicados...
  private string _criterio;
  
  private static DB _dbInstance;
  
  //
  public static DB Instancia
  {
    get
    {
      if (_dbInstance == null)
        _dbInstance = new DB();
        
      return _dbInstance;
    }
  }
  
  private DB()
  {
    string[] lineas;
    
    _Editoriales = new List<Editorial>();
    _Autores = new List<Autor>();
    _Stock = new List<Libro>();
    
    //  Creo la base de autores y editoriales a partir de las cadenas del inicio
    lineas = DB_Autores.Split(new char[] {'\n','\r'}, StringSplitOptions.RemoveEmptyEntries);
    
    foreach (string linea in lineas) 
    {
      Autor aut = Autor.CrearDesdeString(linea);
      
      if (aut != null)
        _Autores.Add(aut);
    }

    lineas = DB_Editoriales.Split(new char[] {'\n','\r'}, StringSplitOptions.RemoveEmptyEntries);
    
    foreach (string linea in lineas) 
    {
      Editorial edit = Editorial.CrearDesdeString(linea);
      
      if (edit != null)
        _Editoriales.Add(edit);
    }
    
    //  Creo la base de libros a partir de la cadena del inicio
    //  No puedo extraer este codigo para hacerlo un metodo de clase en Libro
    //  porque depende de las colecciones de Autor y Editorial
    //
    lineas = DB_Libros.Split(new char[] {'\n','\r'}, StringSplitOptions.RemoveEmptyEntries);
    
    foreach (string linea in lineas) 
    {
      string[] prod = linea.Split('|');
      decimal tmpPrecio;
      
      Libro newLibro = new Libro(prod[0].Trim());   //  titulo
      
      if (!decimal.TryParse(prod[1].Trim(), out tmpPrecio))
        continue;
        
      newLibro.Precio = tmpPrecio;  
      newLibro.Editorial = GetEditorialBynombre(prod[3].Trim());
      newLibro.ISBN = prod[4].Trim();
      
      //  obtengo los autores, por si el libro tiene mas de uno..
      string[] autores = prod[2].Split(',');
      
      foreach (string aut in autores)
      {
        Autor autor = GetAutorByNombre(aut.Trim());
        
        if (autor != null)
          newLibro.Autor.Add(autor);
      }
      
      _Stock.Add(newLibro);
    }
  }
  
  //  La verdadera interface publica...
  
  public List<Libro> GetLibrosByCriterio(string criterio)
  {
    _criterio = criterio;
    return _Stock.FindAll(Predicate_BuscaLibro);
  }
  
  public Autor GetAutorByNombre(string nombre)
  {
    _criterio = nombre;
    return _Autores.Find(Predicate_BuscaAutor);
  }
  
  public Editorial GetEditorialBynombre(string nombre)
  {
    _criterio = nombre;
    return _Editoriales.Find(Predicate_BuscaEditorial);
  }
  
  //  Metodos privados para usar como predicados...
  //  No forman parte de ningun comportamiento de la clase!!
  
  private bool Predicate_BuscaAutor(Autor autor)
  {
    if (autor.Nombre.Contains(_criterio))
      return true;
      
    return false;
  }

  private bool Predicate_BuscaEditorial(Editorial edit)
  {
    return (edit.Nombre.Contains(_criterio));
  }
  
  private bool Predicate_BuscaLibro(Libro libro)
  {
    //  aca tengo varias opciones pero voy a considerar dos: titulo y autor
    if (libro.Titulo.Contains(_criterio))
      return true;
    
    //  Para el autor, tengo que considerar todos los autores que pueda tener el libro
    //  por lo tanto tendre que usar el _criterio para analizar List<Autor>
    //  Por suerte me sirve el mismo predicado que utilizo en GetAutorByNombre()
    
    return libro.Autor.Find(Predicate_BuscaAutor) != null;
  }
}

void Main()
{
  Venta ventaActual = new Venta();
  DB database = DB.Instancia;
  string criterio, strCantidad;
  int cantidad;
  Libro libro;
  
  //  vemos lo que tenemos
  database.Dump();

  //  armamos una venta agregando libros por criterio de busqueda...
  do {
    Console.WriteLine ("Seleccionar por autor o por titulo algun libro...[Enter para salir]");
    criterio = Console.ReadLine();
    
    if (string.IsNullOrWhiteSpace(criterio))
      break;
      
    var found = database.GetLibrosByCriterio(criterio);
    
    if (found.Count > 1)
    {
      string eleccion;
      
      Console.WriteLine ("Se encontro mas de un libro para este criterio, por favor elegir...");
      
      for (int i = 0 ; i < found.Count ; i++)
        Console.WriteLine ("    {0} --> {1}", i+1, found[i].Titulo);
      
      eleccion = Console.ReadLine();
      libro = found[Int32.Parse(eleccion) - 1];   //  no chequeo nada para no escribir mas
    }
    else
    {
      if (found.Count == 0)
      {
        Console.WriteLine ("No se encontraron libros para este criterio, intente nuevamente");
        continue;
      }
      else
        libro = found.First();
    }
    
    Console.WriteLine ("Ingresar la cantidad de ejemplares para {0} [0 para cancelar la entrada]", libro.Titulo);
    strCantidad = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(strCantidad))
      cantidad = 1;
    else
    {
      if (!Int32.TryParse(strCantidad, out cantidad))
        cantidad = 1;
    }
    
    if (cantidad != 0)
      ventaActual.AgregarLibro(libro, cantidad);
  } while (true);
  
  ventaActual.Mostrar();
}