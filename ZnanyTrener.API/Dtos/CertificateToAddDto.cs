using System;

namespace ZnanyTrener.API.Dtos
{
    public class CertificateToAddDto
    {
        public string Number { get; set; }
        public string Institution { get; set; }
        public DateTime GainDate { get; set; }
        public int UserId { get; set; }

    }
}