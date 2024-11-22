using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assesment.EntityFrameworkCore.Models
{
    [Index(nameof(ProviderName))]
    [Table("Providers")]
    public class Provider
    {
        public Guid Id { get; set; }
        public string ProviderName { get; set; }
        public virtual IEnumerable<Game> Games { get; set; }
    }
}
