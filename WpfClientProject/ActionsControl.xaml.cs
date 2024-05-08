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
    /// Interaction logic for ActionsControl.xaml
    /// </summary>
    public partial class ActionsControl : UserControl
    {
        ServiceReferenceBank.ServiceBaseClient ServiceClient;
        User user1;
        double networth;
        BankAccountList BankList = new BankAccountList();
        public ActionsControl(User user)
        {

            InitializeComponent();
            user1 = user;
            ServiceClient = new ServiceReferenceBank.ServiceBaseClient();
            refreshCMB();
            GetAllActions();
            Howmuch.Visibility = Visibility.Collapsed;
        }
        public void refreshCMB()
        {
            BankList = ServiceClient.GetBankAccountsByUser(user1);
            BankNumFrom.ItemsSource = BankList;
            BankNumFrom.DisplayMemberPath = "bankAcuuntNum";
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Howmuch.Visibility = Visibility.Visible;
            double Amount = double.Parse(tbAmount2.Text);
            Amount = Amount + Amount / 100;
            Howmuch.Text = "You will have to pay" + Amount.ToString();
            Yes.Visibility = Visibility.Visible;
            No.Visibility = Visibility.Visible;
            
        }
        public void Transfer()
        {
            
             BankAccount bank = BankNumFrom.SelectedItem as BankAccount; ;
            
            
                MyAction source = (MyAction)cmbSource2.SelectedItem;
            
                AccountAction action = new AccountAction();
                action.Action = source;
                action.BankAccount = ServiceClient.GetBankAccountsByNumber(int.Parse(BankNumFrom.Text));

                action.ToBankAcouunt = ServiceClient.GetBankAccountsByNumber(int.Parse(cmbTarget2.Text));
                action.Amount = int.Parse(tbAmount2.Text);
                action.TimaStamp = DateTime.Now;

                AccountAction ToBank = new AccountAction();
                ToBank.Action = source;
                ToBank.BankAccount = ServiceClient.GetBankAccountsByNumber(int.Parse(BankNumFrom.Text));

                ToBank.ToBankAcouunt = ServiceClient.GetBankAccountsByNumber(5);
                ToBank.Amount = int.Parse(tbAmount2.Text) / 100;
                ToBank.TimaStamp = DateTime.Now;
                AccountAction Form = new AccountAction();
                Form.Action = source;

               // networth = NetWorthcucltour(int.Parse(BankNumFrom.Text));                

                if (bank.balance > double.Parse(tbAmount2.Text))
                {
                    ServiceClient.InsertAccountAction(ToBank);
                    ServiceClient.InsertAccountAction(action);
                    MessageBox.Show("Money transferd");
                    
                }
                else
                {
                    MessageBox.Show("Not enough money " + networth + " this is your net worth");
                }
        
                
        }

        public void GetAllActions()
        {
            cmbSource2.Items.Clear();
            ActionList actionList = ServiceClient.GetAllActions();
            cmbSource2.ItemsSource = actionList;
            cmbSource2.DisplayMemberPath = "actionName";
        }
       
        private void No_Click(object sender, RoutedEventArgs e)
        {
            Yes.Visibility = Visibility.Collapsed;
            No.Visibility = Visibility.Collapsed;
            Howmuch.Visibility = Visibility.Collapsed;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Transfer();
        }

        private void cmbSource2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyAction action = (MyAction)cmbSource2.SelectedItem;
            if (action.actionName == "TransferTo")
            {
                To.Visibility = Visibility.Visible;
                cmbTarget2.Visibility = Visibility.Visible;
            }
            else
            {
                To.Visibility = Visibility.Hidden;
                cmbTarget2.Visibility = Visibility.Hidden;
            }
        }
    }
}
