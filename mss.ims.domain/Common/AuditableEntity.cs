using mss.ims.domain.Common;
using System;

namespace mss.ims.domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAtUtc { get; private set; }
    public string CreatedBy { get; private set; }

    public DateTime? UpdatedAtUtc { get; private set; }
    public string UpdatedBy { get; private set; }

    public void SetCreated(string createdBy)
    {
        CreatedAtUtc = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public void SetUpdated(string updatedBy)
    {
        UpdatedAtUtc = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}