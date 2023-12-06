﻿using System;
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
using System.Windows.Shapes;

namespace WpfClientProject
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        bool passOk;
        public Register()
        {
            InitializeComponent();
            passOk = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            birthdayDatePicker.Visibility = Visibility.Visible;
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {

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
                pbPass.ToolTip=result.ErrorContent.ToString();
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
