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
using WpfClientProject.ServiceReferenceCurrency;

namespace WpfClientProject
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            
            InitializeComponent();
            currencyList = currencyService.GetAllCurrencies();
            lvCurrencies.ItemsSource = currencyList;

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
            Currency source = cmbSource.SelectedItem as Currency;
            Currency target = cmbTarget.SelectedItem as Currency;

            double amount = double.Parse(tbAmount.Text);
            double result = currencyService.Convert(source, target, amount);
            tbResult.Text = $"{amount} {source.Key} is {result} is {target.Key}";
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
    }
}
