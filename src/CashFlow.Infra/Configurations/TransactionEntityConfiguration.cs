using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Infra.Configurations;

public class TransactionEntityConfiguration: IEntityTypeConfiguration<Domain.Aggregates.CashFlow.Entities.Transaction>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.CashFlow.Entities.Transaction> builder)
    {
        builder
            .ToTable("Transactions");

        builder
            .HasKey(c => c.Id);
    }
}