using VAS.Dealer.Models.Entities.CIC;
using VAS.Dealer.Models.Entities.CIC.Store;
using Microsoft.EntityFrameworkCore;
using VAS.Dealer.Models.VOC;

namespace VAS.Dealer.Services
{
    public partial class CICContext : DbContext
    {
        public CICContext(DbContextOptions<CICContext> options)
              : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CC_Group>(e =>
            {
                e.ToTable("CC_Group").HasKey(k => k.GroupId);
                e.Property(p => p.GroupId).ValueGeneratedOnAdd();
                e.HasMany(j => j.CC_Account_Group).WithOne(j => j.CC_Group).HasForeignKey(j => j.GroupId);
            });

            modelBuilder.Entity<CC_CallDetail>(e =>
            {
                e.ToTable("CallDetail").HasKey(k => k.CallId);
                e.Property(p => p.CallId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CC_RecordingCall>(e =>
            {
                e.ToTable("RecordingCall").HasKey(k => k.RECORDINGID);
                e.Property(p => p.RECORDINGID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CC_RecordingData>(e =>
            {
                e.ToTable("RecordingData").HasKey(k => k.RecordingId);
                e.Property(p => p.RecordingId).ValueGeneratedOnAdd();
            });


            modelBuilder.Entity<CC_Account_Group>(e =>
            {
                e.ToTable("CC_Account_Group").HasKey(sc => new { sc.GroupId, sc.AccountId });
                e.HasOne<CC_Group>(sc => sc.CC_Group).WithMany(s => s.CC_Account_Group).HasForeignKey(sc => sc.GroupId);
            });
            modelBuilder.Entity<VIP_BLACK_List>(e =>
            {
                e.ToTable("VIP_BLACK_List")
                .HasKey(k => k.PhoneNumber);


            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


        #region Store
        /// <summary>
        /// Báo cáo năng suất điện thoại viên
        /// </summary>
        public virtual DbSet<RP_Agent_Productivity> Agent_Productivity { get; set; }
        /// <summary>
        /// Báo cáo cuộc gọi nhỡ
        /// </summary>
        public virtual DbSet<RP_Agent_MissCall> Agent_MissCall { get; set; }
        /// <summary>
        /// Tìm kiếm file ghi âm
        /// </summary>
        public virtual DbSet<RP_2021_GetRecording> RP_2021_GetRecording { get; set; }
        /// <summary>
        /// Báo cáo thời gian đăng nhập CIC
        /// </summary>
        public virtual DbSet<RP_2021_AGENT_CICLogin> RP_2021_AGENT_CICLogin { get; set; }
        /// <summary>
        /// Báo cáo Chi tiết inbound
        /// </summary>
        public virtual DbSet<ReportIBCallDetails> ReportCallDetailsIB { get; set; }
        /// <summary>
        /// Báo cáo chi tiết gọi ra
        /// </summary>
        public virtual DbSet<RP_CIC_2021_OBDetails> RP_CIC_2021_OBDetails { get; set; }
        /// <summary>
        /// Báo cáo năng suất inbound
        /// </summary>
        public virtual DbSet<RP_IB_Productivity> IB_Productivity { get; set; }
        /// <summary>
        /// Báo cáo tổng hợp inbound
        /// </summary>
        public virtual DbSet<RP_2021_IBTotal> RP_2021_IBTotal { get; set; }
        /// <summary>
        /// Báo cáo xu hướng
        /// </summary>
        public virtual DbSet<RP_2021_IBCallTrend> RP_2021_IBCallTrend { get; set; }
        /// <summary>
        /// Báo cáo hoạt động agent
        /// </summary>
        public virtual DbSet<RP_2021_Agent_Active> RP_2021_Agent_Active { get; set; }
        /// <summary>
        /// Báo cáo chi tiết trạng thái agent
        /// </summary>
        public virtual DbSet<RP_2021_Agent_StatusDetails> RP_2021_Agent_StatusDetails { get; set; }
        


        #endregion

        #region Tables
        public virtual DbSet<CC_Group> Group { get; set; }
        public virtual DbSet<CC_Account_Group> AccountGroup { get; set; }
        public virtual DbSet<CC_CallDetail> CallDetail { get; set; }
        public virtual DbSet<CC_RecordingCall> RecordingCall { get; set; }
        public virtual DbSet<CC_RecordingData> RecordingData { get; set; }
        public virtual DbSet<VIP_BLACK_List> VIP_BLACK_Lists { get; set; }
        #endregion

    }
}
