using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Datos;
using Entidades;

namespace Servicios
{
  public class SecurityServices
  {
    public Sesion LoginUser(string uid, string pass)
    {
      Usuario usr = DB.Usuarios.FirstOrDefault(x => x.Login == uid);
      Sesion newSesion = null;

      if (usr != null)
      {
        //  ahora chequeamos la password
        if (DB.LoginUsuario(usr, pass))
        {
          newSesion = new Sesion(usr);
        }
      }
      //  mostrar error de usuario o pass invalidos, pero auditar la causa real
      return newSesion;
    }
  }
}
