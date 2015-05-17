<Query Kind="Statements" />

//  Problema 2
//  Las dos soluciones propuestas 
//  #define SOL1 o SOL2 para ejecutar cada solucion

#define SOL2

//  Solucion 1.1
#if SOL1

int contador, numero ;

contador = 1;
numero = 0;
while (contador <= 100) 
{
  if (numero % 2 == 0)
  {
    Console.WriteLine (numero);
    contador = contador + 1;
  }
  numero = numero + 1;
}

#endif

//  Solucion 1.2
#if SOL2

for (int contador = 1, numero = 0; contador <= 100 ; numero+=2, contador++)
{
  Console.WriteLine (numero);
}

#endif

