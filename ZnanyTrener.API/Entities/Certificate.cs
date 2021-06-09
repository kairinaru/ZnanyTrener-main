using System;

namespace ZnanyTrener.API.Entities
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Institution { get; set; }
        public DateTime GainDate { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}