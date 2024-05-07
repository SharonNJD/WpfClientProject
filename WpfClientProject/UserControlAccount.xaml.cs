using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfClientProject.ServiceReferenceBank;

namespace WpfClientProject
{
    /// <summary>
    /// Interaction logic for userusercontrol.xaml
    /// </summary>
    public partial class UserControlAccount : UserControl
    {
        ServiceBaseClient ServiceClient;
        User user;
        Customers customer;
        BankAccountList BankList;
        BankAccount currentAccount;
        AccountActionList actions;
        public UserControlAccount(User user, BankAccountList list, Customers customer)
        {
            InitializeComponent();
            this.user = user;
            UserName.Text = user.FirstName + " " + user.LastName;
            ServiceClient = new ServiceBaseClient();
            CreateCostomer.Visibility = Visibility.Collapsed;
            BankList = list;
            RefreshCMB();
            this.customer = customer;
            if(customer == null)
            {
                foreach(Button button in buttonsPanel.Children)
                    button.Visibility = Visibility.Collapsed;
                CreateCostomer.Visibility=Visibility.Visible;
            }
        }
        public void RefreshCMB()
        {
            
            cmbBankAccounts.ItemsSource = BankList;
            cmbBankAccounts.DisplayMemberPath = "bankAcuuntNum";
            cmbBankAccounts.SelectedIndex = 0;
            currentAccount = BankList[0];
        }
        private void Createcostomer_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceClient.GetCustomerByUser(user) == null)
            {
                Customers customer = new Customers();
                customer.User = user;
                customer.isNative = true;
                customer.dateOfJoining = DateTime.Now;

                ServiceClient.InsertCustomers(customer);
                customer = ServiceClient.GetCustomerByUser(user);
            }
            else
            {
                MessageBox.Show("You have a customer account");
            }
        }
        private void GetAccountActions()
        {
            actions=ServiceClient.GetAccountActionByBankAccount(currentAccount);
            if(actions.Count ==0) { tbLastAction.Text = "No actions"; return; }
            AccountAction action = actions.Last();
            tbLastAction.Text = action.Action.actionName+": "+ action.Amount+"💸, "+action.TimaStamp.ToString("dd/MM/yyyy HH:mm");
        }

        private void Createbank_Click(object sender, RoutedEventArgs e)
        {
            BankAccountList list = ServiceClient.GetBankAccountsByUser(user);
            bool CanCreate = false;
            if (list == null || list.Count < 3)
            {
                CanCreate = true;
            }
            if (ServiceClient.GetCustomerByUser(user) != null)
            {

                if (CanCreate == true)
                {
                    BankAccount bank = new BankAccount();
                    Random rnd = new Random();
                    customer = ServiceClient.GetCustomerByUser(user);

                    bank.secretCode = rnd.Next(1000, 9999);
                    bank.canloan = false;
                    bank.canTransferOverSeas = false;
                    bank.canTradeStocks = false;
                    if (user.Birthday.Year > 2006)
                    {
                        bank.adultAcouunt = true;
                    }
                    else
                        bank.adultAcouunt = false;
                    bank.personalAcouunt = true;
                    bank.customer = customer;


                    ServiceClient.InsertBankAccount(bank);
                    list = ServiceClient.GetBankAccountsByUser(user);
                    SecretCode.Visibility = Visibility.Visible;
                    RefreshCMB();
                }
                else
                {
                    MessageBox.Show("you have the max amount of possible bank acouunts");
                }
            }
        }

        private void RquestLoan_Click(object sender, RoutedEventArgs e)
        {
            if (currentAccount==null) return;
            if (customer != null && currentAccount != null)
            {
                if (customer.dateOfJoining.AddDays(10) < DateTime.Now)
                {
                    currentAccount.canloan = true;
                    ServiceClient.UpdateBankAccount(currentAccount);
                    MessageBox.Show("Sucsses");
                }
                else
                {
                    MessageBox.Show("Customer was created recently you need to wait a few more days");
                }
            }
            else
            {
                MessageBox.Show("You aren't a customer or you don't have a bank acouunt");
            }
        }

        private void AdultAcouunt_Click(object sender, RoutedEventArgs e)
        {
            if (currentAccount == null) return;
            if (user.Birthday.Year < DateTime.Now.Year - 18)
            {
                currentAccount.adultAcouunt = true;
                ServiceClient.UpdateBankAccount(currentAccount);
            }
            else
            {
                MessageBox.Show("to young");
            }
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your secret code is " + currentAccount.secretCode.ToString(),"🔐",MessageBoxButton.OK);
        }

        private void ViewActions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbBankAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentAccount = cmbBankAccounts.SelectedItem as BankAccount;
            GetAccountActions();
            tbNetWorth.Text= currentAccount.balance.ToString();

        }
    }
}
