using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
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
