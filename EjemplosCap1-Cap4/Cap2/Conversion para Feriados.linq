<Query Kind="Program" />

//
//  Usamos una cadena verbatim para especificar los feriados

string fechas = @"
  01/01/2015, 16/02/2015, 17/02/2015, 23/03/2015, 24/03/2015,
  02/04/2015, 03/04/2015, 01/05/2015, 25/05/2015, 20/06/2015,
  09/07/2015, 17/08/2015, 12/10/2015, 23/11/2015, 07/12/2015,
  08/12/2015, 25/12/2015" ;


void Main()
{
  
  GetArrayFeriados(fechas).Dump();
}

DateTime[] GetArrayFeriados(string s) 
{
  string[] fechas;
  DateTime[] result;
  
  fechas = s.Split(',');
  result = new DateTime[fechas.Length];
  
  for (int j = 0; j < fechas.Length; j++)
    result[j] = DateTime.Parse(fechas[j].Trim());

  return result;
}
