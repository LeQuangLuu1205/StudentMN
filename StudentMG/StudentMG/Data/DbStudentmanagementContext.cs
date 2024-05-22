using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudentMG.ViewModels;

namespace StudentMG.Data;

public partial class DbStudentmanagementContext : DbContext
{
    public DbStudentmanagementContext()
    {
    }

    public DbStudentmanagementContext(DbContextOptions<DbStudentmanagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountAdmin> AccountAdmins { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseStudent> CourseStudents { get; set; }

    public virtual DbSet<Coursedetail> Coursedetails { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Kindofimage> Kindofimages { get; set; }

    public virtual DbSet<Lecturer> Lecturers { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentImage> StudentImages { get; set; }

/*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-LAPTOP;Initial Catalog=dbSTUDENTMANAGEMENT;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountAdmin>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__ACCOUNT___349DA5868148079F");

            entity.ToTable("ACCOUNT_ADMIN");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__CLASS__CB1927A0BAFF92F4");

            entity.ToTable("CLASS");

            entity.Property(e => e.ClassId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ClassID");
            entity.Property(e => e.LecturerId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LecturerID");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Lecturer).WithMany(p => p.Classes)
                .HasForeignKey(d => d.LecturerId)
                .HasConstraintName("FK_tbLECTURER_tbCLASS");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__COURSE__C92D71876001D46A");

            entity.ToTable("COURSE");

            entity.Property(e => e.CourseId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CourseID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<CourseStudent>(entity =>
        {
            entity.HasKey(e => new { e.CourseDetailId, e.StudentId }).HasName("PK__COURSE_S__473BADA716651A07");

            entity.ToTable("COURSE_STUDENT");

            entity.Property(e => e.CourseDetailId).HasColumnName("CourseDetailID");
            entity.Property(e => e.StudentId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("StudentID");

            entity.HasOne(d => d.CourseDetail).WithMany(p => p.CourseStudents)
                .HasForeignKey(d => d.CourseDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbCOURSE_STUDENT_tbCOURSEDETAILS");

            entity.HasOne(d => d.Student).WithMany(p => p.CourseStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbCOURSE_STUDENT_tbSTUDENT");
        });

        modelBuilder.Entity<Coursedetail>(entity =>
        {
            entity.HasKey(e => e.CourseDetailId).HasName("PK__COURSEDE__C417FF001AD1615A");

            entity.ToTable("COURSEDETAILS");

            entity.Property(e => e.CourseDetailId).HasColumnName("CourseDetailID");
            entity.Property(e => e.CourseId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CourseID");
            entity.Property(e => e.LecturerId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LecturerID");

            entity.HasOne(d => d.Course).WithMany(p => p.Coursedetails)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_tbCOURSE_tbCOURSEDETAILS");

            entity.HasOne(d => d.Lecturer).WithMany(p => p.Coursedetails)
                .HasForeignKey(d => d.LecturerId)
                .HasConstraintName("FK_tbLECTURER_tbCOURSEDETAILS");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("PK__FACULTY__306F636E1EFC8A80");

            entity.ToTable("FACULTY");

            entity.Property(e => e.FacultyId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FacultyID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Kindofimage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__KINDOFIM__7516F4EC39AC34BA");

            entity.ToTable("KINDOFIMAGE");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Lecturer>(entity =>
        {
            entity.HasKey(e => e.LecturerId).HasName("PK__LECTURER__5A78B91D8DA28420");

            entity.ToTable("LECTURER");

            entity.Property(e => e.LecturerId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LecturerID");
            entity.Property(e => e.AcademicDegree).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FacultyId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FacultyID");
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.IsExist).HasColumnName("isExist");
            entity.Property(e => e.NoIdentity)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Phone_number");
            entity.Property(e => e.RandomKey)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Faculty).WithMany(p => p.Lecturers)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_tbLECTURER_tbFACULTY");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__STUDENT__32C52A79BAED41D9");

            entity.ToTable("STUDENT");

            entity.Property(e => e.StudentId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.ClassId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ClassID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.IsExist).HasColumnName("isExist");
            entity.Property(e => e.NoIdentity)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Phone_number");
            entity.Property(e => e.RandomKey)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_tbSTUDENT_tbCLASS");
        });

        modelBuilder.Entity<StudentImage>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.ImageId }).HasName("PK__STUDENT___E59445371DDBB71C");

            entity.ToTable("STUDENT_IMAGE");

            entity.Property(e => e.StudentId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("StudentID");
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Image).WithMany(p => p.StudentImages)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbSTUDENT_IMAGE_tbKINDOFIMAGE");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentImages)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbSTUDENT_IMAGE_tbSTUDENT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<StudentMG.ViewModels.GradeVM> GradeVM { get; set; } = default!;

public DbSet<StudentMG.ViewModels.ImageVM> ImageVM { get; set; } = default!;

public DbSet<StudentMG.ViewModels.StudentVM> StudentVM { get; set; } = default!;

public DbSet<StudentMG.ViewModels.StudentInfoVM> StudentInfoVM { get; set; } = default!;
}
