using System;

namespace ZnanyTrener.API.Entities
{
    public class Training
    {
        public int Id { get; set; }
        public int CoachId { get; set; }
        public int UserId { get; set; }
        public AppUser Coach { get; set; }
        public AppUser User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}