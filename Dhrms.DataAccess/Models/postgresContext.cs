using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dhrms.DataAccess.Models
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appliedjobs> Appliedjobs { get; set; }
        public virtual DbSet<Candidatedetails> Candidatedetails { get; set; }
        public virtual DbSet<Hr> Hr { get; set; }
        public virtual DbSet<Interviewerdetails> Interviewerdetails { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Appliedjobs>(entity =>
            {
                entity.ToTable("appliedjobs");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Candidateid).HasColumnName("candidateid");

                entity.Property(e => e.Jobid).HasColumnName("jobid");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Appliedjobs)
                    .HasForeignKey(d => d.Candidateid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_candidateid");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Appliedjobs)
                    .HasForeignKey(d => d.Jobid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_jobid");
            });

            modelBuilder.Entity<Candidatedetails>(entity =>
            {
                entity.HasKey(e => e.Candidateid)
                    .HasName("candidatedetails_pkey");

                entity.ToTable("candidatedetails");

                entity.HasIndex(e => e.Email)
                    .HasName("candidatedetails_email_key")
                    .IsUnique();

                entity.Property(e => e.Candidateid)
                    .HasColumnName("candidateid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Contactnumber)
                    .IsRequired()
                    .HasColumnName("contactnumber");

                entity.Property(e => e.Currentaddress)
                    .IsRequired()
                    .HasColumnName("currentaddress")
                    .HasMaxLength(250);

                entity.Property(e => e.Dateofbirth)
                    .HasColumnName("dateofbirth")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(80);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(60);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(1);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(60);

                entity.Property(e => e.Permanentaddress)
                    .IsRequired()
                    .HasColumnName("permanentaddress")
                    .HasMaxLength(250);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Candidatedetails)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_userid");
            });

            modelBuilder.Entity<Hr>(entity =>
            {
                entity.ToTable("hr");

                entity.HasIndex(e => e.Email)
                    .HasName("hr_email_key")
                    .IsUnique();

                entity.Property(e => e.Hrid)
                    .HasColumnName("hrid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Contactnumber)
                    .IsRequired()
                    .HasColumnName("contactnumber");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(80);

                entity.Property(e => e.Employeeid)
                    .HasColumnName("employeeid")
                    .HasMaxLength(50);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(80);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(1);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(30);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Hr)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_fk");
            });

            modelBuilder.Entity<Interviewerdetails>(entity =>
            {
                entity.HasKey(e => e.Intervievwerid)
                    .HasName("interviewerdetails_pkey");

                entity.ToTable("interviewerdetails");

                entity.HasIndex(e => e.Email)
                    .HasName("interviewerdetails_email_key")
                    .IsUnique();

                entity.Property(e => e.Intervievwerid)
                    .HasColumnName("intervievwerid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Contactnumber)
                    .IsRequired()
                    .HasColumnName("contactnumber");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(80);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(80);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(1);

                entity.Property(e => e.Jobrole)
                    .IsRequired()
                    .HasColumnName("jobrole");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(80);

                entity.Property(e => e.Primaryskills)
                    .IsRequired()
                    .HasColumnName("primaryskills");

                entity.Property(e => e.Unitname)
                    .IsRequired()
                    .HasColumnName("unitname");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Interviewerdetails)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_userid");
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.HasKey(e => e.Jobid)
                    .HasName("jobs_pkey");

                entity.ToTable("jobs");

                entity.Property(e => e.Jobid)
                    .HasColumnName("jobid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Ctc)
                    .HasColumnName("ctc")
                    .HasColumnType("numeric");

                entity.Property(e => e.Hrid).HasColumnName("hrid");

                entity.Property(e => e.Jobdescription)
                    .IsRequired()
                    .HasColumnName("jobdescription");

                entity.Property(e => e.Jobname)
                    .IsRequired()
                    .HasColumnName("jobname");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasMaxLength(50);

                entity.Property(e => e.Postedon)
                    .HasColumnName("postedon")
                    .HasColumnType("date");

                entity.Property(e => e.Skills)
                    .IsRequired()
                    .HasColumnName("skills");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Hr)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.Hrid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hrid_fk");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.Roleid)
                    .HasName("roles_pkey");

                entity.ToTable("roles");

                entity.Property(e => e.Roleid)
                    .HasColumnName("roleid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Rolename)
                    .IsRequired()
                    .HasColumnName("rolename")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("users_email_key")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(80);

                entity.Property(e => e.Roleid).HasColumnName("roleid");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(80);

                entity.Property(e => e.Userpassword)
                    .IsRequired()
                    .HasColumnName("userpassword")
                    .HasMaxLength(30);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
