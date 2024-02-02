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
    public partial class NewUserPage : Window
    {
        User user;
        public NewUserPage(User us)
        {
            
            InitializeComponent();
            user = us;
            DataContext = us;
        }
        private void ButtonCloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Trade_Load(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(new UserControl1(user));
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            

        }

        private void ButtonCloseApp_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(new userusercontrol(user));
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(new ActionsControl());
            GridMain.Visibility = Visibility.Visible;
            this.ButtonClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
