using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfClientProject.ServiceReferenceBank;
using System.Windows.Controls.Primitives;
using System.Windows.Automation;



namespace WpfClientProject
{
    /// <summary>
    /// Interaction logic for NewUserPage.xaml
    /// </summary>
    public partial class WindowUser : Window
    {
        private User user;
        private Customers customer;
        private BankAccountList BankList;
        private ServiceBaseClient ServiceClient;
        public WindowUser(User us)
        {
            InitializeComponent();
            user = us;
            ServiceClient = new ServiceBaseClient();
            BankList = ServiceClient.GetBankAccountsByUser(user);
            customer = ServiceClient.GetCustomerByUser(user);
            DataContext = us;
            sepWorker.Visibility=tbWorker.Visibility =BankPage.Visibility = Visibility.Collapsed;
            UserPage.Visibility = ActionPage.Visibility = tbAdmin.Visibility =sepAdmin.Visibility= Visibility.Collapsed;
            if (user.IsWorker)
            {
                sepWorker.Visibility = tbWorker.Visibility = BankPage.Visibility = Visibility.Visible;
                if (user.IsWorker)
                    UserPage.Visibility = ActionPage.Visibility = tbAdmin.Visibility = sepAdmin.Visibility = Visibility.Visible;
            }
        }
        private void ButtonCloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Trade_Selected(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(new UserControlCurrency(user));
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void Account_Selected(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            UserControlAccount u = new UserControlAccount(user, BankList, customer);
            GridMain.Children.Add(u);
            u.Height = 600;
            u.Width = 1080;
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void Actions_Selected(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(new UserControlActions(user, BankList));
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void Setting_Selected(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(new UserControlSettings(user));
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }        
        private void Support_Selected(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(new UserControlSupport());
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void User_Selected(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(new AdminUserControl(user));
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void Action_Selected(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(new AdminActionControl(user));
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void BankPage_Selected(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(new WorkerBankControl(user));
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
