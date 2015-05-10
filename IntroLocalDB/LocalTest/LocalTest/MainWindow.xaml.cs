using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SqlClient;
using System.IO;
using System.Xaml;
using System.Xml;
using Syncfusion.Windows.Shared;

namespace LocalTest
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private SqlConnection _conn;

    private const string LDB_INSTANCE = @"Server=(localdb)\v11.0; Integrated Security=true;";

    //  Opcion con instancia automatica
    //private const string LDB_DATABASE = @"Server=(localdb)\v11.0; Database=LocalTest; Integrated Security=true;";

    //  Opcion con instancia con nombre --> falla la primera vez
    private const string LDB_DATABASE = @"Server=(localdb)\SQL12; Database=LocalTest; Integrated Security=true;";

    //  Opcion con instancia con nombre --> attach file
    private const string LDB_DATABASE_ATTACH = @"Server=(localdb)\SQL12; AttachDbFilename=|DataDirectory|App_Data\LocalTest.mdf; Database=LocalTest; Integrated Security=true;";


    private const string LDB_FILE = @".\App_Data\LocalTest.mdf";

    private const string DBNAME = @"LocalTest";

    private const string CMD_CREATEDATABASE_DEFAULT = @"create database {0} ";

    private const string CMD_CREATEDATABASE_FILE = @"
      create database {0} on primary (
        name=Database_file,
        filename='{1}',
        size=20MB, filegrowth=10%, maxsize=10GB
      )
";

    private const string CMD_DROPDATABASE = @"drop database {0}";
    
    private const string CMD_TEST = @"
      select 
	      SERVERPROPERTY('Edition') as Edicion
	      , SERVERPROPERTY('EngineEdition') as Edicion_Motor
	      , SERVERPROPERTY('ProductVersion') as Version_Producto
	      , SERVERPROPERTY('ProductLevel') as Nivel_Producto
