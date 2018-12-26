namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FitnessDB : DbContext
    {
        public FitnessDB()
            : base("name=DBConnection")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }
        public virtual DbSet<Training> Trainings { get; set; }
        public virtual DbSet<Visiting> Visitings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Visitings)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subscription>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.Subscription)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trainer>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Trainer>()
                .Property(e => e.Specialization)
                .IsUnicode(false);

            modelBuilder.Entity<Training>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
