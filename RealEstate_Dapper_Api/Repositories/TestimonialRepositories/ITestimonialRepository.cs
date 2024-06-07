using RealEstate_Dapper_Api.Dtos.TestimonialDtos;

namespace RealEstate_Dapper_Api.Repositories.TestimonialRepositories
{
    public interface ITestimonialRepository
    {
        Task<List<ResultTestimonialDto>> GetAllTestimonialAsync();
        Task CreateTestimonial(CreateTestimonialDto createTestimonialDto);
        Task DeleteTestimonial(int id);
        Task UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto);
        Task<GetTestimonialDto> GetTestimonial(int id);
    }
}
