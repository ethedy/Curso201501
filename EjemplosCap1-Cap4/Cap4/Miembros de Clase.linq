<Query Kind="Program" />

/*
  Demostracion del uso de miembros de clase
  Como agregado, usamos this como argumento para agregar el usuario actual a la lista
*/

public class Usuario
{
  //  mantiene una lista de los usuarios que se crearon...
	public static List<Usuario> Usuarios { get; private set; }
  
  //  campo de clase, lo usamos en el predicado CompararUsuarioPorlogin para saber el valor
  //  de la cadena que tengo que usar para la comparacion del predicado
  private static string _CriterioLogin ;
  
  //  constructor static ==> inicializa campos o propiedades static (de clase)
  static Usuario()
  {
    Usuarios = new List<Usuario>();
  }

  //  metodo de clase ==> solo puede acceder a miembros de clase (static)
  public static void Desconectar(string login) 
  {
    //  colocamos el criterio de busqueda en un campo privado (ya veremos como solucionar esto)
    _CriterioLogin = login;

    //  primero tengo que encontrar la instancia de usuario que cumple con la condicion 
    //  Login == login (predicado)
    Usuario usrRemover = Usuarios.Find(CompararUsuarioPorLogin);
    
    if (usrRemover != null)
      Usuarios.Remove(usrRemover);
  }
  
  //  el Predicate<Usuario> que necesitamos para procesar cada usuario de la lista y ver
  //  si la propiedad Login del mismo coincide con el criterio que se la pasa a Desconectar
  //
  //  **** PREGUNTA: Por que necesitamos un campo adicional para guardar el criterio?
  //
  private static bool CompararUsuarioPorLogin(Usuario usr)
  {
    return usr.Login == _CriterioLogin;
  }
  
  public string Login { get; set; }
	public string Nombre { get; set; }
	public DateTime LastLogin { get; private set; }
	
  public Usuario(string login)
  {
    Login = login;
    LastLogin = DateTime.Now;
    
    //  usamos propiedades de clase para guardar informacion que involucra a TODAS las 
    //  instancias de la clase y no a una en particular
    Usuario.Usuarios.Add(this);
    
  }
}

void Main()
{
  Usuario homero, lisa, bart, marge;
  
  homero = new Usuario("hsimpson");
  lisa = new Usuario("lsimpson");
  bart = new Usuario("bsimpson");
  marge = new Usuario("mbouvier");
  
  //  listamos los usuarios conectados
  Usuario.Usuarios.Dump("Usuarios Conectados");
  
  bart = null;
  
  //  listamos los usuarios conectados --> observar que Bart sigue conectado por mas que 
  //  localmente se puso en null
  Usuario.Usuarios.Dump("Usuarios Conectados luego de bart=null");
  
  Usuario.Desconectar("bsimpson");
  
  Usuario.Usuarios.Dump("Usuarios Conectados luego de Desconectar(\"bsimpson\")");
  
  //  intentamos deconectar un usuario que no esta conectado...
  Usuario.Desconectar("nflanders");
  
  Usuario.Usuarios.Dump("Usuarios Conectados luego de Desconectar(\"nflanders\")");
}