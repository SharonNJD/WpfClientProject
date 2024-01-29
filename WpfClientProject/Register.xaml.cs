using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using WpfClientProject.ServiceReferenceBank;

namespace WpfClientProject
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
     
    public partial class Register : Window
    {
        ServiceReferenceBank.ServiceBaseClient ServiceClient;
        bool passOk;
        User user;
        public Register()
        {

            InitializeComponent();
            passOk = false;
            user = new User();
            this.DataContext = user;
            addDates();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //birthdayDatePicker.Visibility = Visibility.Visible;
        }
        private void addDates()
        {
            DayComboBox.Items.Clear();
            MonthComboBox.Items.Clear();
            YearComboBox.Items.Clear();
            for (int i = 1907; i < DateTime.Now.Year; i++)
            {
                YearComboBox.Items.Add(i);
            }
            for (int i = 1; i < 13; i++)
            {
                MonthComboBox.Items.Add(i);
            }
            for (int i = 1; i < 32; i++)
            {
                DayComboBox.Items.Add(i);
            }
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            ServiceClient = new ServiceBaseClient();
            user.FirstName = tbxFirstName.Text;
            user.LastName = tbxLastName.Text;
            user.realid = tbxId.Text;
            user.Password = pbPass.Password;
            user.Email = tbxEmail.Text;
            user.Phonenum = tbxPhoneNum.Text;
            user.Gender = ((string)((ComboBoxItem)yesNoComboBox.SelectedItem).Content == "Male");// male - true
            int day = int.Parse(DayComboBox.Text);
            int month = int.Parse(MonthComboBox.Text);
            int year = int.Parse(YearComboBox.Text);                                 // user.Birthday = DateTime.Parse(birthdayDatePicker.SelectedDate.ToString());
            DateTime time = new DateTime(year, month, day);
           

            user.Birthday = time;
            ServiceClient.InsertUser(user);
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
            
        }

        private void Passwor_Changed(object sender, RoutedEventArgs e)
        {
            tbPass.Text = pbPass.Password;
            VaildPassword vaildPassword = new VaildPassword();
            ValidationResult result = vaildPassword.Validate(pbPass.Password, null);
            if(!result.IsValid)
            {
                pbPass.BorderBrush = Brushes.Red;
                pbPass.BorderThickness = new Thickness(2);
                pbPass.ToolTip=result.ErrorContent;
                passOk = false;
            }
            else
            {
                pbPass.BorderBrush = Brushes.Transparent;
                pbPass.BorderThickness = new Thickness(0);
                pbPass.ToolTip = null;
                passOk = true;
            }
        }
    }
}
