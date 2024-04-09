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
    /// Interaction logic for AdminControl.xaml
    /// </summary>
    public partial class AdminControl : UserControl
    {
        ServiceReferenceBank.ServiceBaseClient ServiceClient;
        User user1;
        double networth;
        int idtodelete;
        public AdminControl(User user)
        {
            InitializeComponent();
            ServiceClient = new ServiceReferenceBank.ServiceBaseClient();
            UserList = ServiceClient.GetAllUsers();
            Users.ItemsSource = UserList;
            user1 = user;
            GetAllActions();
            

        }
        private UserList UserList;

        private void Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Userslv.SelectedIndex == -1) return;
            User user = Userslv.SelectedItem as User;

        }
        public void GetAllActions()
        {
            cmbSource2.Items.Clear();
            ActionList actionList = ServiceClient.GetAllActions();
            cmbSource2.ItemsSource = actionList;
            cmbSource2.DisplayMemberPath = "actionName";



        }

        private double NetWorthcucltour()
        {
            double newworth = 0;


            if (ServiceClient.GetBankAccount(user1) != null)
            {

                int id = ServiceClient.GetBankAccount(user1).bankAcuuntNum;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MyAction source = (MyAction)cmbSource2.SelectedItem;
            AccountAction action = new AccountAction();
            action.Action = source;
            action.BankAccount = ServiceClient.GetBankAccount(user1);

            action.ToBankAcouunt = ServiceClient.GetBankAcouuntByNum(int.Parse(cmbTarget2.Text));
            action.Amount = int.Parse(tbAmount2.Text);
            action.TimaStamp = DateTime.Now;
            networth = 1000;
            if (networth > double.Parse(tbAmount2.Text))
            {

                ServiceClient.Insertintoacountaction(action);
                NetWorthcucltour();
            }
            else
            {
                MessageBox.Show("Not enough money " + networth + " this is your net worth");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           

            idtodelete = int.Parse(tbId.Text);
            User user = ServiceClient.GetUserByRealId(idtodelete);
            BankAccountList list = ServiceClient.GetBankAcouuntsByRealId(idtodelete);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ServiceClient.DeleteBankAcouunt(list[i]);
                }
            }
            if (ServiceClient.GetCustomerByUser(user)!= null)
            {
                ServiceClient.DeleteCustomers(ServiceClient.GetCustomerByUser(user));
            }
            ServiceClient.DeleteUser(user);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
          UserControlBanks win = new UserControlBanks(int.Parse(tbId1.Text));
            win.Show();
        }

    }
}
