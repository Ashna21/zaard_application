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
    
    public partial class address
    {
        public int zipcode { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int addressID { get; set; }
        public string email { get; set; }
        public int UserID { get; set; }
    
        public virtual User User { get; set; }
    }
}
