using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        User user1;
        ServiceReferenceBank.ServiceBaseClient ServiceClient;
        public UserControl1(User user)
        {
            
            InitializeComponent();
            currencyList = currencyService.GetAllCurrencies();
            lvCurrencies.ItemsSource = currencyList;
            user1 = user;

            ServiceClient = new ServiceReferenceBank.ServiceBaseClient();
           BankAccountList moshe = ServiceClient.GetAllBankAccountList();
            GetAllActions();
            addListToComboBox();
        }
        private CurrencyServiceClient currencyService = new CurrencyServiceClient();
        private CurrencyList currencyList;


        

        private void tbAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.Text[0]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
         

            Currency s = cmbSource.SelectedItem as Currency;
            Currency t = cmbTarget.SelectedItem as Currency;
            double amount = double.Parse(tbAmount.Text);
            double result = currencyService.Convert(s, t, amount);
            tbResult.Text = $"{amount} {s.Key} is {result} is {t.Key}";





            
        }
       

        public void addListToComboBox()
        {
            cmbSource.Items.Clear();
            cmbTarget.Items.Clear();
            cmbTarget2.Items.Clear();
            cmbSource.DisplayMemberPath = cmbTarget.DisplayMemberPath = "Key";
            cmbSource.ItemsSource = currencyList;
            cmbTarget.ItemsSource = currencyList;
            cmbSource.DisplayMemberPath = cmbTarget2.DisplayMemberPath = "Key";
            cmbTarget2.ItemsSource = currencyList;

        }

        private void lvCurrencies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ForgienCoinCuc()
        {
            double newworth = 0;


            if (ServiceClient.GetBankAccount(user1) != null)
            {

                
                //AccountActionList accountActionsto = new AccountActionList();
                //accountActionsto = ServiceClient.GetAccountActionByBankAcouunt(id);
                //AccountActionList accountActionsto2 = new AccountActionList();
                //accountActionsto2 = ServiceClient.GetbankAcouuntthattransfer(id);
                //foreach (AccountAction accountAction in accountActionsto)
                //{
                //    newworth += accountAction.Amount;
                //}
                //foreach (AccountAction accountAction in accountActionsto2)
                //{
                //    newworth -= accountAction.Amount;
                //}



            }


        }
        public void GetAllActions()
        {
            cmbSource2.Items.Clear ();
            ActionList actionList = ServiceClient.GetAllActions();
            cmbSource2.ItemsSource = actionList ;
            cmbSource2.DisplayMemberPath = "actionName";



        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Currency target = cmbTarget.SelectedItem as Currency;
            MyAction source = (MyAction)cmbSource2.SelectedItem;
            AccountAction action = new AccountAction();
            action.Action = source;
            action.BankAccount = ServiceClient.GetBankAccount(user1);
            
            action.ToBankAcouunt = ServiceClient.GetBankAcouuntByNum(int.Parse(ToBank.Text));
            action.Amount = int.Parse(tbAmount2.Text);
            action.TimaStamp = DateTime.Now;
            
            ServiceClient.Insertintoacountaction(action);

        }
    }
}
