using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using WPFMySQLMVVM.DB;
using WPFMySQLMVVM.Models;
using WPFMySQLMVVM.ViewModel;

namespace WPFMySQLMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Invocamos el modelo y lo asignamos a DataContext
        private UserModelView model = new UserModelView();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = model;
            //Cargamos los datos existentes en la BDD
            model.LoadUsers();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (model.users == null) model.users = new ObservableCollection<User>();
            //Si el registro no existe, procedemos a crearlo
            if(model.users.Where(x => x.userName == model.userName).FirstOrDefault() == null)
            {
                model.users.Add(new User
                {
                    userName = model.userName,
                    mail = model.mail,
                    age = model.age
                });
                //una vez agregado el registro al modelo, lo agregamos a la BDD
                model.NewUser();
            }
            //Si el registro ya existe, debemos actualizarlo
            else
            {
                foreach(User r in model.users)
                {
                    if(r.userName.Equals(model.users))
                    {
                        r.mail = model.mail;    
                        r.age = model.age;
                        break;
                    }
                }

                //Actualizamos
                model.UpdateUser();
            }
        }
    }

   

}
