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
    /// Interaction logic for AdminControl.xaml
    /// </summary>
    public partial class AdminControl : UserControl
    {
        ServiceReferenceBank.ServiceBaseClient ServiceClient;
        User user1;
        double networth;
        int idtodelete;
        public AdminControl(User user)
        {
            InitializeComponent();
            ServiceClient = new ServiceReferenceBank.ServiceBaseClient();
            UserList = ServiceClient.GetAllUsers();
            Userslv.ItemsSource = UserList;
            user1 = user;
            GetAllActions();
            cbIsWorker.Items.Add("Worker");
            cbIsWorker.Items.Add("User");
            cbIsWorker.Items.Add("Both");

        }
        private UserList UserList;

        private void Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AdminUserClick.Children.Clear();
            if (Userslv.SelectedIndex == -1) return;
            User user = Userslv.SelectedItem as User;
            DropDownAdmin dropDown = new DropDownAdmin(user);
            
            dropDown.Width = 200;
            dropDown.Height = 100;
            AdminUserClick.Children.Add(dropDown);            

        }
        public void GetAllActions()
        {

      
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           

            idtodelete = int.Parse(tbId.Text);
            //User user = ServiceClient.GetUserByRealId(idtodelete);
            //BankAccountList list = ServiceClient.GetBankAcouuntsByRealId(idtodelete);
            //if (list != null)
            //{
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        ServiceClient.DeleteBankAcouunt(list[i]);
            //    }
            //}
            //if (ServiceClient.GetCustomerByUser(user)!= null)
            //{
            //    ServiceClient.DeleteCustomers(ServiceClient.GetCustomerByUser(user));
            //}
            //ServiceClient.DeleteUser(user);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
          
        }

        private void cbIsWorker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbIsWorker.SelectedItem.ToString() == null)
            {
                Userslv.ItemsSource = UserList;
            }
            if (cbIsWorker.SelectedItem.ToString() == "User")
            {
                
                Userslv.ItemsSource = UserList.FindAll(u => u.IsWorker == false);
            }
            if (cbIsWorker.SelectedItem.ToString() == "Worker")
            {
                
                Userslv.ItemsSource = UserList.FindAll(u => u.IsWorker);
            }
            if (cbIsWorker.SelectedItem.ToString() == "Both")
            {
                
                Userslv.ItemsSource = UserList;
            }
        }
    }
}
