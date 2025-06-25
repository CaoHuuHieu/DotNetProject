
namespace JobBoard.Domain.Entity;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    public void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
    
}