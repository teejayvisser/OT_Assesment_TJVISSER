﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assesment.EntityFrameworkCore.Dto.Player
{
    public class CasinoWagerDto
    {
        public Guid WagerId { get; set; }
        public string Theme { get; set; }
        public string Provider { get; set; }
        public string GameName { get; set; }
        public Guid TransactionId { get; set; }
        public Guid BrandId { get; set; }
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public Guid ExternalReferenceId { get; set; }
        public Guid TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int NumberOfBets { get; set; }
        public string CountryCode { get; set; }
        public string sessionData { get; set; }
        public int duration { get; set; }
    }
}
