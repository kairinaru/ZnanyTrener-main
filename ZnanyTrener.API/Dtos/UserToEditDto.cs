namespace ZnanyTrener.API.Dtos
{
    public class UserToEditDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }
}