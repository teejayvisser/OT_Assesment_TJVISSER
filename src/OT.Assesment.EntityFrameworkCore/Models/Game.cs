using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assesment.EntityFrameworkCore.Models
{
    [Index(nameof(Name))]
    [Table("Games")]
    public class Game
    {
        public Guid Id { get; set; }
        public Guid ProviderId { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
    }
}
