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
    
    public partial class ledger
    {
        public long id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string alt_name { get; set; }
        public Nullable<long> parent_id { get; set; }
        public Nullable<long> chart_of_account_id { get; set; }
        public Nullable<long> organization_id { get; set; }
        public Nullable<long> cost_centre_id { get; set; }
        public Nullable<long> currency_id { get; set; }
        public Nullable<bool> apply_cost_centre { get; set; }
        public Nullable<bool> allow_sub_ledger { get; set; }
        public Nullable<bool> apply_billwise { get; set; }
        public Nullable<bool> apply_interest { get; set; }
        public Nullable<bool> apply_tax { get; set; }
        public Nullable<bool> apply_tds { get; set; }
        public Nullable<bool> apply_tcs { get; set; }
        public Nullable<bool> apply_service_tax { get; set; }
        public Nullable<int> position { get; set; }
        public Nullable<double> opening_balance { get; set; }
        public Nullable<int> opening_cr_dr { get; set; }
        public Nullable<double> current_balance { get; set; }
        public Nullable<int> current_cr_dr { get; set; }
        public string photo { get; set; }
        public string remote_photo_url { get; set; }
        public string notes { get; set; }
        public Nullable<bool> system { get; set; }
        public Nullable<bool> managed { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<bool> archived { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<int> updated_by { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
    }
}