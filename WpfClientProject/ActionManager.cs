using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClientProject.ServiceReferenceBank;

namespace WpfClientProject
{
    public class ActionManager
    {

        public static void ExcuteAction(AccountAction accountAction, BankAccount otherAccount)
        {
            double amount = accountAction.Amount;
            double commition = (amount*accountAction.Action.commissionTaken/100);
            ServiceBaseClient ServiceClient = new ServiceBaseClient();
            ServiceClient.InsertAccountAction(accountAction);
            //Other bank account
            
            //Commition command
            accountAction.Amount = commition;
            accountAction.Action = new MyAction() { Id = 7 };
            ServiceClient.InsertAccountAction(accountAction);

            //Deposit commition to bank
            accountAction.Amount = commition;
            accountAction.BankAccount = new BankAccount() { Id = 1 };
            accountAction.Action = new MyAction() { Id = 4 };
            ServiceClient.InsertAccountAction(accountAction);
            if (otherAccount != null)
            {
                accountAction.Amount = amount;
                accountAction.BankAccount = otherAccount;
                accountAction.Action = new MyAction() { Id = 4 };
                ServiceClient.InsertAccountAction(accountAction);
            }
        }
    }
}
