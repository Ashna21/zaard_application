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
    
    public partial class BidItem
    {
        public string itemCategory { get; set; }
        public string itemName { get; set; }
        public string itemLink { get; set; }
        public string itemLocation { get; set; }
        public string itemDescription { get; set; }
        public string UserID { get; set; }
        public Nullable<bool> isDigital { get; set; }
        public int bidItemID { get; set; }
        public Nullable<bool> auctionStatus { get; set; }
        public Nullable<System.DateTime> auctionStart { get; set; }
        public Nullable<System.DateTime> auctionEnd { get; set; }
        public byte[] image { get; set; }
    
        public virtual Bid Bid { get; set; }
        public virtual User User { get; set; }
    }
}