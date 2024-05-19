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
    /// Interaction logic for WorkerBankControl.xaml
    /// </summary>
    public partial class WorkerBankControl : UserControl
    {
        private ServiceBaseClient ServiceClient;
        private BankAccountList bankAccountList;
        private User user;
        private Customers customer;
        public WorkerBankControl(User user)
        {
            InitializeComponent();
            ServiceClient = new ServiceBaseClient();
            bankAccountList = ServiceClient.GetAllBankAccounts();
            BanksLv.ItemsSource= bankAccountList;
            this.user= user;            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceClient.GetBankAccountsByNumber(int.Parse(tbBankNum.Text)) != null)
            {
                ServiceClient.DeleteBankAccount(ServiceClient.GetBankAccountsByNumber(int.Parse(tbBankNum.Text)));
            }
            
        }
    }
}
