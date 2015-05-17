<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

/*

  A diferencia de lo que normalmente se hace (usar el tipo long para obtener el resultado de la
  operacion) vamos a usar directamente el tipo BigInteger
  
  Por supuesto, colocaremos un tope de 100 para que la operacion y el tamaño del resultado 
  no se extiendan mucho
  
  Recordar que debemos elegir "program" (tenemos que definir metodos)
  
  Desde Main, llamamos al metodo principal
  
      Factorial(x).Dump();

  Recordar agregar referencias y namespace si se desea
*/

void Main()
{
  Factorial(100).Dump();
}

BigInteger Factorial(byte n) {
  if (n > 100) {
    Console.WriteLine ("Sorry, no se puede calcular más alla de 100!...");
    return BigInteger.Zero;
  }
  
  //  esta es la condicion de salida de la recursion...
  if (n == 1)
    return 1;
    
  return n * Factorial((byte)(n - 1));
}

