using System;
using System.Collections.Generic;
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
using WpfClientProject.ServiceReferenceBank;

namespace WpfClientProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool passOk;
       
        ServiceReferenceBank.ServiceBaseClient ServiceClient;
        User user;
        public MainWindow()
        {
            InitializeComponent();
            ServiceClient = new ServiceReferenceBank.ServiceBaseClient();
            passOk = false;
            user = new User();
            
            this.DataContext = user;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register newWindow = new Register();
            newWindow.Show();
            this.Close();
            


        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Password = pbPass.Password;
            user.realid = tbID.Text;
            User loggeduser = ServiceClient.UserLogin(user);
            if (loggeduser != null)
            {
                NewUserPage reg = new NewUserPage(loggeduser);
                this.Close();
                reg.Show();
            }

        }
        private void Passwor_Changed(object sender, RoutedEventArgs e)
        {
            tbPass.Text = pbPass.Password;
            VaildPassword vaildPassword = new VaildPassword();
            ValidationResult result = vaildPassword.Validate(pbPass.Password, null);
            if (!result.IsValid)
            {
                pbPass.BorderBrush = Brushes.Red;
                pbPass.BorderThickness = new Thickness(2);
                pbPass.ToolTip = result.ErrorContent;
                passOk = false;
            }
            else
            {
                pbPass.BorderBrush = Brushes.Transparent;
                pbPass.BorderThickness = new Thickness(0);
                pbPass.ToolTip = null;
                passOk = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NewUserPage newWindow = new NewUserPage(user);
            newWindow.Show();
            this.Close();
        }
    }
}
