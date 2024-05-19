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
using WpfClientProject.ServiceReferenceCurrency;

namespace WpfClientProject
{
    /// <summary>
    /// Interaction logic for AdminActionControl.xaml
    /// </summary>
    public partial class AdminActionControl : UserControl
    {
        private ServiceBaseClient ServiceClient;
        private CurrencyServiceClient CurrencyService;
        private ActionList myActions;
        private CurrencyList currencyList;
        public AdminActionControl(User user)
        {
            InitializeComponent();
            ServiceClient= new ServiceBaseClient();
            myActions=ServiceClient.GetAllActions();
            Actionslv.ItemsSource = myActions;

            CurrencyService = new CurrencyServiceClient();
            currencyList = CurrencyService.GetAllCurrencies();
            cmbSymbols.ItemsSource = currencyList;
            cmbSymbols.DisplayMemberPath = "Key";
            for(double c=0;c<=20;c++)
                cmbCommision.Items.Add(c.ToString());
            for(int num=0;num<5;num++)
                cmbRank.Items.Add(num.ToString());
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = tbSearch.Text.ToLower().Trim();
            List<MyAction> searchResult = myActions.FindAll(u => u.actionName.ToLower().Contains(text) ||
            u.Id.Equals(text)).ToList();
            Actionslv.ItemsSource = searchResult;
        }

        private void Type_Click(object sender, RoutedEventArgs e)
        {
            if (myActions == null) return;
            tbSearch.Text = string.Empty;
            if ((bool)rbAll.IsChecked)
                Actionslv.ItemsSource = myActions;
            else if ((bool)rbAdding.IsChecked)
                Actionslv.ItemsSource = myActions.FindAll(u => u.adding == true);
            else
                Actionslv.ItemsSource = myActions.FindAll(u => u.adding == false);
        }

        private void Actionslv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSave.Content = "Save";
            MyAction action = Actionslv.SelectedItem as MyAction;
            if (action == null) return;
            spAction.DataContext = action;
            cmbRank.SelectedValue = action.minRank.ToString();
            cmbCommision.SelectedItem = ((double)action.commissionTaken).ToString();
            Currency currency = currencyList.Find(c => (c != null && c.Key != null && c.Key.Equals(action.coinSymbol)));
            if (currency != null)
                cmbSymbols.SelectedItem = currency.Symbol;
            else
                cmbSymbols.Text = string.Empty;
        }

        private void Excute_Click(object sender, RoutedEventArgs e)
        {
            if(tbName.Text.Trim().Equals(string.Empty) || cmbCommision.SelectedIndex==-1 || cmbRank.SelectedIndex==-1)
            {
                MessageBox.Show("Error/s found!", "Error", MessageBoxButton.OK);
                return;
            }
            if(cbForeign.IsChecked == true && cmbSymbols.SelectedIndex == -1)
            {
                MessageBox.Show("Select coin/s found!", "Error", MessageBoxButton.OK);
                return;
            }
            MyAction action = spAction.DataContext as MyAction;
            action.minRank=int.Parse(cmbRank.SelectedValue.ToString());
            action.commissionTaken=double.Parse(cmbCommision.SelectedValue.ToString());
            action.coinSymbol = string.Empty;
            if (cbForeign.IsChecked == true)
                action.coinSymbol= currencyList.Find(c => (c.Key != null && c.Key.Equals(cmbSymbols.Text))).Key; 
            if(btnSave.Content.Equals("Save"))
                ServiceClient.UpdateAction(action);
            else
                ServiceClient.InsertAction(action);
            myActions = ServiceClient.GetAllActions();
            Actionslv.ItemsSource = myActions;
        }

        private void NewAction_Click(object sender, RoutedEventArgs e)
        {
            MyAction action = new MyAction();
            spAction.DataContext = action;
            cmbRank.SelectedIndex = 0;
            cmbCommision.SelectedIndex = 0;
            cmbSymbols.SelectedIndex = 0;
            tbName.Focus();
            btnSave.Content = "Create";
        }
    }
}
