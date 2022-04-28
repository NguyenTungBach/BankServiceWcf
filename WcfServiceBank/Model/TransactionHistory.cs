using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfServiceBank.Model
{
    [DataContract]
    public class TransactionHistory
    {
        public TransactionHistory()
        {

        }
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public int SenderAccountNumber { get; set; } // (string FK from Account): ai gửi đến
        [DataMember]
        public int ReceiverAccountNumber { get; set; } // (string FK from Account): ai nhận tiền
        [DataMember]
        public int Type { get; set; } // withdraw (1), deposit (2), transfer (3)
    }
}