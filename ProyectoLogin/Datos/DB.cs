using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidades;

namespace Datos
{
  public static class DB
  {
    private static List<Persona> _personas = new List<Persona>()
    {
      new Persona() { Apellido = "Simpson", Nombre = "Homer" , CorreoElectronico = "hsimpson@pp.com" },
      new Persona() { Apellido = "Simpson", Nombre = "Abraham" , CorreoElectronico = "asimpson@pp.com" },
      new Persona() { Apellido = "Burns", Nombre = "Montgomery" , CorreoElectronico = "mburns@central.com" },
      new Persona() { Apellido = "Bouvier", Nombre = "Marge" , CorreoElectronico = "mbouvier@pp.com" }
    };

    private static List<Usuario> _usuarios = new List<Usuario>()
    {
      new Usuario() { Login = "hsimpson", FechaExpiracionPassword =  DateTime.Now.AddDays(30), Persona = _personas[0] },
      new Usuario() { Login = "mburns", FechaExpiracionPassword =  DateTime.Now.AddDays(60), Persona = _personas[2] },
      new Usuario() { Login = "mbouvier", FechaExpiracionPassword =  DateTime.Now.AddDays(45), Persona = _personas[3] }
    };

    private static string[] _passwords = { "123456", "monty", "ringo" };

    public static bool LoginUsuario(Usuario usr, string pass)
    {
      int index = _usuarios.IndexOf(usr);

      return (_passwords[index] == pass);
    }

    public static List<Usuario> Usuarios { get { return _usuarios;  } }
  }
}
