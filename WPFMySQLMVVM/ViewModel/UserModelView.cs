using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using WPFMySQLMVVM.DB;
using WPFMySQLMVVM.Models;

namespace WPFMySQLMVVM.ViewModel
{
    public class UserModelView : INotifyPropertyChanged
    {
        #region VARIABLES
        public event PropertyChangedEventHandler? PropertyChanged;

        //Declaro la constante para la conexión a la BDD
        private const String cnstr = "server=localhost;uid=inma;pwd=inma_dam2t_mysql;database=maptrack";
        //Modelo de la lista de registros a mostrar
        private ObservableCollection<User> _users;
        private String _user = "";
        private String _mail = "";
        private String _age = "";
        #endregion

        #region OBJETOS
        public ObservableCollection<User> users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChange("users");
            }
        }
        public String userName
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChange("user");
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

        public String age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChange("age");
            }
        }
        #endregion

        //Método que se encarga de actualizar las propiedades en cada cambio
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NewUser()
        {
            String SQL = $"INSERT INTO usuarios (usuario, mail, edad) VALUES ('{userName}','{mail}', '{age}');";
            //usaremos las clases de la librería de MySQL para ejecutar queries
            //Instalar el paqueta MySQL.Data
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void UpdateUser()
        {
            String SQL = $"UPDATE usuarios SET mail = '{mail}', edad = '{age}' WHERE usuario = '{userName}';";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void LoadUsers()
        {
            String SQL = $"SELECT usuario, mail, edad FROM usuarios;";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);
            if (dt.Rows.Count > 0)
            {
                if (users == null) users = new ObservableCollection<User>();
                foreach (DataRow i in dt.Rows)
                {
                    users.Add(new User
                    {
                        userName = i[0].ToString(),
                        mail = i[0].ToString(),
                        age = i[0].ToString()
                    });
                }
            }
            dt.Dispose();
        }
    }
}
