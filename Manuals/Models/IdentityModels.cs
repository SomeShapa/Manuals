using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Manuals.Entities;
using System.Collections.Generic;
using System;

namespace Manuals.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public virtual ICollection<Medal> Medals { get; set; }

        public virtual ICollection<Manual> Manuals { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Language { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }

        public string Theme { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Manual> Manuals { get; set; }
        public virtual DbSet<Medal> Medals { get; set; }
        public virtual DbSet<RatingComment> RatingComments { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Manauls)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.RatingComments)
                .WithRequired(e => e.Comment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manual>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Manual>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Manual>()
                .Property(e => e.ImageLink)
                .IsUnicode(false);

            modelBuilder.Entity<Manual>()
                .Property(e => e.VideoLink)
                .IsUnicode(false);

            modelBuilder.Entity<Manual>()
                .Property(e => e.Body)
                .IsUnicode(false);

            modelBuilder.Entity<Manual>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Manual)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manual>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Manual)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manual>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Manuals)
                .Map(m => m.ToTable("BasketTags").MapLeftKey("ManualId").MapRightKey("TagId"));

            modelBuilder.Entity<Medal>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Medal>()
                .Property(e => e.ImageLink)
                .IsUnicode(false);

            modelBuilder.Entity<Medal>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Medals)
                .Map(m => m.ToTable("BasketMedal").MapLeftKey("MedalId").MapRightKey("UserId"));

            modelBuilder.Entity<Tag>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                //create two roles
                var role1 = new IdentityRole { Name = "Admin" };
                var role2 = new IdentityRole { Name = "User" };

                //added roles in DB 
                roleManager.Create(role1);
                roleManager.Create(role2);

                //create admin
                var admin = new ApplicationUser { Email = "akostiv262@gmail.com", UserName = "akostiv262@gmail.com" };
                string password = "123456";
                var result = userManager.Create(admin, password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, role1.Name);
                    userManager.AddToRole(admin.Id, role2.Name);
                }

                admin = new ApplicationUser { Email = "pavel.shipicyn@gmail.com", UserName = "pavel.shipicyn@gmail.com" };

                result = userManager.Create(admin, password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, role1.Name);
                    userManager.AddToRole(admin.Id, role2.Name);
                }
                base.Seed(context);
            }
        }

    }
}