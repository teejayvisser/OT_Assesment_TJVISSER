using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace OT.Assesment.EntityFrameworkCore.Dto.Wager
{
    [AutoMap(typeof(Models.Wager))]
    public class WagerByPlayerDto
    {
        public Guid WagerId { get; set; }
        public string Game { get; set; }
        public string Provider { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
