﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPhongKham.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class qlpkEntities2 : DbContext
    {
        public qlpkEntities2()
            : base("name=qlpkEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BACSI> BACSIs { get; set; }
        public virtual DbSet<CHUYENKHOA> CHUYENKHOAs { get; set; }
        public virtual DbSet<GOIKHAM> GOIKHAMs { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<LICHKHACHHANG> LICHKHACHHANGs { get; set; }
        public virtual DbSet<LOAIGOIKHAM> LOAIGOIKHAMs { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
    }
}
