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

        public virtual DbSet<AllocatedUnitdetails> AllocatedUnitdetails { get; set; }
        public virtual DbSet<Appliedjobs> Appliedjobs { get; set; }
        public virtual DbSet<Candidatedetails> Candidatedetails { get; set; }
        public virtual DbSet<Diplomadetails> Diplomadetails { get; set; }
        public virtual DbSet<Educationaldetails> Educationaldetails { get; set; }
        public virtual DbSet<Highereducationaldetails> Highereducationaldetails { get; set; }
        public virtual DbSet<Hr> Hr { get; set; }
        public virtual DbSet<Interviewdetails> Interviewdetails { get; set; }
        public virtual DbSet<Interviewerdetails> Interviewerdetails { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<LoginDetails> LoginDetails { get; set; }
        public virtual DbSet<Pgdetails> Pgdetails { get; set; }
        public virtual DbSet<Pucdetails> Pucdetails { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Secondaryeducationaldetails> Secondaryeducationaldetails { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
        public virtual DbSet<Sslcdetails> Sslcdetails { get; set; }
        public virtual DbSet<Ugdetails> Ugdetails { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Workexperiencedetails> Workexperiencedetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum(null, "gen", new[] { "f", "m", "o" });

            modelBuilder.Entity<AllocatedUnitdetails>(entity =>
            {
                entity.HasKey(e => e.UnitId)
                    .HasName("pk_unit");

                entity.ToTable("allocated_unitdetails");

                entity.Property(e => e.UnitId)
                    .HasColumnName("unit_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.BaseLocation)
                    .IsRequired()
                    .HasColumnName("base_location")
                    .HasMaxLength(50);

                entity.Property(e => e.Candidateid).HasColumnName("candidateid");

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasColumnName("designation")
                    .HasMaxLength(50);

                entity.Property(e => e.JoinedOn)
                    .HasColumnName("joined_on")
                    .HasColumnType("date");

                entity.Property(e => e.MonthsRemaining)
                    .HasColumnName("months_remaining")
                    .HasColumnType("numeric");

                entity.Property(e => e.NoticePeriod)
                    .HasColumnName("notice_period")
                    .HasColumnType("numeric");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasColumnName("unit_name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.AllocatedUnitdetails)
                    .HasForeignKey(d => d.Candidateid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_candidate");
            });

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

            modelBuilder.Entity<Diplomadetails>(entity =>
            {
                entity.HasKey(e => e.Diplomaid)
                    .HasName("diplomadetails_pkey");

                entity.ToTable("diplomadetails");

                entity.Property(e => e.Diplomaid)
                    .HasColumnName("diplomaid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Institutionname)
                    .IsRequired()
                    .HasColumnName("institutionname");

                entity.Property(e => e.Percentage)
                    .HasColumnName("percentage")
                    .HasColumnType("numeric");

                entity.Property(e => e.Streamname)
                    .IsRequired()
                    .HasColumnName("streamname");

                entity.Property(e => e.Yearofpassing)
                    .IsRequired()
                    .HasColumnName("yearofpassing");
            });

            modelBuilder.Entity<Educationaldetails>(entity =>
            {
                entity.HasKey(e => e.Educationalid)
                    .HasName("educationaldetails_pkey");

                entity.ToTable("educationaldetails");

                entity.HasIndex(e => e.Candidateid)
                    .HasName("educationaldetails_candidateid_key")
                    .IsUnique();

                entity.Property(e => e.Educationalid)
                    .HasColumnName("educationalid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Candidateid).HasColumnName("candidateid");

                entity.Property(e => e.Highereducationalid).HasColumnName("highereducationalid");

                entity.Property(e => e.Resumea).HasColumnName("resumea");

                entity.Property(e => e.Secondaryeducationalid).HasColumnName("secondaryeducationalid");

                entity.HasOne(d => d.Candidate)
                    .WithOne(p => p.Educationaldetails)
                    .HasForeignKey<Educationaldetails>(d => d.Candidateid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_candidate");

                entity.HasOne(d => d.Highereducational)
                    .WithMany(p => p.Educationaldetails)
                    .HasForeignKey(d => d.Highereducationalid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_highereducation");

                entity.HasOne(d => d.Secondaryeducational)
                    .WithMany(p => p.Educationaldetails)
                    .HasForeignKey(d => d.Secondaryeducationalid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_secondaryeducation");
            });

            modelBuilder.Entity<Highereducationaldetails>(entity =>
            {
                entity.HasKey(e => e.Highereducationalid)
                    .HasName("highereducationaldetails_pkey");

                entity.ToTable("highereducationaldetails");

                entity.Property(e => e.Highereducationalid)
                    .HasColumnName("highereducationalid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Diplomaid).HasColumnName("diplomaid");

                entity.Property(e => e.Pgid).HasColumnName("pgid");

                entity.Property(e => e.Ugid).HasColumnName("ugid");

                entity.HasOne(d => d.Diploma)
                    .WithMany(p => p.Highereducationaldetails)
                    .HasForeignKey(d => d.Diplomaid)
                    .HasConstraintName("fk_diploma");

                entity.HasOne(d => d.Pg)
                    .WithMany(p => p.Highereducationaldetails)
                    .HasForeignKey(d => d.Pgid)
                    .HasConstraintName("fk_pg");

                entity.HasOne(d => d.Ug)
                    .WithMany(p => p.Highereducationaldetails)
                    .HasForeignKey(d => d.Ugid)
                    .HasConstraintName("fk_ug");
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

            modelBuilder.Entity<Interviewdetails>(entity =>
            {
                entity.HasKey(e => e.Interviewid)
                    .HasName("interviewdetails_pkey");

                entity.ToTable("interviewdetails");

                entity.Property(e => e.Interviewid)
                    .HasColumnName("interviewid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Attended)
                    .IsRequired()
                    .HasColumnName("attended")
                    .HasMaxLength(50);

                entity.Property(e => e.Candidateid).HasColumnName("candidateid");

                entity.Property(e => e.Intervievwerid).HasColumnName("intervievwerid");

                entity.Property(e => e.Interviewerfeedback)
                    .IsRequired()
                    .HasColumnName("interviewerfeedback");

                entity.Property(e => e.Roundname)
                    .IsRequired()
                    .HasColumnName("roundname");

                entity.Property(e => e.Scheduleddate)
                    .HasColumnName("scheduleddate")
                    .HasColumnType("date");

                entity.Property(e => e.Scheduledtime)
                    .IsRequired()
                    .HasColumnName("scheduledtime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Interviewdetails)
                    .HasForeignKey(d => d.Candidateid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_candidate");

                entity.HasOne(d => d.Intervievwer)
                    .WithMany(p => p.Interviewdetails)
                    .HasForeignKey(d => d.Intervievwerid)
                    .HasConstraintName("fk_intervievwer");
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

            modelBuilder.Entity<LoginDetails>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("pk_login");

                entity.ToTable("login_details");

                entity.Property(e => e.LoginId)
                    .HasColumnName("login_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasColumnName("ip_address");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location");

                entity.Property(e => e.LoginTime)
                    .IsRequired()
                    .HasColumnName("login_time");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginDetails)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<Pgdetails>(entity =>
            {
                entity.HasKey(e => e.Pgid)
                    .HasName("pgdetails_pkey");

                entity.ToTable("pgdetails");

                entity.Property(e => e.Pgid)
                    .HasColumnName("pgid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Institutionname)
                    .IsRequired()
                    .HasColumnName("institutionname");

                entity.Property(e => e.Percentage)
                    .HasColumnName("percentage")
                    .HasColumnType("numeric");

                entity.Property(e => e.Streamname)
                    .IsRequired()
                    .HasColumnName("streamname");

                entity.Property(e => e.Yearofpassing)
                    .IsRequired()
                    .HasColumnName("yearofpassing");
            });

            modelBuilder.Entity<Pucdetails>(entity =>
            {
                entity.HasKey(e => e.Pucid)
                    .HasName("pucdetails_pkey");

                entity.ToTable("pucdetails");

                entity.Property(e => e.Pucid)
                    .HasColumnName("pucid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Institutionname)
                    .IsRequired()
                    .HasColumnName("institutionname");

                entity.Property(e => e.Percentage)
                    .HasColumnName("percentage")
                    .HasColumnType("numeric");

                entity.Property(e => e.Yearofpassing)
                    .IsRequired()
                    .HasColumnName("yearofpassing");
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

            modelBuilder.Entity<Secondaryeducationaldetails>(entity =>
            {
                entity.HasKey(e => e.Secondaryeducationalid)
                    .HasName("secondaryeducationaldetails_pkey");

                entity.ToTable("secondaryeducationaldetails");

                entity.Property(e => e.Secondaryeducationalid)
                    .HasColumnName("secondaryeducationalid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Pucid).HasColumnName("pucid");

                entity.Property(e => e.Sslcid).HasColumnName("sslcid");

                entity.HasOne(d => d.Puc)
                    .WithMany(p => p.Secondaryeducationaldetails)
                    .HasForeignKey(d => d.Pucid)
                    .HasConstraintName("fk_puc");

                entity.HasOne(d => d.Sslc)
                    .WithMany(p => p.Secondaryeducationaldetails)
                    .HasForeignKey(d => d.Sslcid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sslc");
            });

            modelBuilder.Entity<Skills>(entity =>
            {
                entity.HasKey(e => e.Skillid)
                    .HasName("skills_pkey");

                entity.ToTable("skills");

                entity.Property(e => e.Skillid)
                    .HasColumnName("skillid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Candidateid).HasColumnName("candidateid");

                entity.Property(e => e.Primaryskill)
                    .IsRequired()
                    .HasColumnName("primaryskill");

                entity.Property(e => e.Secondaryskill)
                    .IsRequired()
                    .HasColumnName("secondaryskill");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.Candidateid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_candidate");
            });

            modelBuilder.Entity<Sslcdetails>(entity =>
            {
                entity.HasKey(e => e.Sslcid)
                    .HasName("sslcdetails_pkey");

                entity.ToTable("sslcdetails");

                entity.Property(e => e.Sslcid)
                    .HasColumnName("sslcid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Institutionname)
                    .IsRequired()
                    .HasColumnName("institutionname");

                entity.Property(e => e.Percentage)
                    .HasColumnName("percentage")
                    .HasColumnType("numeric");

                entity.Property(e => e.Yearofpassing)
                    .IsRequired()
                    .HasColumnName("yearofpassing");
            });

            modelBuilder.Entity<Ugdetails>(entity =>
            {
                entity.HasKey(e => e.Ugid)
                    .HasName("ugdetails_pkey");

                entity.ToTable("ugdetails");

                entity.Property(e => e.Ugid)
                    .HasColumnName("ugid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Institutionname)
                    .IsRequired()
                    .HasColumnName("institutionname");

                entity.Property(e => e.Percentage)
                    .HasColumnName("percentage")
                    .HasColumnType("numeric");

                entity.Property(e => e.Streamname)
                    .IsRequired()
                    .HasColumnName("streamname");

                entity.Property(e => e.Yearofpassing)
                    .IsRequired()
                    .HasColumnName("yearofpassing");
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

            modelBuilder.Entity<Workexperiencedetails>(entity =>
            {
                entity.HasKey(e => e.Experienceid)
                    .HasName("workexperiencedetails_pkey");

                entity.ToTable("workexperiencedetails");

                entity.Property(e => e.Experienceid)
                    .HasColumnName("experienceid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Candidateid).HasColumnName("candidateid");

                entity.Property(e => e.Companyname)
                    .IsRequired()
                    .HasColumnName("companyname");

                entity.Property(e => e.Domainname)
                    .IsRequired()
                    .HasColumnName("domainname");

                entity.Property(e => e.Noofmonths)
                    .HasColumnName("noofmonths")
                    .HasColumnType("numeric");

                entity.Property(e => e.Noofyears)
                    .HasColumnName("noofyears")
                    .HasColumnType("numeric");

                entity.Property(e => e.Project)
                    .IsRequired()
                    .HasColumnName("project");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Workexperiencedetails)
                    .HasForeignKey(d => d.Candidateid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_candidate");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
