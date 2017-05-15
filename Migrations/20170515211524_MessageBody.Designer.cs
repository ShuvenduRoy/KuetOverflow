using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using KuetOverflow.Data;

namespace KuetOverflow.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20170515211524_MessageBody")]
    partial class MessageBody
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KuetOverflow.Models.Answer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ActivityID");

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("QuestionID");

                    b.Property<string>("Title");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.HasIndex("ActivityID");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("KuetOverflow.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("LectureID");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("KuetOverflow.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseNo");

                    b.Property<double>("Credits");

                    b.Property<int>("DepartmentID");

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.HasKey("CourseID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("KuetOverflow.Models.CourseAssignment", b =>
                {
                    b.Property<int>("CourseID");

                    b.Property<int>("InstructorID");

                    b.HasKey("CourseID", "InstructorID");

                    b.HasIndex("InstructorID");

                    b.ToTable("CourseAssignment");
                });

            modelBuilder.Entity("KuetOverflow.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<int?>("InstructorID");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("StartDate");

                    b.HasKey("DepartmentID");

                    b.HasIndex("InstructorID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("KuetOverflow.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseID");

                    b.Property<int?>("Grade");

                    b.Property<int>("StudentID");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("CourseID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("KuetOverflow.Models.Follow", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FolloweeId");

                    b.Property<int>("FollowerId");

                    b.HasKey("ID");

                    b.HasIndex("FolloweeId");

                    b.HasIndex("FollowerId");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("KuetOverflow.Models.Instructor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasMaxLength(50);

                    b.Property<DateTime>("HireDate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("KuetOverflow.Models.Lecture", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int>("CourseId");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("ID");

                    b.ToTable("Lecture");
                });

            modelBuilder.Entity("KuetOverflow.Models.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("From");

                    b.Property<bool>("IsSeen");

                    b.Property<int>("To");

                    b.HasKey("ID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("KuetOverflow.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("Time");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("KuetOverflow.Models.OfficeAssignment", b =>
                {
                    b.Property<int>("InstructorID");

                    b.Property<string>("Location")
                        .HasMaxLength(50);

                    b.HasKey("InstructorID");

                    b.ToTable("OfficeAssignment");
                });

            modelBuilder.Entity("KuetOverflow.Models.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ActivityID");

                    b.Property<int>("CourseID");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.Property<int>("TotalAnswers");

                    b.Property<int>("TotalStars");

                    b.Property<int>("TotalVote");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.Property<int>("Views");

                    b.HasKey("ID");

                    b.HasIndex("ActivityID");

                    b.HasIndex("CourseID");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("KuetOverflow.Models.SchoolViewModels.Activity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("KuetOverflow.Models.Star", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuestionID");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.ToTable("Stars");
                });

            modelBuilder.Entity("KuetOverflow.Models.Student", b =>
                {
                    b.Property<int>("ID");

                    b.Property<string>("Email");

                    b.Property<DateTime>("EnrollmentDate");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("KuetOverflow.Models.Tweet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("TweetUserID");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Tweet");
                });

            modelBuilder.Entity("KuetOverflow.Models.TwitterUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Followee");

                    b.Property<int>("Follower");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.ToTable("TwitterUsers");
                });

            modelBuilder.Entity("KuetOverflow.Models.UserNotification", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("NotificationId");

                    b.Property<bool>("IsRead");

                    b.HasKey("UserId", "NotificationId");

                    b.HasAlternateKey("NotificationId", "UserId");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("KuetOverflow.Models.Vote", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuestionID");

                    b.Property<string>("UserID");

                    b.Property<int>("Value");

                    b.HasKey("ID");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("KuetOverflow.Models.Answer", b =>
                {
                    b.HasOne("KuetOverflow.Models.SchoolViewModels.Activity")
                        .WithMany("Answers")
                        .HasForeignKey("ActivityID");
                });

            modelBuilder.Entity("KuetOverflow.Models.Course", b =>
                {
                    b.HasOne("KuetOverflow.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KuetOverflow.Models.CourseAssignment", b =>
                {
                    b.HasOne("KuetOverflow.Models.Course", "Course")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KuetOverflow.Models.Instructor", "Instructor")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KuetOverflow.Models.Department", b =>
                {
                    b.HasOne("KuetOverflow.Models.Instructor", "Administrator")
                        .WithMany()
                        .HasForeignKey("InstructorID");
                });

            modelBuilder.Entity("KuetOverflow.Models.Enrollment", b =>
                {
                    b.HasOne("KuetOverflow.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KuetOverflow.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KuetOverflow.Models.Follow", b =>
                {
                    b.HasOne("KuetOverflow.Models.TwitterUser", "Followee")
                        .WithMany()
                        .HasForeignKey("FolloweeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KuetOverflow.Models.TwitterUser", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KuetOverflow.Models.OfficeAssignment", b =>
                {
                    b.HasOne("KuetOverflow.Models.Instructor", "Instructor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("KuetOverflow.Models.OfficeAssignment", "InstructorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KuetOverflow.Models.Question", b =>
                {
                    b.HasOne("KuetOverflow.Models.SchoolViewModels.Activity")
                        .WithMany("Questions")
                        .HasForeignKey("ActivityID");

                    b.HasOne("KuetOverflow.Models.Course")
                        .WithMany("Questions")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KuetOverflow.Models.UserNotification", b =>
                {
                    b.HasOne("KuetOverflow.Models.Notification", "Notification")
                        .WithMany()
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
