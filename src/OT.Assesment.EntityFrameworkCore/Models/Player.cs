using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OT.Assesment.EntityFrameworkCore.Models;

[Index(nameof(Username))]
[Table("Players")]
public class Player
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public virtual IEnumerable<Wager> Wagers { get; set; }
}