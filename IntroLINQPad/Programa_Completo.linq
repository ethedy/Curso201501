<Query Kind="Program" />


/*
    MomentoDelDia representan una especie de “constantes agrupadas” similar a lo que 
    podrian ser los dias de la semana o los meses del año
    Cada literal internamente esta representado por un numero sin embargo 
    para la mejor lectura del programa conviene usar el literal y no la 
    constante numerica
*/
public enum MomentoDelDia {
  Mañana, 
  Tarde,
  Noche,
  Madrugada
}

void Main()
{
  string nombre;
  
  nombre = Console.ReadLine();
  switch (GetMomento())
  {
    case MomentoDelDia.Madrugada:
      Console.WriteLine ("Hola, {0}. Estamos desvelados??", nombre);
      break;
      
    case MomentoDelDia.Mañana:
      Console.WriteLine ("Buenos dias {0}, que mañana nos espera!", nombre);
      break;
    
    case MomentoDelDia.Tarde:
      Console.WriteLine ("Buenas tardes {0}. Ya falta poco...", nombre);
      break;

    case MomentoDelDia.Noche:
      Console.WriteLine ("Buenas noches {0}. Dia agotador, no?", nombre);
      break;
  }
}

public MomentoDelDia GetMomento() 
{
  int hora = DateTime.Now.Hour;
  
  //  Forzar hora para que retorne Madrugada
  //  hora = 3;
  //  Forzar hora para que retorne Mañana
  //  hora = 8;
  //  Forzar hora para que retorne Tarde
  //  hora = 16;
  //  Forzar hora para que retorne Noche
  //  hora = 21;
  switch (hora / 6) 
  {
    case 0:
      return MomentoDelDia.Madrugada;

    case 1:
      return MomentoDelDia.Mañana;

    case 2:
      return MomentoDelDia.Tarde;

    case 3:
      return MomentoDelDia.Noche;
  }
  return MomentoDelDia.Mañana;
}