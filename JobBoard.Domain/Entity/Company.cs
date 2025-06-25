using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Domain.Entity;

public class Company : BaseEntity
{
    [Column("name")]
    public required string Name { get; set; }

    [Column("code")]
    public required string Code { get; set; }

    [Column("website")]
    public string Website { get; set; } = string.Empty;

    [Column("email")]
    public required string Email { get; set; }

    [Column("address")]
    public required string Address { get; set; }
}