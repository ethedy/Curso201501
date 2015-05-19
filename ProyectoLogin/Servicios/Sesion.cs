using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Servicios
{
  public class Sesion
  {
    public Usuario Usuario { get; private set; }

    public string FullName
    {
      get { return string.Format("{0} {1}", Usuario.Persona.Nombre, Usuario.Persona.Apellido); }
    }

    public DateTime PasswordExpira
    {
      get
      {
        return Usuario.FechaExpiracionPassword;
      }
    }

    public Sesion(Usuario connected)
    {
      Usuario = connected;
    }
  }
}
