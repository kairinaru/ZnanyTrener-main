using System;

namespace ZnanyTrener.API.Dtos
{
    public class TrainingToAddDto
    {
        public int CoachId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}