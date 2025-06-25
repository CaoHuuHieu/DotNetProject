using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Domain.Entity;

public abstract class BaseEntity
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    public void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
    
}