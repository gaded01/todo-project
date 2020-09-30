using System;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data {
	public class TodoContext : DbContext {
		public TodoContext (DbContextOptions options) : base (options) { }

		protected override void OnModelCreating (ModelBuilder builder) {
			//User Model
			builder.Entity<User> ().Property (p => p.Name).IsRequired().HasMaxLength(50);
			builder.Entity<User> ().Property (p => p.Email).IsRequired().HasMaxLength(100);
			builder.Entity<User> ().Property (p => p.MobileNumber).IsRequired().HasMaxLength(20);
			builder.Entity<User> ().Property (p => p.BirthDate).IsRequired().HasColumnType("date");
			builder.Entity<User> ().Property (p => p.Username).IsRequired().HasMaxLength(50);
			builder.Entity<User> ().Property (p => p.Password).IsRequired().HasMaxLength(50);

			//Activity Model
			builder.Entity<Activity> ().Property (p => p.EventActivity).IsRequired().HasMaxLength(100);
			builder.Entity<Activity> ().Property (p => p.Date).IsRequired().HasColumnType("date");
			builder.Entity<Activity> ().Property (p => p.Time).IsRequired();
			builder.Entity<Activity> ().Property (p => p.Setting).HasMaxLength(100);
			builder.Entity<Activity> ().Property (p => p.Details).HasMaxLength(100);
			

			//Configure entity table name
			builder.Entity<User> ().ToTable("User");
			builder.Entity<Activity> ().ToTable("Activity");

		}

    public DbSet<User> Users { get; set;}
		public DbSet<Activity> Activities { get; set;}
   
  }
}