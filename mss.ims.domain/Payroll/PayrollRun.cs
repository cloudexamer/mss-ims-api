namespace mss.ims.domain.Payroll
{
    using System;
    using mss.ims.domain.Common;

    public class PayrollRun : AuditableEntity
    {
        public string RunNumber { get; private set; }
        public DateTime PeriodStartUtc { get; private set; }
        public DateTime PeriodEndUtc { get; private set; }
        public DateTime PayDateUtc { get; private set; }

        public string Status { get; private set; }

        private PayrollRun()
        {
        }

        public PayrollRun(
            string runNumber,
            DateTime periodStartUtc,
            DateTime periodEndUtc,
            DateTime payDateUtc)
        {
            RunNumber = runNumber;
            PeriodStartUtc = periodStartUtc;
            PeriodEndUtc = periodEndUtc;
            PayDateUtc = payDateUtc;
            Status = "Draft";
        }

        public void Post()
        {
            Status = "Posted";
        }

        public void Cancel()
        {
            Status = "Cancelled";
        }
    }
}