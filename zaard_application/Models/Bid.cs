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
    
    public partial class Bid
    {
        public int bidAmount { get; set; }
        public System.DateTime bidTime { get; set; }
        public int userID { get; set; }
        public int bidItemID { get; set; }
    
        public virtual BidItem BidItem { get; set; }
        public virtual User User { get; set; }
    }
}
