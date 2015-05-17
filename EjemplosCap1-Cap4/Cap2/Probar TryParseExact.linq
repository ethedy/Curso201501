<Query Kind="Statements" />


DateTime fecha;
System.Globalization.CultureInfo cultura = new System.Globalization.CultureInfo("en-US");
string cadena ;

//  cadena = "lunes, 10 de abril de 1967";
//  cadena = "monday, april 10, 1967";
cadena = "10/04/1967";

DateTime.TryParseExact(cadena, new [] { "dd/MM/yyyy", "D"}, 
  cultura, System.Globalization.DateTimeStyles.None, out fecha);

Console.WriteLine (fecha.ToString("D", null));

