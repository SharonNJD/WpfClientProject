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
    /// Interaction logic for UserControlBanks.xaml
    /// </summary>
    public partial class UserControlBanks : Window
    {
        ServiceReferenceBank.ServiceBaseClient ServiceClient;
        BankAccountList bankAccountList;
        public UserControlBanks(User user)
        {
            InitializeComponent();
            ServiceClient = new ServiceReferenceBank.ServiceBaseClient();
            bankAccountList = ServiceClient.GetBankAccountsByUser(user);
            Banks.ItemsSource = bankAccountList;
            
            RefreshCMB();
        }
        public void RefreshCMB()
        {

            tbBankNum.ItemsSource = bankAccountList;
            tbBankNum.DisplayMemberPath = "bankAcuuntNum";
            tbBankNum.SelectedIndex = 0;
            
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
