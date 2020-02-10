using Microsoft.EntityFrameworkCore;

namespace UportTransactions.DAL.Models
{
    public partial class UportTransactionsContext : DbContext
    {
        public UportTransactionsContext(DbContextOptions<UportTransactionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<TransactionRequest> TransactionRequest { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:DefaultSchema", "db_owner");

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => new { e.TransactionId, e.RequestId });

                entity.ToTable("Transaction", "dbo");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID").ValueGeneratedOnAdd();

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BlockNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Data)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GasPrice)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GasUsed)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LogIndex)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Topics)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionHash)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionIndex)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Transaction_TransactionRequest");
            });

            modelBuilder.Entity<TransactionRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("TransactionRequest", "dbo");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(500);
            });
        }
    }
}
