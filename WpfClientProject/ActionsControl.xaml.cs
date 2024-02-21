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
          networth =  NetWorthcucltour();
            GetAllActions();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            MyAction source = (MyAction)cmbSource2.SelectedItem;
            AccountAction action = new AccountAction();
            action.Action = source;
            action.BankAccount = ServiceClient.GetBankAccount(user1);

            action.toBankAcouunt = ServiceClient.GetBankAcouuntByNum(int.Parse(cmbTarget2.Text));
            action.Amount = int.Parse(tbAmount2.Text);
            action.TimaStamp = DateTime.Now;
            networth = 1000;
            if (networth> double.Parse(tbAmount2.Text))
            {

                ServiceClient.Insertintoacountaction(action);
                NetWorthcucltour();
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
    }
}
