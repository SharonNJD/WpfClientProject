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
            double commition = accountAction.Action.commissionTaken*amount;
            ServiceBaseClient ServiceClient = new ServiceBaseClient();
            ServiceClient.InsertAccountAction(accountAction);
            //Other bank account
            if (otherAccount != null)
            {
                accountAction.Amount = accountAction.Amount;
                accountAction.BankAccount = otherAccount;
                accountAction.Action = new MyAction() { Id = 4 };
                ServiceClient.InsertAccountAction(accountAction);
            }
            //Commition command
            accountAction.Amount = commition;
            accountAction.Action = new MyAction() { Id = 4 };
            ServiceClient.InsertAccountAction(accountAction);

            //Deposit commition to bank
            accountAction.Amount = commition;
            accountAction.BankAccount = new BankAccount() { Id = 1 };
            accountAction.Action = new MyAction() { Id = 2 };
            ServiceClient.InsertAccountAction(accountAction);
        }
    }
}
