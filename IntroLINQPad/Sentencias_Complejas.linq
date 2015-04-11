<Query Kind="Statements">
  <Reference Relative="..\..\..\Descargas\NSoup.dll">E:\ET\Descargas\NSoup.dll</Reference>
  <Namespace>NSoup</Namespace>
  <Namespace>NSoup.Nodes</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

//
//  Recordar elegir en el combo Language --> C# Statement(s)

//  Agregar en propiedades de la consulta (F4) "Additional Namespace Imports" el namespace
//  System.Net

HttpWebRequest req = WebRequest.Create("http://www.w3schools.com/html/default.asp") as HttpWebRequest;

HttpWebResponse res = req.GetResponse() as HttpWebResponse;

StreamReader rdr = new StreamReader(res.GetResponseStream());

//  [*]
  rdr.ReadToEnd().Dump();   

//  una vez que compruebo que se accede al contenido HTML...comentar [*] 


//  Descomentar [1] y [2] comprobar que no funciona
//  Download de nsoup.dll desde https://nsoup.codeplex.com/
//  F4 --> Additional References --> Add --> ubicar el assembly (DLL) bajado y agregarlo
//  F4 --> Additional Namespace Imports --> Pick From Assemblies --> agregar los 
//         namespaces NSoup y NSoup.Nodes

//  F5 --> tarda un momento y visualiza con Dump los elementos <a> del documento (hipervinculos)
//         en un formato de tabla con las columnas como propiedades, que a su vez tambien
//         pueden ser objetos complejos

//  [1]
//  Document doc = NSoupClient.Parse(rdr.ReadToEnd());

//  [2]
//  doc.Select("a").Dump();

//  comentar [2]
//  descomentar [3]

//  El primer Select es de NSoup (con el selector CSS como argumento)
//    div.row ==> selecciona todos los elementos <div> que tengan class="row" dentro
//                del documento HTML
//  El segundo Select es de LINQ y actua sobre colecciones, usando una expresion lambda
//  que permite proyectar la coleccion mostrando solamente las propiedades que nos
//  interesan (en este caso OuterHtml)

//  [3]
//  doc.Select("div.row")
//    .Select( elem => new { Elem = "DIV", Texto = elem.OuterHtml() } )
//    .Dump();

//  Pregunta
//  Como se podria hacer para mostrar el resultado del primer Select (o sea TODAS las 
//  propiedades de los Element que cumplen con el criterio de seleccion) SIN AFECTAR
//  el resultado final de la consulta?

//  comentar [3]
//  descomentar [4]

//  [4]
//  doc.Select("a")
//    .Select( elem => new { URL = elem.Attributes["href"], Texto = elem.OuterHtml() } )
//    .Dump();

//  Podria explicar que hace esta consulta?