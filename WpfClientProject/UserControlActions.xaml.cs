using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UserControlActions.xaml
    /// </summary>
    public partial class UserControlActions : UserControl
    {
        private ServiceBaseClient ServiceClient;
        private User user;
        private BankAccount mainBankAccount;
        private BankAccountList BankList;
        private ActionList actions;
        public UserControlActions(User user, BankAccountList banks)
        {
            InitializeComponent();
            this.user = user;
            ServiceClient = new ServiceBaseClient();
            GetAllActions();
            mainBankAccount= ServiceClient.GetBankAccountsByNumber(1);
            BankList = ServiceClient.GetBankAccountsByUser(user);
            cmbBankAccounts.ItemsSource = BankList;
            cmbBankAccounts.DisplayMemberPath = "bankAcuuntNum";
            if(BankList!= null) 
                cmbBankAccounts.SelectedIndex = 0;
            cmbActions.DisplayMemberPath = "actionName";
            cmbActions.ItemsSource = actions=ServiceClient.GetAllActions();
        }

        public void GetAllActions()
        {
            cmbActions.Items.Clear();
            ActionList actionList = ServiceClient.GetAllActions();
            cmbActions.ItemsSource = actionList;
            cmbActions.DisplayMemberPath = "actionName";
        }
        private void Excute_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBankAccounts.SelectedIndex == -1 || cmbActions.SelectedIndex == -1 || tbAmount.Text.Equals(string.Empty))
            {
                MessageBox.Show("Missimg data!\nCheck youself", "Error", MessageBoxButton.OK);
                return; 
            }
            try
            {
                BankAccount otherAccount = null;
                double num = double.Parse(tbAmount.Text);
                AccountAction accountAction = new AccountAction()
                {
                    Action = cmbActions.SelectedItem as MyAction,
                    Amount = double.Parse(tbAmount.Text),
                    BankAccount = cmbBankAccounts.SelectedItem as BankAccount,
                    TimaStamp = DateTime.Now
                };
                if (cmbActions.Text.Equals("Transfer To"))
                {
                    otherAccount = new BankAccount();
                }
                ActionManager.ExcuteAction(accountAction, otherAccount);
                cmbActions.SelectedItem = -1;
                tbAmount.Text= string.Empty;
                cmbBankAccounts_SelectionChanged(null,null);
            }
            catch (Exception)
            {
                MessageBox.Show("Amount is not a valid number!", "Error", MessageBoxButton.OK);
            }
        }
        private void cmbBankAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BankAccount account=cmbBankAccounts.SelectedItem as BankAccount;
            AccountActionList actions = ServiceClient.GetAccountActionByBankAccount(account);
            Actionslv.ItemsSource= actions;
            tbAccountToAcction.Text= "Account: "+account.bankAcuuntNum.ToString()+"  ";
        }
    }
}
