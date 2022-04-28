using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace WcfServiceBank.Model
{
    [DataContract]
    public class Account
    {
        private static string test = DateTime.Now.ToString();
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountNumber { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public double Balance { get; set; } // số tiền mặc định là 50000
        [DataMember]
        [DefaultValue(1)]
        public int Status { get; set; } // trạng thái mặc định là 1
        [DataMember]
        public DateTime CreateAt { get; set; } // ngày mặc định là DateTime.Now
        [DataMember]
        public DateTime UpdateAt { get; set; } // ngày mặc định là DateTime.Now
        [DataMember]
        public DateTime DeleteAt { get; set; } // ngày mặc định là DateTime.Now

        

    }
}