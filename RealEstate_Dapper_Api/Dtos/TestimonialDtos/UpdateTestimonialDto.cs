namespace RealEstate_Dapper_Api.Dtos.TestimonialDtos
{
    public class UpdateTestimonialDto
    {
        public int TestimonialID { get; set; }
        public string NameSurname { get; set; }
        public string Tittle { get; set; }
        public string Comment { get; set; }
        public bool Status { get; set; }
    }
}
