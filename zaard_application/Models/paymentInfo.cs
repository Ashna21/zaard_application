//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace zaard_application.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class paymentInfo
    {
        public int userID { get; set; }
        public int paymentID { get; set; }
        public string nameOnCard { get; set; }
        public string cardNum { get; set; }
        public string cardType { get; set; }
        public string expiration { get; set; }
        public string cvv { get; set; }
        public Nullable<System.DateTime> timeOfPurchase { get; set; }
    
        public virtual User User { get; set; }
    }
}
