using System;

namespace ZnanyTrener.API.Dtos
{
    public class TrainingToReturnDto
    {
        public int Id { get; set; }
        public int CoachId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhoneNumber { get; set; }
        public string CoachFirstName { get; set; }
        public string CoachLastName { get; set; }
        public string CoachPhoneNumber { get; set; }
    }
}