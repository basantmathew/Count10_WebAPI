﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Count10_DevEntities : DbContext
    {
        public Count10_DevEntities()
            : base("name=Count10_DevEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<currency> currencies { get; set; }
        public virtual DbSet<uom> uoms { get; set; }
        public virtual DbSet<chart_of_accounts> chart_of_accounts { get; set; }
        public virtual DbSet<region> regions { get; set; }
        public virtual DbSet<location> locations { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<ledger> ledgers { get; set; }
        public virtual DbSet<item> items { get; set; }
        public virtual DbSet<organization> organizations { get; set; }
    }
}
