using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// Interaction logic for DropDownAdmin.xaml
    /// </summary>
    public partial class DropDownAdmin : UserControl
    {
        ServiceReferenceBank.ServiceBaseClient ServiceClient;
        User User1;
        public DropDownAdmin(User user)
        {
             User1 = user;
            InitializeComponent();
            UserName.Header = user.realid;
            ServiceClient = new ServiceReferenceBank.ServiceBaseClient();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            BankAccountList list = ServiceClient.GetBankAccountsByUser(User1);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ServiceClient.DeleteBankAccount(list[i]);
                }
            }
            if (ServiceClient.GetCustomerByUser(User1) != null)
            {
                ServiceClient.DeleteCustomers(ServiceClient.GetCustomerByUser(User1));
            }
            ServiceClient.DeleteUser(User1);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
        //    WorkerBankControl win = new WorkerBankControl(int.Parse(User1.realid));
        //    win.Show();
        }
    }
}
