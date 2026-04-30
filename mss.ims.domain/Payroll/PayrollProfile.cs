namespace mss.ims.domain.Payroll
{
    using System;
    using mss.ims.domain.Common;

    public class PayrollProfile : AuditableEntity
    {
        public Guid EmployeeId { get; private set; }

        public string PayType { get; private set; }
        public decimal PayRate { get; private set; }

        public string PaySchedule { get; private set; }
        public string TaxId { get; private set; }

        public bool IsActive { get; private set; }

        private PayrollProfile()
        {
        }

        public PayrollProfile(
            Guid employeeId,
            string payType,
            decimal payRate,
            string paySchedule,
            string taxId)
        {
            EmployeeId = employeeId;
            PayType = payType;
            PayRate = payRate;
            PaySchedule = paySchedule;
            TaxId = taxId;
            IsActive = true;
        }

        public void Update(
            string payType,
            decimal payRate,
            string paySchedule,
            string taxId)
        {
            PayType = payType;
            PayRate = payRate;
            PaySchedule = paySchedule;
            TaxId = taxId;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}