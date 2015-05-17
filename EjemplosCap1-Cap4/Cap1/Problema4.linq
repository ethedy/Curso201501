<Query Kind="Statements" />

//  Crear una lista de numeros enteros positivos comenzando desde 2
//  Al inicio, comenzamos con p=2 (primer numero primo en la lista)
//  Marcar todos los numeros multiplos de p MAYORES a p (hasta que no tengamos mas numeros)
//  Encontrar el siguiente numero no marcado MAYOR a p, repetir...
//

int[] numeros;

numeros = new int[121];

//  marcamos el 0 y el 1 
numeros[0] = numeros[1] = -1;

//
for (int i = 2 ;  i <= 120 ; i++ )
  numeros[i] = i;

//  numeros.Dump();

int n = 2;
int p = 2;
int marcar;
bool salir = false;

while (!salir)
{
  marcar = n * p;
  
  while ( marcar <= 120 )
  {
    numeros[marcar] = -1;
    n++;
    marcar = n * p;
  }
  
  //  encontrar siguiente valor de p
  int next_p = p + 1;
  
  while (next_p <= 120)
  {
    if (numeros[next_p] == -1)
      next_p++;
    else
      break;
  }
  //  next_p.Dump();
  //  salir = true;
  
  //  si es mayor a 120, terminar
  if (next_p >= 120)
    salir = true;
  else 
  {
    p = next_p;
    n = 2;
  }
}

Console.WriteLine ("Los numeros primos menores a 120 son:");

for (int idx = 0 ; idx < 121 ; idx++)
  if (numeros[idx] != -1) 
    Console.WriteLine (numeros[idx]);
    