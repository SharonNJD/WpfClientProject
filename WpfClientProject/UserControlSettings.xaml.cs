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
    /// Interaction logic for UserControlSettings.xaml
    /// </summary>
    public partial class UserControlSettings : UserControl
    {
        User User;
        bool passOk = false;
        ServiceReferenceBank.ServiceBaseClient ServiceClient;
        public UserControlSettings(User user)
        {
            InitializeComponent();
            User = user;
            this.DataContext = User;
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
        private void Updatebtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Validation.GetHasError(tbxId) && !Validation.GetHasError(tbxFirstName) && !Validation.GetHasError(tbxLastName) && (passOk) && !Validation.GetHasError(tbxEmail1) && !Validation.GetHasError(tbxPhoneNum1))
            {
                ServiceClient = new ServiceBaseClient();
                User.Password = pbPass.Password;
                User.realid = tbxId.Text;
                User.FirstName = tbxFirstName.Text;
                User.LastName = tbxLastName.Text;
                User.Email = tbxEmail1.Text;
                User.Phonenum = tbxPhoneNum1.Text;
                ServiceClient.UpdateUser(User);
                MessageBox.Show("Saved!");
            }
            else
            {
                MessageBox.Show("something is wrong pls fix");
            }
        }

        private void Deleteacouunt_Click(object sender, RoutedEventArgs e)
        {
             
            
            BankAccountList list = ServiceClient.GetBankAccountsByUser(User);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ServiceClient.DeleteBankAccount(list[i]);
                }
            }
            if (ServiceClient.GetCustomerByUser(User) != null)
            {
                ServiceClient.DeleteCustomers(ServiceClient.GetCustomerByUser(User));
            }
            ServiceClient.DeleteUser(User);
        }
    }
}
