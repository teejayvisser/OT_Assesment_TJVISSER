using System.ComponentModel.DataAnnotations.Schema;

namespace OT.Assesment.EntityFrameworkCore.Models;

[Table("Wagers")]
public class Wager
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public int NumberOfBets { get; set; }
    public Guid PlayerId { get; set; }
    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }
    public Guid? GameId { get; set; }
    [ForeignKey("ProviderId")]
    public virtual Provider Provider { get; set; }
    public Guid? ProviderId { get; set; }

}