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
            
            Currency target = cmbTarget.SelectedItem as Currency;
            Action source = cmbSource2.SelectedItem as Action;
            AccountAction action = new AccountAction { BankAccount = null, Action = null, Amount = 0, TimaStamp = DateTime.Now };
            ServiceClient.Insertintoacountaction(action);
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
        public void GetAllActions()
        {
            cmbSource2.Items.Clear ();
            ActionList actionList = ServiceClient.GetAllActions();
            cmbSource2.ItemsSource = actionList ;
            cmbSource2.DisplayMemberPath = "actionName";



        }
    }
}
