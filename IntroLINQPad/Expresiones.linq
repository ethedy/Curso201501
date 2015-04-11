<Query Kind="Expression" />

//
//  Recordar elegir en el combo Language --> C# Expression

//  Comentar y descomentar cada linea de codigo (una por vez)
//  Ejecutar con F5
//  Recordar que una expresion DEBE retornar un valor (ya sea boolean, numerico, cadena...etc)
//  Recordar que las expresiones no pueden terminar con punto y coma

//  seno hiperbolico, mediante x^y usando como base la constante Math.E para el valor de e
//
//  (Math.Pow(Math.E, 3) - Math.Pow(Math.E, -3)) / 2

//  seno hiperbolico, mediante e^x usando Math.Exp
//  La base esta prefijada en la constante e
//
//  (Math.Exp(3) - Math.Exp(-3)) / 2)


//  seno hipperbolico mediante la funcion integrada
//
//  Math.Sinh(3)

//  expresion booleana que utiliza como operandos datos de diversos tipos
//
//  Int32.MaxValue < Int64.MaxValue & "abcd" != "ABCD" | !true

//  expresion de cadena (string) quita los espacios de la cadena izquierda, luego
//  toma una subcadena de 3 caracteres empezando desde el tercero
//  ATENTO: se empiezan a contar los caracteres desde CERO
//
//"    Hola  , Mundo ".Replace(" ", "").Substring(3, 3) 

//  Que pasa si invierto el orden de las llamadas?
//
//  "    Hola  , Mundo ".Substring(3, 3) .Replace(" ", "")

//  Dump() permite imprimir resultados parciales sin afectar el resultado
//  --> Toma lo que retorna Replace()
//  --> Lo muestra
//  --> Se lo pasa como parametro a Substring
//
//  "    Hola  , Mundo ".Replace(" ", "").Dump().Substring(3, 3) 
