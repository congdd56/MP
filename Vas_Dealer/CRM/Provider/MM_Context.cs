using Microsoft.EntityFrameworkCore;
using System;

namespace VAS.Dealer.Provider
{
    public partial class MM_Context : DbContext
    {
        #region DBSETs
        public DbSet<MM_OnlineUser> OnlineUser { get; set; }
        public DbSet<MM_ActiveUser> ActiveUser { get; set; }
        public DbSet<MM_Customer> Customer { get; set; }
        public DbSet<MM_CheckMPLoadElSerialModel> CheckMPLoadElSerialModel { get; set; }
        #endregion

        #region CONTRUCTOR
        public MM_Context(DbContextOptions options) : base(options)
        {
        }
        #endregion
    }

    #region Entities
    public class MM_OnlineUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string StatusValue { get; set; }
        public string Areas { get; set; }
        public string PermisManager { get; set; }
        public DateTime LoginDate { get; set; }
    }

    public class MM_ActiveUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Extension { get; set; }
    }

    public class MM_Customer
    {
        public Guid Id { get; set; }
        public string NumSeqRetailCM { get; set; }
        public string PHONE { get; set; }
        public string SEGMENTID { get; set; }
        public string STREET { get; set; }
        public string SUBSEGMENTID { get; set; }
        public string RetailCMName { get; set; }
        public string SALESDISTRICTID { get; set; }
        public string RetailCMTyple { get; set; }
    }

    public class MM_CheckMPLoadElSerialModel
    {
        public Guid Id { get; set; }
        public string Item { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }

        public bool Found { get; set; }
        public bool RequiredDoc { get; set; }
        public bool RequestDoc { get; set; }
        public bool IsExpired { get; set; }
        public string PurchaseDate { get; set; }
        public string ExpiredDate { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Trường hợp tìm thấy ở MPLoadEWarrantyView
        /// </summary>
        public bool IsFound_MPLoadEWarrantyView { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    #endregion
}