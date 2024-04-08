using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionsControl(User user)
        {

            InitializeComponent();
            user1 = user;
            ServiceClient = new ServiceReferenceBank.ServiceBaseClient();
          
            GetAllActions();
            Howmuch.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Howmuch.Visibility = Visibility.Visible;
            double Amount = int.Parse(tbAmount2.Text);
            Amount = Amount + Amount / 100;
            Howmuch.Text = "You will have to pay" + Amount.ToString();
            Yes.Visibility = Visibility.Visible;
            No.Visibility = Visibility.Visible;
            
        }
        public void Transfer()
        {
            bool isThebankForm = false;
            bool isTheBankTo = false;
            if (Regex.IsMatch(cmbFrom2.Text, @"^[0-9]+$"))
            {


                BankAccount account = ServiceClient.GetBankAcouuntByNum(int.Parse(cmbFrom2.Text));
                BankAccountList bankAccounts = ServiceClient.GetAllBankAcouuntsByUser(user1);
                

                for (int i = 0; i < bankAccounts.Count; i++)
                {

                    if (bankAccounts[i].bankAcuuntNum == int.Parse(cmbFrom2.Text))
                    {
                        isThebankForm = true;
                    }
                }
            }
            if (Regex.IsMatch(cmbTarget2.Text, @"^[0-9]+$"))
            {


                BankAccount account = ServiceClient.GetBankAcouuntByNum(int.Parse(cmbTarget2.Text));
                BankAccountList bankAccounts = ServiceClient.GetAllBankAccountList();


                for (int i = 0; i < bankAccounts.Count; i++)
                {

                    if (bankAccounts[i].bankAcuuntNum == int.Parse(cmbTarget2.Text))
                    {
                        isTheBankTo = true;
                    }
                }
            }
            else { MessageBox.Show("Wrong Input From It must be only numbers"); }
            if (isThebankForm && isTheBankTo)
            {
                MyAction source = (MyAction)cmbSource2.SelectedItem;
                AccountAction action = new AccountAction();
                action.Action = source;
                action.BankAccount = ServiceClient.GetBankAccount(user1);

                action.ToBankAcouunt = ServiceClient.GetBankAcouuntByNum(int.Parse(cmbTarget2.Text));
                action.Amount = int.Parse(tbAmount2.Text);
                action.TimaStamp = DateTime.Now;

                AccountAction ToBank = new AccountAction();
                action.Action = source;
                action.BankAccount = ServiceClient.GetBankAccount(user1);

                action.ToBankAcouunt = ServiceClient.GetBankAcouuntByNum(5);
                action.Amount = int.Parse(tbAmount2.Text) / 100;
                action.TimaStamp = DateTime.Now;
                networth = NetWorthcucltour(int.Parse(cmbFrom2.Text));
                if (networth > double.Parse(tbAmount2.Text))
                {
                    ServiceClient.Insertintoacountaction(ToBank);
                    ServiceClient.Insertintoacountaction(action);
                    MessageBox.Show("Money transferd");
                    
                }
                else
                {
                    MessageBox.Show("Not enough money " + networth + " this is your net worth");
                }
            }
            else
            {
                if (!isThebankForm)
                {
                    MessageBox.Show("this is not your bank acouunt");
                }
                if (!isTheBankTo)
                {
                    MessageBox.Show("This bank acouunt dosent exist");
                }
                
            }
                
        }
        public void GetAllActions()
        {
            cmbSource2.Items.Clear();
            ActionList actionList = ServiceClient.GetAllActions();
            cmbSource2.ItemsSource = actionList;
            cmbSource2.DisplayMemberPath = "actionName";



        }
        private double NetWorthcucltour(int num)
        {
            double newworth = 0;
            
            
                if (ServiceClient.GetBankAccount(user1) != null)
                {
                    
                    int id = num;
                    AccountActionList accountActionsto = new AccountActionList();
                    accountActionsto = ServiceClient.GetAccountActionByBankAcouunt(id);
                    AccountActionList accountActionsto2 = new AccountActionList();
                    accountActionsto2 = ServiceClient.GetbankAcouuntthattransfer(id);
                    foreach (AccountAction accountAction in accountActionsto)
                    {
                        newworth += accountAction.Amount;
                    }
                    foreach (AccountAction accountAction in accountActionsto2)
                    {
                        newworth -= accountAction.Amount;
                    }
                   


                }

            
            return newworth;
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
    }
}
