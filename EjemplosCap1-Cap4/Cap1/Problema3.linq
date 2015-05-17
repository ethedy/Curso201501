<Query Kind="Statements" />

//  Problema 3
//  Las dos soluciones propuestas 
//  #define SOL1 o SOL2 para ejecutar cada solucion

#define SOL2

//  Solucion 1.1
#if SOL1

DateTime dia = new DateTime(2015, 1, 1);
DateTime final = new DateTime(2015, 12, 31);

while (dia <= final)
{
  if (dia.DayOfWeek == DayOfWeek.Saturday || dia.DayOfWeek == DayOfWeek.Sunday)
    Console.WriteLine ("{0} es fin de semana", dia); 
  
  dia = dia.AddDays(1);
}

#endif

//  Solucion 1.2
//  Un poco mas amigable para dar la fecha

#if SOL2

DateTime dia = new DateTime(2015, 1, 1);
DateTime final = new DateTime(2015, 12, 31);

while (dia <= final)
{
  if (dia.DayOfWeek == DayOfWeek.Saturday || dia.DayOfWeek == DayOfWeek.Sunday)
    Console.WriteLine ("{0:dd/MMM} es fin de semana ({1})", 
      dia, 
      dia.DayOfWeek == DayOfWeek.Sunday ? "Domingo": "Sabado");
  
  dia = dia.AddDays(1);
}

#endif

