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
    
    public partial class item
    {
        public long id { get; set; }
        public string name { get; set; }
        public string alt_name { get; set; }
        public string print_name { get; set; }
        public Nullable<long> parent_id { get; set; }
        public Nullable<long> organization_id { get; set; }
        public Nullable<long> category_id { get; set; }
        public Nullable<long> currency_id { get; set; }
        public Nullable<long> tax_group_id { get; set; }
        public Nullable<long> supplier_id { get; set; }
        public Nullable<long> uom_id { get; set; }
        public Nullable<bool> allow_descendants { get; set; }
        public Nullable<bool> @virtual { get; set; }
        public Nullable<bool> inventoriable { get; set; }
        public string grade { get; set; }
        public string brand { get; set; }
        public string ean_code { get; set; }
        public string hsn_code { get; set; }
        public Nullable<bool> pack_item { get; set; }
        public Nullable<double> standard_cost { get; set; }
        public Nullable<double> standard_price { get; set; }
        public string photo { get; set; }
        public string remote_photo_url { get; set; }
        public string properties { get; set; }
        public string taxes { get; set; }
        public string notes { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<bool> archived { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> updated_by { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
    }
}
