<Query Kind="Statements" />

//
//  Recordar elegir en el combo Language --> C# Statement(s)

//  con las sentencias podemos declarar y usar variables
string nombre;
int horaActual;

nombre = Console.ReadLine();    //  <-- recordar siempre el punto y coma!!
horaActual = DateTime.Now.Hour;

//  podemos utilizar estructuras de decision, iteracion, etc...
if (horaActual > 12)
  Console.WriteLine ("Buenas tardes, {0}", nombre);
else
  Console.WriteLine ("Buenos dias, {0}", nombre);
