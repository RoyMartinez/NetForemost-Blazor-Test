namespace Netforemost_Todo_Api.Models;

public abstract class BaseEntity
{
    public int Id { get; set; }
}

public interface IAudit
{
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }
}
public abstract class AuditableEntity : BaseEntity, IAudit
{
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public AuditableEntity()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
    public void UpdateTimestamps()
    {
        UpdatedAt = DateTime.Now;
    }
}

