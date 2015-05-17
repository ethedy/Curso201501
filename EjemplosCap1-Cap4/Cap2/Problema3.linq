<Query Kind="Program" />

void Main()
{
  P(5, 100).Dump();   //  P(x) = 100
  P(5, 100, 50).Dump();   //  P(x) = 100x + 50 
  P(5, 10, 20, 30).Dump();   //  P(x) = 10 x2 + 20 x + 30 = 250 + 100 + 30
}

double P(int x, params int[] coef) {
  double result = 0.0;
  int grado;
  
  if (coef.Length == 0)
    Console.WriteLine ("Sin coeficientes???");
  
  //  vemos cuantos coeficientes hay... y ahi tenemos el grado del polinomio
  //
  grado = coef.Length - 1;
  
  for (int j = 0; grado >= 0 ; grado--, j++)
    result += coef[j] * Math.Pow(x, grado);
  
  return result;
}
