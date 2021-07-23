using Microsoft.EntityFrameworkCore;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Models.VAS;

namespace VAS.Dealer.Services
{
    public partial class MP_Context : DbContext
    {
        public MP_Context(DbContextOptions<MP_Context> options)
              : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region MANAGER



            modelBuilder.Entity<MP_Role>(e =>
            {
                e.ToTable("MP_Role").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
                e.HasMany(j => j.RolePermission).WithOne(j => j.Role).HasForeignKey(j => j.IdRole);
                e.HasMany(j => j.AccountRole).WithOne(j => j.Role).HasForeignKey(j => j.RoleId);
            });
            modelBuilder.Entity<MP_AccountLoginTime>(e =>
            {
                e.ToTable("MP_AccountLoginTime").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MP_Permission>(e =>
            {
                e.ToTable("MP_Permission").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
                e.HasOne<MP_GroupPermission>(sc => sc.MP_GroupPermission).WithMany(s => s.MP_Permission)
               .HasForeignKey(sc => sc.GroupId);
            });
            modelBuilder.Entity<MP_Account>(e =>
            {
                e.ToTable("MP_Account").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
                e.HasMany(j => j.AccountRole).WithOne(j => j.Account).HasForeignKey(j => j.AccId);
                e.HasOne(x => x.Vendor).WithOne(x => x.Account).HasForeignKey<VAS_Vendor>(x => x.AccId);
            });

            modelBuilder.Entity<MP_GroupPermission>(e =>
            {
                e.ToTable("MP_GroupPermission").HasKey(k => k.GroupId);
                e.Property(p => p.GroupId).ValueGeneratedOnAdd();

                e.HasMany<MP_Permission>(sc => sc.MP_Permission).WithOne(s => s.MP_GroupPermission)
                    .HasForeignKey(sc => sc.GroupId);
            });

            modelBuilder.Entity<MP_Role_Permission>(e =>
            {
                e.ToTable("MP_Role_Permission").HasKey(sc => new { sc.IdRole, sc.IdPermission });
                e.HasOne<MP_Role>(sc => sc.Role).WithMany(s => s.RolePermission).HasForeignKey(sc => sc.IdRole);
                e.HasOne<MP_Permission>(sc => sc.Permission).WithMany(s => s.RolePermission).HasForeignKey(sc => sc.IdPermission);
            });

            modelBuilder.Entity<MP_AccountRole>(e =>
            {
                e.ToTable("MP_AccountRole").HasKey(sc => new { sc.AccId, sc.RoleId });
                e.HasOne<MP_Account>(sc => sc.Account).WithMany(s => s.AccountRole).HasForeignKey(sc => sc.AccId);
                e.HasOne<MP_Role>(sc => sc.Role).WithMany(s => s.AccountRole).HasForeignKey(sc => sc.RoleId);
            });
            modelBuilder.Entity<MP_CatType>(e =>
            {
                e.ToTable("MP_CatType").HasKey(k => k.Id);

                e.Property(p => p.Id).ValueGeneratedOnAdd();

                e.HasMany(j => j.Categories).WithOne(j => j.CatType).HasForeignKey(j => j.CatTypeId);
            });

            modelBuilder.Entity<MP_Category>(e =>
            {
                e.ToTable("MP_Category").HasKey(k => k.Id);

                e.Property(p => p.Id).ValueGeneratedOnAdd();

                e.HasOne(j => j.CatType).WithMany(j => j.Categories).HasForeignKey(j => j.CatTypeId);
            });
            modelBuilder.Entity<MP_RecoverPassword>(e =>
            {
                e.ToTable("MP_RecoverPassword").HasKey(k => k.Id);

                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MP_EmailConfig>(e =>
            {
                e.ToTable("MP_EmailConfig").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            #endregion

            #region VAS
            modelBuilder.Entity<VAS_Registered>(e =>
            {
                e.ToTable("VAS_Registered").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<VAS_Vendor>(e =>
            {
                e.ToTable("VAS_Vendor").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
                e.HasOne(x => x.Account).WithOne(x => x.Vendor).HasForeignKey<MP_Account>(x => x.VendorId);
            });
            modelBuilder.Entity<VAS_ServicesLog>(e =>
            {
                e.ToTable("VAS_ServicesLog").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<VAS_AuthenProperty>(e =>
            {
                e.ToTable("VAS_AuthenProperty").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<VAS_FtpHistory>(e =>
            {
                e.ToTable("CDR_FtpHistory").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CDR_PrefixRegis1Day>(e =>
            {
                e.ToTable("CDR_PrefixRegis1Day").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CDR_PrefixRegis30>(e =>
            {
                e.ToTable("CDR_PrefixRegis30").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CDR_PrefixRenew1Day>(e =>
            {
                e.ToTable("CDR_PrefixRenew1Day").HasKey(k => k.Id);
                e.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        #region MANAGER

        public virtual DbSet<MP_Account> Account { get; set; }
        public virtual DbSet<Lotto> Lotto { get; set; }
        public virtual DbSet<MP_AccountLoginTime> AccountLoginTime { get; set; }
        public virtual DbSet<MP_Role> Role { get; set; }
        public virtual DbSet<MP_Permission> Permission { get; set; }
        public virtual DbSet<MP_GroupPermission> GroupPermission { get; set; }
        public virtual DbSet<MP_AccountRole> AccountRole { get; set; }
        public virtual DbSet<MP_Role_Permission> RolePermission { get; set; }
        public virtual DbSet<MP_CatType> CatType { get; set; }
        public virtual DbSet<MP_Category> Category { get; set; }
        public virtual DbSet<MP_RecoverPassword> RecoverPassword { get; set; }
        public virtual DbSet<MP_EmailConfig> EmailConfig { get; set; }
        #endregion

        #region VAS

        public virtual DbSet<VAS_Registered> Registered { get; set; }
        public virtual DbSet<VAS_AuthenProperty> AuthenProperty { get; set; }
        /// <summary>
        /// Lịch sử đăng ký mới thuê bao theo tài khoản
        /// </summary>
        public virtual DbSet<RegisteredModel> VAS_GetRegisterByUser { get; set; }
        public virtual DbSet<AllRegisteredModel> VAS_GetRegister { get; set; }
        public virtual DbSet<VAS_Vendor> Vendor { get; set; }
        public virtual DbSet<VAS_ServicesLog> ServicesLog { get; set; }
        public virtual DbSet<VAS_FtpHistory> FtpHistory { get; set; }
        public virtual DbSet<CDR_PrefixRegis1Day> VAS_PrefixRegis1Day { get; set; }
        public virtual DbSet<CDR_PrefixRegis30> VAS_PrefixRegis30 { get; set; }
        public virtual DbSet<CDR_PrefixRenew1Day> VAS_PrefixRenew1Day { get; set; }
        public virtual DbSet<SP_CDR_PrefixRegis30> SP_CDR_PrefixRegis30 { get; set; }
        public virtual DbSet<SP_CDR_PrefixRenew1Day> SP_CDR_PrefixRenew1Day { get; set; }
        public virtual DbSet<SP_CDR_PrefixRegis1Day> SP_CDR_PrefixRegis1Day { get; set; }
        public virtual DbSet<SP_CDRRegis> SP_CDRRegis { get; set; }
        public virtual DbSet<SP_CDRRenew> SP_CDRRenew { get; set; }


        #endregion


    }


}
