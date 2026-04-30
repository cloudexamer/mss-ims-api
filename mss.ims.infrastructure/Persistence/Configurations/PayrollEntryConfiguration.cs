namespace mss.ims.infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mss.ims.domain.Payroll;

public class PayrollEntryConfiguration : IEntityTypeConfiguration<PayrollEntry>
{
    public void Configure(EntityTypeBuilder<PayrollEntry> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.GrossPay).HasColumnType("decimal(18,2)");
        builder.Property(x => x.TotalDeductions).HasColumnType("decimal(18,2)");
        builder.Property(x => x.TotalTaxes).HasColumnType("decimal(18,2)");
        builder.Property(x => x.NetPay).HasColumnType("decimal(18,2)");
        builder.Property(x => x.RegularHours).HasColumnType("decimal(18,2)");
        builder.Property(x => x.OvertimeHours).HasColumnType("decimal(18,2)");

        builder.HasOne<PayrollRun>()
            .WithMany()
            .HasForeignKey(x => x.PayrollRunId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<mss.ims.domain.Employees.Employee>()
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}