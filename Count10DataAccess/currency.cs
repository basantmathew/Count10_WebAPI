//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Count10DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class currency
    {
        public long id { get; set; }
        public string name { get; set; }
        public string alt_name { get; set; }
        public string iso_code { get; set; }
        public Nullable<int> iso_number { get; set; }
        public Nullable<int> fractionals { get; set; }
        public string fractionals_name { get; set; }
        public string display_name { get; set; }
        public string placement { get; set; }
        public string notes { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<bool> archived { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> updated_by { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
    }
}
