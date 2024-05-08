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
            if (source.actionName == "TransferFrom") {
                AccountAction action = new AccountAction();
                action.Action = source;
                action.BankAccount = ServiceClient.GetBankAccountsByNumber(int.Parse(BankNumFrom.Text));
                action.ToBankAcouunt = ServiceClient.GetBankAccountsByNumber(int.Parse(BankNumFrom.Text));

                action.Amount = int.Parse(tbAmount2.Text);
                action.TimaStamp = DateTime.Now;
                ActionList actionList = ServiceClient.GetAllActions();


                AccountAction ToBank = new AccountAction();
                ToBank.Action = actionList[4]; ;
                ToBank.BankAccount = ServiceClient.GetBankAccountsByNumber(5);

                ToBank.ToBankAcouunt = ServiceClient.GetBankAccountsByNumber(5);
                int num = int.Parse(tbAmount2.Text);
                double num2 = num / 100;
                ToBank.Amount = ((double.Parse(tbAmount2.Text) * 1.0) / 100)*1.0;
                ToBank.TimaStamp = DateTime.Now;

                AccountAction Toperson = new AccountAction();
                Toperson.Action = actionList[4];
                Toperson.BankAccount = ServiceClient.GetBankAccountsByNumber(int.Parse(cmbTarget2.Text));

                Toperson.ToBankAcouunt = ServiceClient.GetBankAccountsByNumber(int.Parse(cmbTarget2.Text));
                Toperson.Amount = double.Parse(tbAmount2.Text) - (double.Parse(tbAmount2.Text) / 100);
                Toperson.TimaStamp = DateTime.Now;

                // networth = NetWorthcucltour(int.Parse(BankNumFrom.Text));                

                if (bank.balance > double.Parse(tbAmount2.Text))
                {
                    ServiceClient.InsertAccountAction(Toperson);
                    ServiceClient.InsertAccountAction(action);
                    ServiceClient.InsertAccountAction(ToBank);
                    MessageBox.Show("Money transferd");

                }
                else
                {
                    MessageBox.Show("Not enough money " + networth + " this is your net worth");
                }




            }


        }

        public void GetAllActions()
        {
            cmbSource2.Items.Clear();
            ActionList actionList = ServiceClient.GetAllActions();
            actionList.RemoveAt(4);
            actionList.RemoveAt(0);
            actionList.RemoveAt(1);
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
            if (action.actionName == "TransferFrom")
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
