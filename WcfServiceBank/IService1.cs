using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfServiceBank.Model;

namespace WcfServiceBank
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        Account CreateAccount(Account account);
        [OperationContract]
        Account FindAccount(int id);
        [OperationContract]
        Account Login(string user , string password);
        [OperationContract]
        TransactionHistory Deposit(int AccountNumber, double Amount,string Token); // 1. thực hiện gửi tiền
        [OperationContract]
        TransactionHistory Withdraw(int AccountNumber, double Amount, string Token); // 2. thực hiện rút tiền
        [OperationContract]
        TransactionHistory Transfer(int SenderAccountNumber, int ReceiverAccountNumber, double Amount, string Token); // 3. thực hiện chuyển tiền
        [OperationContract]
        List<Account> ListAccount();


        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
