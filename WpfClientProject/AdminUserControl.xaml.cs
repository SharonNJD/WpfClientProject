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
using WpfClientProject.ServiceReferenceCurrency;

namespace WpfClientProject
{
    /// <summary>
    /// Interaction logic for AdminUserControl.xaml
    /// </summary>
    public partial class AdminUserControl : UserControl
    {
        private ServiceBaseClient ServiceClient;
        private User user;
        private User viewUser;
        private UserList UserList;
        private BankAccountList userAccounts;
        public AdminUserControl(User user)
        {
            InitializeComponent();
            ServiceClient = new ServiceBaseClient();
            UserList = ServiceClient.GetAllUsers();
            Userslv.ItemsSource = UserList;
            this.user = user;
        }

        private void Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbAccountNet.Text = tbTotalNet.Text = string.Empty;
            cmbAccounts.ItemsSource = null;
            if (Userslv.SelectedIndex == -1) return;
            viewUser = Userslv.SelectedItem as User;
            tbTitle.Text= viewUser.FirstName+" "+ viewUser.LastName;
            tbType.Text = viewUser.IsWorker ? "Bank Worker" : "Customer";
            userAccounts=ServiceClient.GetBankAccountsByUser(viewUser);
            tbTotalNet.Text = userAccounts==null? "": userAccounts.Sum(a => a.balance).ToString();
            cmbAccounts.ItemsSource= userAccounts;
        }

        private void ViewAccount_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            Customers customer= ServiceClient.GetCustomerByUser(viewUser);
            window.Content = new UserControlAccount(viewUser,userAccounts, customer);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.WindowStyle = WindowStyle.SingleBorderWindow;
            window.Width = 1250;
            window.Height = 850;
            window.ShowDialog();
        }

        private void UserSettings_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Content = new UserControlSettings(viewUser);
            window.WindowStartupLocation= WindowStartupLocation.CenterScreen;
            window.WindowStyle = WindowStyle.ToolWindow;
            window.Width = 1250;
            window.Height = 850;
            window.ShowDialog();
        }
        private void BankAcount_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Content = new WorkerBankControl(viewUser);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.WindowStyle = WindowStyle.ToolWindow;
            window.Width = 1250;
            window.Height = 850;
            window.ShowDialog();
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {

        }


        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {

            User user = viewUser;
            BankAccountList list = ServiceClient.GetBankAccountsByUser(viewUser);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ServiceClient.DeleteBankAccount(list[i]);
                }
            }
            if (ServiceClient.GetCustomerByUser(user) != null)
            {
                ServiceClient.DeleteCustomers(ServiceClient.GetCustomerByUser(user));
            }
            ServiceClient.DeleteUser(user);

        }
        private void cmbAccounts_Selected(object sender, RoutedEventArgs e)
        {
            BankAccount account = cmbAccounts.SelectedItem as BankAccount;
            if (account == null) return;
            tbAccountNet.Text = account.balance.ToString();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = tbSearch.Text.ToLower().Trim();
            List<User> searchUsers= UserList.FindAll(u=>u.FirstName.ToLower().Contains(text) ||
            u.FirstName.ToLower().Contains(text) || u.realid.Contains(text)).ToList();
            Userslv.ItemsSource= searchUsers;
        }

        private void Type_Click(object sender, RoutedEventArgs e)
        {
            if (UserList == null) return;
            tbSearch.Text = string.Empty;
            if ((bool)rbAll.IsChecked)
                Userslv.ItemsSource = UserList;
            else if((bool)rbCustomer.IsChecked)
                Userslv.ItemsSource = UserList.FindAll(u=>u.IsWorker==false);
            else
                Userslv.ItemsSource = UserList.FindAll(u => u.IsWorker == true);
        }
    }
}
