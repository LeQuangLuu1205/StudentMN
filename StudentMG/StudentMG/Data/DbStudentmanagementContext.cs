using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Lecturer> Lecturers { get; set; }

    public virtual DbSet<Student> Students { get; set; }
/*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-LAPTOP;Initial Catalog=dbSTUDENTMANAGEMENT;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountAdmin>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__ACCOUNT___349DA58652896007");

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
            entity.HasKey(e => e.ClassId).HasName("PK__CLASS__CB1927A0EC665394");

            entity.ToTable("CLASS");

            entity.Property(e => e.ClassId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ClassID");
            entity.Property(e => e.LecturerId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LecturerID");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Lecturer).WithMany(p => p.Classes)
                .HasForeignKey(d => d.LecturerId)
                .HasConstraintName("FK_tbLECTURER_tbCLASS");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__COURSE__C92D71872D89F2E6");

            entity.ToTable("COURSE");

            entity.Property(e => e.CourseId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CourseID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<CourseStudent>(entity =>
        {
            entity.HasKey(e => new { e.CourseDetailId, e.StudentId }).HasName("PK__COURSE_S__473BADA7662545E5");

            entity.ToTable("COURSE_STUDENT");

            entity.Property(e => e.CourseDetailId).HasColumnName("CourseDetailID");
            entity.Property(e => e.StudentId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
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
            entity.HasKey(e => e.CourseDetailId).HasName("PK__COURSEDE__C417FF00F2C740E2");

            entity.ToTable("COURSEDETAILS");

            entity.Property(e => e.CourseDetailId).HasColumnName("CourseDetailID");
            entity.Property(e => e.CourseId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CourseID");
            entity.Property(e => e.LecturerId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
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
            entity.HasKey(e => e.FacultyId).HasName("PK__FACULTY__306F636E0F61060B");

            entity.ToTable("FACULTY");

            entity.Property(e => e.FacultyId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FacultyID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Lecturer>(entity =>
        {
            entity.HasKey(e => e.LecturerId).HasName("PK__LECTURER__5A78B91D2C068185");

            entity.ToTable("LECTURER");

            entity.Property(e => e.LecturerId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LecturerID");
            entity.Property(e => e.AcademicDegree).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FacultyId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("FacultyID");
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Faculty).WithMany(p => p.Lecturers)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_tbLECTURER_tbFACULTY");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__STUDENT__32C52A797CE44021");

            entity.ToTable("STUDENT");

            entity.Property(e => e.StudentId)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.ClassId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ClassID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fullname).HasMaxLength(50);
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
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK_tbSTUDENT_tbCLASS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
