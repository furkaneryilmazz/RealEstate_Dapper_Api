using RealEstate_Dapper_Api.Dtos.CategoryDtos;

namespace RealEstate_Dapper_Api.Repositories.CategoryRepository
{
    public interface ICategoryRepository //burda napıyoruz? CategoryRepository için CRUD işlemlerini tanımlıyoruz.
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync(); //Tüm kategorileri getir //task neden kullanıyoruz? async işlemler için kullanılır.
        Task CreateCategory(CreateCategoryDto categoryDto); //Kategori oluştur //neden bunu yaptık? Kategori oluşturmak için.
        Task DeleteCategory(int id);//neden bunu yaptık? Kategori silmek için.
        Task UpdateCategory(UpdateCategoryDto categoryDto); //Kategori güncelle //neden bunu yaptık? Kategori güncellemek için.
        Task<GetByIDCategoryDto> GetCategory(int id); //Kategori getir //neden bunu yaptık? Kategori getirmek için.

    }
}
