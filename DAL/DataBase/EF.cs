using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public partial class EF : DbContext
    {
        public EF()
            : base("name=EF")
        {
        }

        public virtual DbSet<C_Service> C_Service { get; set; }
        public virtual DbSet<C_Service_Connection> C_Service_Connection { get; set; }
        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Calling> Calling { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<Individual> Individual { get; set; }
        public virtual DbSet<Internet> Internet { get; set; }
        public virtual DbSet<LegalEntity> LegalEntity { get; set; }
        public virtual DbSet<Monthly_remains_tarif> Monthly_remains_tarif { get; set; }
        public virtual DbSet<Number> Number { get; set; }
        public virtual DbSet<SMS> SMS { get; set; }
        public virtual DbSet<Tarif> Tarif { get; set; }
        public virtual DbSet<Tarif_History> Tarif_History { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<C_Service>()
                .Property(e => e.Name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C_Service>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<C_Service>()
                .HasMany(e => e.C_Service_Connection)
                .WithOptional(e => e.C_Service)
                .HasForeignKey(e => e.ID_Service);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.C_Login)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.C_Password)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.C_Login)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.C_Password)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Number)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.ID_Client);

            modelBuilder.Entity<Expenses>()
                .Property(e => e.Expense)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Expenses>()
                .HasMany(e => e.Calling)
                .WithOptional(e => e.Expenses)
                .HasForeignKey(e => e.ID_Expenses);

            modelBuilder.Entity<Expenses>()
                .HasMany(e => e.Internet)
                .WithOptional(e => e.Expenses)
                .HasForeignKey(e => e.ID_Expenses);

            modelBuilder.Entity<Expenses>()
                .HasMany(e => e.SMS)
                .WithOptional(e => e.Expenses)
                .HasForeignKey(e => e.ID_Expenses);

            modelBuilder.Entity<Individual>()
                .Property(e => e.FirstName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Individual>()
                .Property(e => e.LastName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Individual>()
                .Property(e => e.Patronymic)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Individual>()
                .Property(e => e.Passport)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Internet>()
                .Property(e => e.C_Data)
                .HasPrecision(18, 0);

            modelBuilder.Entity<LegalEntity>()
                .Property(e => e.OrganizationName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LegalEntity>()
                .Property(e => e.MSRN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LegalEntity>()
                .Property(e => e.IdividualTaxpayerNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LegalEntity>()
                .Property(e => e.Adress)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Monthly_remains_tarif>()
                .HasMany(e => e.Number)
                .WithOptional(e => e.Monthly_remains_tarif)
                .HasForeignKey(e => e.ID_Monthly_remains_tarif);

            modelBuilder.Entity<Number>()
                .Property(e => e.Number1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Number>()
                .Property(e => e.Bill)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Number>()
                .HasMany(e => e.C_Service_Connection)
                .WithOptional(e => e.Number)
                .HasForeignKey(e => e.ID_number);

            modelBuilder.Entity<Number>()
                .HasMany(e => e.Calling)
                .WithOptional(e => e.Number)
                .HasForeignKey(e => e.ID_number_host);

            modelBuilder.Entity<Number>()
                .HasMany(e => e.Calling1)
                .WithOptional(e => e.Number1)
                .HasForeignKey(e => e.ID_number_slave);

            modelBuilder.Entity<Number>()
                .HasMany(e => e.Expenses)
                .WithOptional(e => e.Number)
                .HasForeignKey(e => e.ID_number);

            modelBuilder.Entity<Number>()
                .HasMany(e => e.Internet)
                .WithOptional(e => e.Number)
                .HasForeignKey(e => e.ID_number);

            modelBuilder.Entity<Number>()
                .HasMany(e => e.SMS)
                .WithOptional(e => e.Number)
                .HasForeignKey(e => e.ID_number_host);

            modelBuilder.Entity<Number>()
                .HasMany(e => e.SMS1)
                .WithOptional(e => e.Number1)
                .HasForeignKey(e => e.ID_number_slave);

            modelBuilder.Entity<Number>()
                .HasMany(e => e.Tarif_History)
                .WithRequired(e => e.Number)
                .HasForeignKey(e => e.ID_Number)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SMS>()
                .Property(e => e.C_Data)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tarif>()
                .Property(e => e.Price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Tarif>()
                .Property(e => e.call_price_inCity)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Tarif>()
                .Property(e => e.call_price_outCity)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Tarif>()
                .Property(e => e.call_price_outContry)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Tarif>()
                .Property(e => e.sms_price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Tarif>()
                .Property(e => e.internet_price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Tarif>()
                .Property(e => e.name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tarif>()
                .HasMany(e => e.Number)
                .WithRequired(e => e.Tarif)
                .HasForeignKey(e => e.ID_Tarif)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tarif>()
                .HasMany(e => e.Tarif_History)
                .WithRequired(e => e.Tarif)
                .HasForeignKey(e => e.ID_Tarif)
                .WillCascadeOnDelete(false);
        }
    }
}