";

    public MainWindow()
    {
      InitializeComponent();
    }
    
    private void btnConectarInstancia_Click(object sender, RoutedEventArgs args)
    {
      Message(null);
      Message("Intentando conectar a una instancia de LocalDB...");
      try
      {
        _conn = new SqlConnection(LDB_INSTANCE);
        _conn.Open();
        Message("Instancia conectada con exito...");
        Message(string.Format("Conectado a la base de datos: {0}", _conn.Database));

        _conn.Close();
        Message("Conexion cerrada...");
      }
      catch (SqlException ex)
      {
        Message(ex.Message);
      }
    }

    private void crearDB_Default(object sender, RoutedEventArgs args)
    {
      Message(null);
      Message("Intentando conectar a una instancia de LocalDB...");
      try
      {
        _conn = new SqlConnection(LDB_INSTANCE);
        _conn.Open();
        Message("Instancia conectada con exito...");

        Message("Ejecutando CREATE DATABASE (default)");

        SqlCommand cmd = _conn.CreateCommand();

        cmd.CommandText = string.Format(CMD_CREATEDATABASE_DEFAULT, DBNAME);
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery();

        Message("Base de datos creada con exito!!");

        _conn.Close();
        Message("Conexion cerrada...");
      }
      catch (SqlException ex)
      {
        Message(ex.Message);
      }
    }

    private void crearDB_Ubicacion(object sender, RoutedEventArgs args)
    {
      Message(null);
      Message("Intentando conectar a una instancia de LocalDB...");
      try
      {
        _conn = new SqlConnection(LDB_INSTANCE);
        _conn.Open();
        Message("Instancia conectada con exito...");

        Message("Ejecutando CREATE DATABASE en ubicacion determinada");

        SqlCommand cmd = _conn.CreateCommand();

        //  Tenemos que proporcionar la ruta completa del archivo
        string fullPath = System.IO.Path.GetFullPath(LDB_FILE);

        //  Chequeamos si la ruta (solo la ruta) no existe, y en todo caso la creamos
        string pathOnly = System.IO.Path.GetDirectoryName(fullPath);

        if (!Directory.Exists(pathOnly))
          Directory.CreateDirectory(pathOnly);

        cmd.CommandText = string.Format(CMD_CREATEDATABASE_FILE, DBNAME, fullPath);
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery();

        Message("Base de datos creada con exito!!");

        _conn.Close();
        Message("Conexion cerrada...");
      }
      catch (SqlException ex)
      {
        Message(ex.Message);
      }
      catch (Exception ex)
      {
        //  Puede haber algun error en la creacion de la ruta por ejemplo...
        Message(ex.Message);
      }
    }

    private void btnOpenDB_Default(object sender, RoutedEventArgs args)
    {
      Message(null);
      Message("Intentando abrir la base de datos a partir de la informacion de la instancia...");
      try
      {
        //  por las dudas que no funcione, inhabilitamos el boton de prueba
        btnCloseDB.IsEnabled = btnDropDB.IsEnabled = btnTestDB.IsEnabled = false;

        _conn = new SqlConnection(LDB_DATABASE);
        _conn.Open();

        Message(string.Format("Base de datos abierta OK: {0}", _conn.Database));

        //  observar que no cerramos la conexión!!

        //  si todo anduvo bien, habilitamos botones contextuales
        btnCloseDB.IsEnabled = btnDropDB.IsEnabled = btnTestDB.IsEnabled = true;
      }
      catch (SqlException ex)
      {
        SqlError error = ex.Errors[0];

        Message(string.Format("{1,12:}: {0}", error.Message, error.Number));
      }
    }

    private void btnOpenDB_Attach(object sender, RoutedEventArgs args)
    {
      Message(null);
      Message("Intentando abrir la base de datos atachando el archivo MDF...");
      try
      {
        //  por las dudas que no funcione, inhabilitamos el boton de prueba
        btnCloseDB.IsEnabled = btnDropDB.IsEnabled = btnTestDB.IsEnabled = false;

        _conn = new SqlConnection(LDB_DATABASE_ATTACH);
        _conn.Open();

        Message(string.Format("Base de datos abierta OK: {0}", _conn.Database));

        //  observar que no cerramos la conexión!!

        //  si todo anduvo bien, habilitamos botones contextuales
        btnCloseDB.IsEnabled = btnDropDB.IsEnabled = btnTestDB.IsEnabled = true;
      }
      catch (SqlException ex)
      {
        //  puede ocurrir que no se encuentre la instancia por ejemplo o que no se encuentre la DB
        //  ambos errores vienen en la misma excepcion, por eso hay que chequear el numero de error
        SqlError error = ex.Errors[0];
        //  4060 ==> no se puede abrir la base de datos
        //  -1983577849 ==> 
        /*
              {"Error relacionado con la red o específico de la instancia mientras se establecía una conexión con el 
                servidor SQL Server. No se encontró el servidor o éste no estaba accesible. Compruebe que el nombre de 
                la instancia es correcto y que SQL Server está configurado para admitir conexiones remotas. (provider: 
                SQL Network Interfaces, error: 50 - Se produjo un error de Local Database Runtime. La instancia de 
                LocalDB especificada no existe.\r\n)"}

          Observar que el error de LDB esta incrustado en el mensaje, no forma parte de la coleccion!!
        */
        Message(string.Format("{1,12:}: {0}", error.Message, error.Number));
      }
    }

    private void btnTestDB_Click(object sender, RoutedEventArgs args)
    {
      //  suponemos que _conn tiene una conexion valida
      if (_conn != null && _conn.State == ConnectionState.Open)
      {
        SqlCommand cmd = _conn.CreateCommand();
        
        cmd.CommandText = CMD_TEST;
        cmd.CommandType = CommandType.Text;

        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.HasRows)
        {
          dr.Read();

          Message(string.Format("Resultado test: Edicion={0}; Motor={1}; Version={2}; Level={3}", 
            dr.GetString(0), dr.GetInt32(1), dr.GetString(2), dr.GetString(3)));
        }
        dr.Close();
      }
      else
      {
        Message("La conexion no existe o esta cerrada...");
      }
    }

    private void btnDropDB_Click(object sender, RoutedEventArgs args)
    {
      try
      {
        //  si la conexion esta abierta la usamos...si no la tenemos que abrir
        if (_conn != null && _conn.State == ConnectionState.Open)
          _conn.ChangeDatabase("master");
        else
        {
          _conn = new SqlConnection(LDB_INSTANCE);
          _conn.Open();
        }
        SqlCommand cmd = _conn.CreateCommand();

        cmd.CommandText = string.Format(CMD_DROPDATABASE, DBNAME);
        cmd.CommandType = CommandType.Text;

        cmd.ExecuteNonQuery();
        Message("Base de datos eliminada...");
        _conn.Close();
      }
      catch (Exception ex)
      {
        Message(ex.Message);
      }
    //else
    //{
    //  Message("La conexion no existe o esta cerrada...");
    //}
    }

    private void btnCloseDB_Click(object sender, RoutedEventArgs args)
    {
      if (_conn != null && _conn.State == ConnectionState.Open)
      {
        _conn.Close();
        btnCloseDB.IsEnabled = btnDropDB.IsEnabled = btnTestDB.IsEnabled = false;
        Message("La conexion fue cerrada...");
      }
    }


    private void Message(string texto)
    {
      if (texto == null)
        txtMensajes.Clear();
      else
      {
        txtMensajes.AppendText(texto);
        txtMensajes.AppendText("\n");
      }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      //GetTemplate();
    }
/*
    private void GetTemplate()
    {
      ControlTemplate template = ButtonAdv1.Template;

      XmlWriterSettings settings = new XmlWriterSettings();
      settings.Indent = true;
      StringBuilder sb = new StringBuilder();
      XmlWriter wr = XmlWriter.Create(sb, settings);
      System.Windows.Markup.XamlWriter.Save(template, wr);

      string s = sb.ToString();
    }
 * */
  }
}
