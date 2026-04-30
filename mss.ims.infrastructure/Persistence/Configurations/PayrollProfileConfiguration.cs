namespace mss.ims.infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mss.ims.domain.Payroll;

public class PayrollProfileConfiguration : IEntityTypeConfiguration<PayrollProfile>
{
    public void Configure(EntityTypeBuilder<PayrollProfile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PayType).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PaySchedule).IsRequired().HasMaxLength(50);
        builder.Property(x => x.TaxId).HasMaxLength(50);

        builder.HasOne<mss.ims.domain.Employees.Employee>()
            .WithOne()
            .HasForeignKey<PayrollProfile>(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.EmployeeId).IsUnique();
    }
}