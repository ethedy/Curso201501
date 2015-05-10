using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance
{
  /// <summary>
  /// Utilizaremos esta clase para probar que los genéricos son más eficientes que las colecciones de objetos
  /// 
  /// Por favor, compilar este código en modo RELEASE y con la opcion Platform Target en x64 o bien si queda en AnyCPU 
  /// quitar el tilde de la casilla que dice "Prefer 32-bit"
  /// 
  /// Si compilamos en modo DEBUG ver que el codigo condicional no este comentado o bien que la opcion de optimizacion TILDADA
  /// 
  /// NO EJECUTAR DESDE EL IDE --> hacerlo desde una ventana de linea de comandos (desde la carpeta bin/release) 
  /// Si ejecutamos desde el IDE hacerlo con Ctrl+F5 (ejecutar sin debug)
  /// 
  /// Otra posibilidad es reducir la cantidad de objetos a crear (constante ITEMS, podemos ponerla en 50 millones)
  /// Pueden ejecutar el .EXE teniendo el Task Manager en la pestaña Procesos y ordenar por memoria fisica utilizada
  /// o bien en la pestaña rendimiento y chequear como la grafica de memoria fisica sube increiblemente
  /// 
  /// Observar si hacen la prueba con 32 bits que aparece como "Genericos.exe*32"
  /// </summary>
  class Program
  {
    private const int ITEMS = 100000000;    //  100 millones está bien para una arquitectura de 64bits
                                            //  sin embargo en modo x86 el heap sólo puede crecer hasta 1.5GB
                                            //  observar que esto no es problema si usamos List<int>

    static void Main(string[] args)
    {
      int gcCount;
      Stopwatch sw = new Stopwatch();
      
      Console.WriteLine("Ejecutando en modo {0} ; Version {1}", Environment.Is64BitProcess ? "64 bits" : "32 bits",
#if DEBUG
        "DEBUG"
#else
        "RELEASE"
#endif
      );
      
      Console.WriteLine("Reseteando HEAP...");
      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();

      Console.WriteLine("Iniciando...\n");
      sw.Start();
      gcCount = GC.CollectionCount(0);

      ArrayList numArrayList = new ArrayList();

      for (int i = 0; i < ITEMS; i++)
      {
        numArrayList.Add(i);
        int x = (int)numArrayList[i];
      }

      sw.Stop();
      gcCount = GC.CollectionCount(0) - gcCount;
      Console.WriteLine("Con ArrayList:\n  milisegundos {0}\n  #Pasadas GC: {1}\n  Tamaño del heap (aprox): {2}\n",
                        sw.ElapsedMilliseconds, gcCount, GC.GetTotalMemory(false));
      Console.WriteLine("Presionar enter para seguir [CHECK TASK MANAGER ++++]");
      Console.ReadLine();
      
#if DEBUG
      numArrayList = null;
#endif      
      Console.WriteLine("Limpiando el heap...");

      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();

      Console.WriteLine("Listo, press any key....[CHECK TASK MANAGER ----]");
      Console.ReadLine();

      sw.Restart();
      gcCount = GC.CollectionCount(0);

      List<int> numList = new List<int>();

      for (int i = 0; i < ITEMS; i++)
      {
        numList.Add(i);
        int x = numList[i];
      }

      sw.Stop();
      gcCount = GC.CollectionCount(0) - gcCount;

      Console.WriteLine("Con List<int>:\n  milisegundos {0}\n  #Pasadas GC: {1}\n  Tamaño del heap (aprox): {2}\n",
                        sw.ElapsedMilliseconds, gcCount, GC.GetTotalMemory(false));
      Console.WriteLine("Presionar enter para seguir [CHECK TASK MANAGER ++]");
      Console.ReadLine();

#if DEBUG
      numList = null;
#endif
      
      Console.WriteLine("Limpiando el heap...");

      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();

      Console.WriteLine("Listo, press any key para terminar....[CHECK TASK MANAGER --]");
      Console.ReadLine();
    }
  }
}
