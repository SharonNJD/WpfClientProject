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
    /// Interaction logic for userusercontrol.xaml
    /// </summary>
    public partial class userusercontrol : UserControl
    {
        ServiceReferenceBank.ServiceBaseClient ServiceClient;
        User user1;
        Customers customer;

        public userusercontrol(User user)
        {
            InitializeComponent();
            user1 = user;
            
            UserName.Text = user.FirstName + " " + user.LastName;
            ServiceClient = new ServiceReferenceBank.ServiceBaseClient();

        }

        private void Createcostomer_Click(object sender, RoutedEventArgs e)
        {
            
            Customers customer = new Customers();
            customer.User = user1;
            customer.isNative = true;
            customer.dateOfJoining = DateTime.Now;

            ServiceClient.InsertIntoCustomers(customer);
            customer = ServiceClient.GetCustomerByUser(user1);

        }

        private void createbank_Click(object sender, RoutedEventArgs e)
        {
            BankAccount bank = new BankAccount();
            Random rnd = new Random();
            customer = ServiceClient.GetCustomerByUser(user1);


            bank.secretCode = rnd.Next(1000, 9999);
            bank.canloan = false;
            bank.canTransferOverSeas = false;
            bank.canTradeStocks = false;
            if (user1.Birthday.Year > 2006 )
            {
                bank.adultAcouunt = true;
            }
            else
            bank.adultAcouunt = false;
            bank.personalAcouunt = true;
            bank.customer = customer;
            

            ServiceClient.InsertIntoBankAcouunt(bank);
        }
    }
    }

