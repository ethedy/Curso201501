using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Servicios;

namespace Terminal
{
  class Program
  {
    static void Main(string[] args)
    {
      string uid, pass;

      Console.WriteLine("Bienvenido a la consola de acceso a OMB. Por favor ingrese sus credenciales...");
      Console.Write("Usuario -->  ");
      uid = Console.ReadLine();

      Console.Write("Password -->  ");
      Console.ForegroundColor = ConsoleColor.Black;
      pass = Console.ReadLine();

      Console.ResetColor();

      //  intentamos acceder al servicio de login...
      SecurityServices sec = new SecurityServices();

      Sesion sesion = sec.LoginUser(uid, pass);
      if (sesion == null)
        Console.WriteLine("Error de credenciales!!");
      else
      {
        Console.WriteLine("Hola {0} su password expira el {1}", 
          sesion.FullName,
          sesion.PasswordExpira);
      }
      Console.WriteLine("Gracias!!");
      Console.ReadLine();
    }
  }
}
