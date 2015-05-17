<Query Kind="Statements" />


//  Problema 1
//  Las tres soluciones propuestas 
//  #define SOL1, SOL2 o SOL3 para ejecutar cada solucion

#define SOL1

//  Solucion 1.1
#if SOL1

Console.WriteLine ("Solucion usando while");
int i ;

i = 1;
while (i <= 100) 
{
  Console.WriteLine (i);
  i = i +1;
}

#endif

//  Solucion 1.2
#if SOL2

Console.WriteLine ("Solucion usando do ... while");
int i ;

i = 1;
do 
{
  Console.WriteLine (i);
  i = i +1;
} while (i <= 100);

#endif

//  Solucion 1.3
#if SOL3

Console.WriteLine ("Solucion usando for");

for (int i = 1; i <= 100; i = i + 1) 
  Console.WriteLine (i);

#endif