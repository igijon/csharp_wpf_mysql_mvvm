using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WPFMySQLMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Invocamos el modelo y lo asignamos a DataContext
        private ModelView model = new ModelView();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (model.registros == null) model.registros = new ObservableCollection<Registro>();
            //Si el registro no existe, procedemos a crearlo
            if(model.registros.Where(x => x.usuario == model.usuario).FirstOrDefault() == null)
            {
                model.registros.Add(new Registro
                {
                    usuario = model.usuario,
                    mail = model.mail,
                    edad = model.edad
                });
                //una vez agregado el registro al modelo, lo agregamos a la BDD
                model.NuevoRegistro();
            }
        }
    }

    public class ModelView: INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;
        //Declaro la constante para la conexión a la BDD
        private const String cnstr = "server=localhost;uid=inma;pwd=inma_dam2t_mysql;database=maptrack";
        //Modelo de la lista de registros a mostrar
        private ObservableCollection<Registro> _registros;
        private String _usuario = "";
        private String _mail = "";
        private String _edad = "";
        #endregion

        #region OBJETOS
        public ObservableCollection<Registro> registros
        {
            get { return _registros; }
            set { 
                _registros = value;
                OnPropertyChange("registros");
            }
        }
        public String usuario
        {
            get { return _usuario; }
            set { 
                _usuario = value;
                OnPropertyChange("usuario");
            }
        }
        public String mail
        {
            get { return _mail; }
            set { 
                _mail = value;
                OnPropertyChange("mail");
            }
        }

        public String edad
        {
            get { return _edad; }
            set
            {
                _edad = value;
                OnPropertyChange("edad");
            }
        }
        #endregion

        //Método que se encarga de actualizar las propiedades en cada cambio
        private void OnPropertyChange(string propertyName)
        {
            if(PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NuevoRegistro()
        {
            String SQL = $"INSERT INTO usuarios (usuario, mail, edad) VALUES ('{usuario}','{mail}', '{edad}');";
            //usaremos las clases de la librería de MySQL para ejecutar queries
            //Instalar el paqueta MySQL.Data
            MySqlConnection con = new MySqlConnection(cnstr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(SQL, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
    }

    //Creamos el modelo de datos de la lista
    public class Registro: INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;
        private String _usuario = "";
        private String _mail = "";
        private String _edad = "";
        #endregion

        #region OBJETOS
        public String usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
                OnPropertyChange("usuario");
            }
        }
        public String mail
        {
            get { return _mail; }
            set
            {
                _mail = value;
                OnPropertyChange("mail");
            }
        }

        public String edad
        {
            get { return _edad; }
            set
            {
                _edad = value;
                OnPropertyChange("edad");
            }
        }
        #endregion

        //Método que se encarga de actualizar las propiedades en cada cambio
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
