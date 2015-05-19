using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
  public class Persona
  {
    public string Apellido { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Localidad { get; set; }
    public string Provincia { get; set; }
    public string CodigoPostal { get; set; }
    public string CorreoElectronico { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaNacimiento { get; set; }
  }
}
