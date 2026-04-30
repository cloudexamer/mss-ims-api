namespace mss.ims.domain.Payroll
{
    using System;
    using mss.ims.domain.Common;

    public class PayrollEntry : AuditableEntity
    {
        public Guid PayrollRunId { get; private set; }
        public Guid EmployeeId { get; private set; }

        public decimal RegularHours { get; private set; }
        public decimal OvertimeHours { get; private set; }

        public decimal GrossPay { get; private set; }
        public decimal TotalDeductions { get; private set; }
        public decimal TotalTaxes { get; private set; }
        public decimal NetPay { get; private set; }

        private PayrollEntry()
        {
        }

        public PayrollEntry(
            Guid payrollRunId,
            Guid employeeId,
            decimal regularHours,
            decimal overtimeHours,
            decimal grossPay,
            decimal totalDeductions,
            decimal totalTaxes,
            decimal netPay)
        {
            PayrollRunId = payrollRunId;
            EmployeeId = employeeId;
            RegularHours = regularHours;
            OvertimeHours = overtimeHours;
            GrossPay = grossPay;
            TotalDeductions = totalDeductions;
            TotalTaxes = totalTaxes;
            NetPay = netPay;
        }

        public void UpdateTotals(
            decimal regularHours,
            decimal overtimeHours,
            decimal grossPay,
            decimal totalDeductions,
            decimal totalTaxes,
            decimal netPay)
        {
            RegularHours = regularHours;
            OvertimeHours = overtimeHours;
            GrossPay = grossPay;
            TotalDeductions = totalDeductions;
            TotalTaxes = totalTaxes;
            NetPay = netPay;
        }
    }
}