﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        BankAccountList BankList = new BankAccountList();
        public userusercontrol(User user)
        {
            InitializeComponent();
            user1 = user;
            Createcostomer.Visibility = Visibility.Visible;
            Createcus.Visibility = Visibility.Visible;

            UserName.Text = user.FirstName + " " + user.LastName;
            ServiceClient = new ServiceReferenceBank.ServiceBaseClient();
            refreshCMB();

            NetWorthcucltour();
            if (ServiceClient.GetCustomerByUser(user) != null)
            {
                Createcostomer.Visibility = Visibility.Collapsed;
                Createcus.Visibility = Visibility.Collapsed;
            }


        }
        public void refreshCMB()
        {
            BankList = ServiceClient.GetAllBankAcouuntsByUser(user1);
            BankNum2_Copy.ItemsSource = BankList;
            BankNum2_Copy.DisplayMemberPath = "bankAcuuntNum";
            BankNum2.ItemsSource = BankList;
            BankNum2.DisplayMemberPath = "bankAcuuntNum";
        }

        private void Createcostomer_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceClient.GetCustomerByUser(user1) == null)
            {
                Customers customer = new Customers();
                customer.User = user1;
                customer.isNative = true;
                customer.dateOfJoining = DateTime.Now;

                ServiceClient.InsertIntoCustomers(customer);
                customer = ServiceClient.GetCustomerByUser(user1);
                
            }
            else
            {
                MessageBox.Show("You have a customer account");
            }
            

        }
        private void NetWorthcucltour()
        {
            if (ServiceClient.GetBankAccount(user1) != null)
            {
                double newworth = 0;
                int id = ServiceClient.GetBankAccount(user1).bankAcuuntNum;
                AccountActionList accountActionsto = new AccountActionList();
                accountActionsto = ServiceClient.GetAccountActionByBankAcouunt(id, 1);
                AccountActionList accountActionsto2 = new AccountActionList();
                accountActionsto2 = ServiceClient.GetbankAcouuntthattransfer(id, 1);
                
                foreach (AccountAction accountAction in accountActionsto)
                {
                    newworth += accountAction.Amount;
                }
                foreach (AccountAction accountAction in accountActionsto2)
                {
                    newworth -= accountAction.Amount;
                }
                NetWorth.Text = newworth.ToString();


            }
           
        }

        private void createbank_Click(object sender, RoutedEventArgs e)
        {
            
            BankAccountList list = ServiceClient.GetAllBankAcouuntsByUser(user1);
            bool CanCreate = false;
            if ((list == null || list.Count<3))
            {
                CanCreate = true;

            }
            else
            {
                CanCreate= false;
            }
            if (ServiceClient.GetCustomerByUser(user1) != null)
            {
                
                if (CanCreate = true)
                {
                    BankAccount bank = new BankAccount();
                    Random rnd = new Random();
                    customer = ServiceClient.GetCustomerByUser(user1);


                    bank.secretCode = rnd.Next(1000, 9999);
                    bank.canloan = false;
                    bank.canTransferOverSeas = false;
                    bank.canTradeStocks = false;
                    if (user1.Birthday.Year > 2006)
                    {
                        bank.adultAcouunt = true;
                    }
                    else
                        bank.adultAcouunt = false;
                    bank.personalAcouunt = true;
                    bank.customer = customer;


                    ServiceClient.InsertIntoBankAcouunt(bank);
                    list = ServiceClient.GetAllBankAcouuntsByUser(user1);
                    SecretCode.Visibility = Visibility.Visible;
                    BankAcouuntnum.Visibility = Visibility.Visible;
                    SecretCode.Text = "Your secret code is " + list[list.Count - 1].secretCode.ToString();
                    BankAcouuntnum.Text = "Your Bank Acouunt Num is " + list[list.Count - 1].bankAcuuntNum.ToString();
                    refreshCMB();
                }
                else
                {
                    MessageBox.Show("you have the max amount of possible bank acouunts");
                }
            }
            
            

        }

        private void RquestToLoan_Click(object sender, RoutedEventArgs e)
        {
            Customers customer = ServiceClient.GetCustomerByUser(user1);
            // need to fix this and also need to make sure someone else dosent change other people account


            if (BankNum2.SelectedIndex == -1) return;
            BankAccount bankAccount = BankNum2.SelectedItem as BankAccount;


            BankAccount account = ServiceClient.GetBankAcouuntByNum(int.Parse(BankNum2.Text));
               

               
                
                    if (customer != null && account != null)
                    {
                        if (customer.dateOfJoining.AddDays(10) < DateTime.Now)
                        {
                            account.canloan = true;
                            ServiceClient.UpdateBankAcouunt(account);
                            MessageBox.Show("Sucsses");

                        }
                        else
                        {
                            MessageBox.Show("Customer was crated recently you need to wait more days");
                        }
                    }
                    else
                    {
                        MessageBox.Show("You arent a customer or you dont have a bank acouunt");
                    }
                
            
           
            
        }

        private void AdultAcouunt_Click(object sender, RoutedEventArgs e)
        {
            if (BankNum2.SelectedIndex == -1) return;
            BankAccount bankAccount = BankNum2.SelectedItem as BankAccount;
           
                

               
                
                
                    BankAccount account = ServiceClient.GetBankAcouuntByNum(int.Parse(BankNum2.Text));
                    if (user1.Birthday.Year < DateTime.Now.Year - 18)
                    {
                        account.adultAcouunt = true;
                        ServiceClient.UpdateBankAcouunt(account);

                    }
                    else
                    {
                        MessageBox.Show("to young");
                    }
                
            

        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if (BankNum2_Copy.SelectedIndex == -1) return;
            BankAccount bankAccount=BankNum2_Copy.SelectedItem as BankAccount;
                               
                    
                        double newworth = 0;
                        int id = int.Parse(BankNum2_Copy.Text);
                        AccountActionList accountActionsto = new AccountActionList();
                        accountActionsto = ServiceClient.GetAccountActionByBankAcouunt(id,1);
                        AccountActionList accountActionsto2 = new AccountActionList();
                        accountActionsto2 = ServiceClient.GetbankAcouuntthattransfer(id, 1);

                        foreach (AccountAction accountAction in accountActionsto)
                        {
                            newworth += accountAction.Amount;
                        }
                        foreach (AccountAction accountAction in accountActionsto2)
                        {
                            newworth -= accountAction.Amount;
                        }
                        NetWorth.Text = newworth.ToString();


                    
                

            }
        }
    }
    

