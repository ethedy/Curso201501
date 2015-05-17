<Query Kind="Program" />

//
//  Armar diccionario, mostrar
//  Mostrar ordenando por Valor en lugar de clave

#undef PRINT_INICIO
#define SORT_GENERICO
#undef SORT_COMUN
#undef DICC_INVERSO

public static string donQuijote = @"
  En un lugar de la Mancha, de cuyo nombre no quiero acordarme, no ha mucho tiempo que vivía un hidalgo de 
  los de lanza en astillero, adarga antigua, rocín flaco y galgo corredor. Una olla de algo más vaca que 
  carnero, salpicón las más noches, duelos y quebrantos los sábados, lentejas los viernes, algún palomino 
  de añadidura los domingos, consumían las tres partes de su hacienda. El resto della concluían sayo de 
  velarte, calzas de velludo para las fiestas con sus pantuflos de lo mismo, los días de entre semana se 
  honraba con su vellori de lo más fino. Tenía en su casa una ama que pasaba de los cuarenta, y una sobrina 
  que no llegaba a los veinte, y un mozo de campo y plaza, que así ensillaba el rocín como tomaba la podadera. 
  Frisaba la edad de nuestro hidalgo con los cincuenta años, era de complexión recia, seco de carnes, enjuto 
  de rostro; gran madrugador y amigo de la caza. Quieren decir que tenía el sobrenombre de Quijada o Quesada 
  (que en esto hay alguna diferencia en los autores que deste caso escriben), aunque por conjeturas 
  verosímiles se deja entender que se llama Quijana; pero esto importa poco a nuestro cuento; basta 
  que en la narración dél no se salga un punto de la verdad.
  
  Es, pues, de saber, que este sobredicho hidalgo, los ratos que estaba ocioso (que eran los más del año) se 
  daba a leer libros de caballerías con tanta afición y gusto, que olvidó casi de todo punto el ejercicio de 
  la caza, y aun la administración de su hacienda; y llegó a tanto su curiosidad y desatino en esto, que 
  vendió muchas hanegas de tierra de sembradura, para comprar libros de caballerías en que leer; y así llevó 
  a su casa todos cuantos pudo haber dellos; y de todos ningunos le parecían tan bien como los que compuso el 
  famoso Feliciano de Silva: porque la claridad de su prosa, y aquellas intrincadas razones suyas, le parecían 
  de perlas; y más cuando llegaba a leer aquellos requiebros y cartas de desafío, donde en muchas partes 
  hallaba escrito: la razón de la sinrazón que a mi razón se hace, de tal manera mi razón enflaquece, que 
  con razón me quejo de la vuestra fermosura, y también cuando leía: los altos cielos que de vuestra 
  divinidad divinamente con las estrellas se fortifican, y os hacen merecedora del merecimiento que merece 
  la vuestra grandeza. Con estas y semejantes razones perdía el pobre caballero el juicio, y desvelábase 
  por entenderlas, y desentrañarles el sentido, que no se lo sacara, ni las entendiera el mismo Aristóteles, 
  si resucitara para sólo ello. No estaba muy bien con las heridas que don Belianis daba y recibía, porque 
  se imaginaba que por grandes maestros que le hubiesen curado, no dejaría de tener el rostro y todo el 
  cuerpo lleno de cicatrices y señales; pero con todo alababa en su autor aquel acabar su libro con la 
  promesa de aquella inacabable aventura, y muchas veces le vino deseo de tomar la pluma, y darle fin al 
  pie de la letra como allí se promete; y sin duda alguna lo hiciera, y aun saliera con ello, si otros 
  mayores y continuos pensamientos no se lo estorbaran.";
    
void Main()
{
      string[] palabras;
      Dictionary<string, int> diccionario;

      palabras = donQuijote.ToLower().Split(new [] { ' ', ',', ':', ';', '.', '\n', '\r', '(', ')' }, 
        StringSplitOptions.RemoveEmptyEntries);

      //foreach (var x in palabras)
      //  Console.WriteLine(x);

      diccionario = new Dictionary<string, int>();

      foreach (string palabra in palabras)
      {
        if (diccionario.ContainsKey(palabra))
          diccionario[palabra] += 1;
        else
          diccionario.Add(palabra, 1);
      }

#if PRINT_INICIO
      //  entrada --> KeyValuePair<>
      foreach (var entrada in diccionario)
        Console.WriteLine("La palabra {0} aparece {1} veces",entrada.Key, entrada.Value);
      
      Console.WriteLine("==================================================================");
#endif

#if SORT_COMUN
      //  Ordenamos por frecuencia de aparicion (tenemos que extraer Value)
      //
      int[] aFrecuencias;     //  value
      string[] aPalabras;     //  key

      aFrecuencias = new int[diccionario.Count];
      aPalabras = new string[diccionario.Count];
      
      ((Dictionary<string, int>.ValueCollection)diccionario.Values).CopyTo(aFrecuencias, 0);
      ((Dictionary<string, int>.KeyCollection)diccionario.Keys).CopyTo(aPalabras, 0);
      

      //  Ordena ambos arreglos en base al primero
      Array.Sort(aFrecuencias, aPalabras);

      for (int idx = 0; idx < aFrecuencias.Length; idx++)
      {
        Console.WriteLine("La palabra {1} aparece {0} veces", 
          aFrecuencias[idx], aPalabras[idx]);
      }
#endif

#if SORT_GENERICO
      foreach (var par in SortByValue<string, int>(diccionario))
      {
        Console.WriteLine ("{0, -5} {1}", par.Item1, par.Item2);
      }
#endif      

      //  Diccionario inverso ==> frecuencia con lista de palabras
#if DICC_INVERSO      
      SortedDictionary<int, List<string>> diccInverso = new SortedDictionary<int, List<string>>();

      foreach (var entrada in diccionario)
      {
        int frecuenciaPalabra = entrada.Value;
        string palabraActual = entrada.Key;

        if (diccInverso.ContainsKey(frecuenciaPalabra))
        {
          diccInverso[frecuenciaPalabra].Add(palabraActual);
        }
        else
        {
          diccInverso.Add(frecuenciaPalabra, new List<string>() { palabraActual });
        }
      }

      //  KeyValuePair<int, List<string>>
      foreach (var entry in diccInverso)
      {
        Console.WriteLine("Palabras que aparecen {0} veces: {1}",
          entry.Key, ListaDesdeArray(entry.Value));
      }
#endif
      Console.ReadLine();
}

public static string ListaDesdeArray(List<string> lista)
{
  StringBuilder sb = new StringBuilder();

  foreach (string s in lista)
  {
    sb.AppendFormat("{0} , ", s);
  }
  return sb.ToString();
}

/*
    DESAFIO: escribir un metodo generico para ordenar un diccionario por los valores 
    
    Retornar un arreglo de tuplas

*/
public Tuple<Value, Key>[] SortByValue<Key, Value>(Dictionary<Key, Value> dicc)
{
  Tuple<Value, Key>[] resultado = new Tuple<Value, Key>[dicc.Count];
  Value[] aValores = new Value[dicc.Count];
  Key[] aClaves = new Key[dicc.Count];

  ((Dictionary<Key, Value>.ValueCollection)dicc.Values).CopyTo(aValores, 0);
  ((Dictionary<Key, Value>.KeyCollection)dicc.Keys).CopyTo(aClaves, 0);
  
  Array.Sort(aValores, aClaves);
  
  for(int idx = 0; idx < aValores.Length; idx++)
  {
    resultado[idx] = new Tuple<Value, Key>(aValores[idx], aClaves[idx]);
  }
  return resultado;
}