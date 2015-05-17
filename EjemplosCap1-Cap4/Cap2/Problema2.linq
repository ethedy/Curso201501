<Query Kind="Program" />

//  #define CONVERT

string nombre, input;
DateTime fechaNacimiento;
decimal salario;
int hijos;

void Main()
{
  Console.WriteLine ("Por favor ingresar nombre del empleado...");
  nombre = Console.ReadLine();

  Console.WriteLine("Ingresar cantidad de hijos de {0}", nombre);
  input = Console.ReadLine();
  
#if CONVERT
  hijos = Convert.ToInt32(input);
#else
  //  probar con diferentes entradas, verificar que es bastante amplia
  if (!int.TryParse(input, out hijos))
  {
    Console.WriteLine ("Error de ingreso!! La cadena {0} no representa un valor entero", input);
    return;
  }
#endif
  
  Console.WriteLine("Ingresar la fecha de nacimiento de {0}", nombre);
  input = Console.ReadLine();
  
#if CONVERT
  fechaNacimiento = Convert.ToDateTime(input);
#else
  //  probar con diferentes entradas, verificar que es bastante amplia
  if (!DateTime.TryParse(input, out fechaNacimiento))
  {
    Console.WriteLine ("Error de ingreso!! La cadena {0} no representa un valor de fecha", input);
    return;
  }
#endif

  Console.WriteLine ("Ingresar el salario de {0}", nombre);
  input = Console.ReadLine();
  
  //  Observar que pasa si uso puntos decimales...

#if CONVERT  
  //  Podemos usar los metodos de la clase Convert
  salario = Convert.ToDecimal(input);
#else
  //  O podemos usar los metodos de la clase particular que queremos obtener (value-type)
  if (!decimal.TryParse(input, out salario))
  {
    Console.WriteLine ("Error de ingreso!! La cadena {0} no representa un valor decimal", input);
    return;
  }
#endif

  Console.WriteLine ("===========================================================");
  Console.WriteLine ("Datos de {0}", nombre);
  Console.WriteLine ("--> Cantidad de hijos {0}", hijos);   
  Console.WriteLine ("--> Salario de {0:C3}", salario);   //  default con 2 decimales
  Console.WriteLine ("--> Fecha de nacimiento {0:D}", fechaNacimiento);   
}