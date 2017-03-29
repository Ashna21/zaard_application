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
    
    public partial class BuyItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BuyItem()
        {
            this.Reviews = new HashSet<Review>();
        }
    
        public int buyItemID { get; set; }
        public string stock { get; set; }
        public decimal price { get; set; }
        public Nullable<bool> released { get; set; }
        public string itemType { get; set; }
        public string genre { get; set; }
        public Nullable<bool> fiction { get; set; }
        public string minAge { get; set; }
        public string itemName { get; set; }
        public string itemLink { get; set; }
        public string itemLocation { get; set; }
        public string itemDescription { get; set; }
        public int supplierID { get; set; }
        public Nullable<bool> isDigital { get; set; }
        public byte[] image { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
