
using Microsoft.EntityFrameworkCore;
using mss.ims.domain.Customers;
using mss.ims.domain.Employees;
using mss.ims.domain.Payroll;
using mss.ims.domain.Vendors;

namespace mss.ims.application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Vendor> Vendors { get; }
    DbSet<Employee> Employees { get; }
    DbSet<PayrollProfile> PayrollProfiles { get; }
    DbSet<PayrollRun> PayrollRuns { get; }
    DbSet<PayrollEntry> PayrollEntries { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}