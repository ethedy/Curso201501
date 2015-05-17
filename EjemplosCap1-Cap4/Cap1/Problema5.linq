<Query Kind="Statements" />

//  los agrego desordenados para ver como funciona Array.Sort
//
DateTime[] feriados = new DateTime[] {
  new DateTime(2015, 1, 1), new DateTime(2015, 2, 16), new DateTime(2015, 2, 17),
  new DateTime(2015, 10, 12), new DateTime(2015, 11, 23), new DateTime(2015, 12, 7),
  new DateTime(2015, 4, 3), new DateTime(2015, 5, 1), new DateTime(2015, 5, 25),
  new DateTime(2015, 6, 20), new DateTime(2015, 7, 9), new DateTime(2015, 8, 17),
  new DateTime(2015, 3, 23), new DateTime(2015, 3, 24), new DateTime(2015, 4, 2),
  new DateTime(2015, 12, 8), new DateTime(2015, 12, 25)
};

//  feriados.Dump();

//  me aseguro que esten ordenados
Array.Sort(feriados);

//  feriados.Dump();

int cuentaDias;
int nextFeriado = 0;

/*  
  Tengamos en cuenta que puede haber dos feriados consecutivos que formen parte de un
  fin de semana largo... por eso no conviene usar for ya que la cantidad de iteraciones
  no es conocida (aunque sepamos que solamente tenemos 17 feriados)
*/  

while (nextFeriado < feriados.Length) {
  DateTime diaBase = feriados[nextFeriado++];   //  ojo con el ++ tramposo
  DateTime inicioFinde;
  bool posibleLargo;
  
  if (diaBase.DayOfWeek == DayOfWeek.Monday)
  {
    //  si es lunes, el inicio del fin de semana largo sera el sabado
    //  no puede ser el viernes ya que de otro modo se hubiera procesado primero
    cuentaDias = 3;   //  lunes + domingo + sabado
    inicioFinde = diaBase.AddDays(-2);  //  inicio del fdsl es el sabado
  }
  else
  {
    //  aca vienen todos los otros casos...
    cuentaDias = 1;           //  el dia feriado actual
    inicioFinde = diaBase;    //  el dia actual
  }
  
  posibleLargo = true;
  while (posibleLargo)
  {
    bool nextDiaFeriado;
    
    diaBase = diaBase.AddDays(1);    //  agrego uno al dia base
    //  si el siguiente dia es feriado, agrego uno a nextFeriado y a la cuenta de dias
    //  si es fin de semana, entonces agrego 1 solamente a la cuenta de dias
    if ((nextDiaFeriado = Array.BinarySearch(feriados, diaBase) > 0) ||
        diaBase.DayOfWeek == DayOfWeek.Saturday || diaBase.DayOfWeek == DayOfWeek.Sunday) 
    {
      cuentaDias++;
      if (nextDiaFeriado)
        nextFeriado++;
    }
    else
      posibleLargo = false;   //  salir del loop
  }
  
  if (cuentaDias >= 3)
  {
    Console.WriteLine ("Se encontro un fin de semana largo:");
    for (DateTime inicio=inicioFinde ; cuentaDias > 0 ; cuentaDias--, inicioFinde = inicioFinde.AddDays(1))
      Console.WriteLine ("  --> {0:D}", inicioFinde);
  }
}