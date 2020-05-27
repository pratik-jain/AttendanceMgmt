namespace AttendanceMgmt.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AttendanceManagment : DbContext
    {
        public AttendanceManagment()
            : base("name=AttendanceManagment")
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Leaf> Leaves { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<vHolidayDetail> vHolidayDetails { get; set; }
        public virtual DbSet<vUserAttendanceDetail> vUserAttendanceDetails { get; set; }
        public virtual DbSet<vUserDetail> vUserDetails { get; set; }
        public virtual DbSet<vUserFullDetail> vUserFullDetails { get; set; }
        public virtual DbSet<vUserLeaveDetail> vUserLeaveDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
                .Property(e => e.ApprovedByName)
                .IsUnicode(false);

            modelBuilder.Entity<Holiday>()
                .Property(e => e.HolidayName)
                .IsUnicode(false);

            modelBuilder.Entity<Holiday>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Leaf>()
                .Property(e => e.ApprovedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Leaf>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Attendances)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Leaves)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserDetails)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserType>()
                .Property(e => e.UserTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<UserType>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<vHolidayDetail>()
                .Property(e => e.HolidayName)
                .IsUnicode(false);

            modelBuilder.Entity<vHolidayDetail>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<vUserAttendanceDetail>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<vUserAttendanceDetail>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<vUserAttendanceDetail>()
                .Property(e => e.UserTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<vUserAttendanceDetail>()
                .Property(e => e.ApprovedByName)
                .IsUnicode(false);

            modelBuilder.Entity<vUserDetail>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<vUserDetail>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<vUserDetail>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<vUserDetail>()
                .Property(e => e.UserTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<vUserFullDetail>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<vUserFullDetail>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<vUserFullDetail>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<vUserFullDetail>()
                .Property(e => e.UserTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<vUserLeaveDetail>()
                .Property(e => e.ApprovedBy)
                .IsUnicode(false);

            modelBuilder.Entity<vUserLeaveDetail>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<vUserLeaveDetail>()
                .Property(e => e.UserName)
                .IsUnicode(false);
        }
    }
}
