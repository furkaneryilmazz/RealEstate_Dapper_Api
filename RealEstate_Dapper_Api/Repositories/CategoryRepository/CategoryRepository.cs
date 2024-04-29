using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async void CreateCategory(CreateCategoryDto categoryDto)
        {
            string query = "INSERT INTO Category (CategoryName,CategoryStatus) VALUES (@categoryName,@categoryStatus)";
            var parameters = new DynamicParameters(); //burda ne yaptık? DynamicParameters nesnesi oluşturduk. //neden bunu yaptık? parametre eklemek için.
            parameters.Add("@categoryName", categoryDto.CategoryName);
            parameters.Add("@categoryStatus", true);
            using (var connection = _context.CreateConnection()) //burda ne yaptık? connection nesnesi oluşturduk ve using bloğu içerisine aldık. //neden bunu yaptık? using bloğu içerisine aldık çünkü connection nesnesi işi bitince bellekten silinsin diye.
            {
                await connection.ExecuteAsync(query, parameters); //burda ne yaptık? connection nesnesi üzerinden ExecuteAsync metodunu çağırdık ve query ve parameters parametrelerini verdik. //neden bunu yaptık? Kategori oluşturmak için. //await neden kullandık? async işlemler için kullanılır.//ExecuteAsync neden kullandık? veritabanına insert işlemi yapmak için.
            }
        }

        public async void DeleteCategory(int id)
        {
            string query = "DELETE FROM Category Where CategoryID=@categoryID";
            var parameters = new DynamicParameters(); //burda ne yaptık? DynamicParameters nesnesi oluşturduk. //neden bunu yaptık? parametre eklemek için.
            parameters.Add("@categoryID",id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            string query = "SELECT * FROM Category";
            using(var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryDto>(query);//burda ne yaptık? connection nesnesi üzerinden QueryAsync metodunu çağırdık ve ResultCategoryDto türünde bir liste döndürdük. //QueryAsync ne işe yarıyor? veritabanından veri çekmek için kullanılır. //neden bunu yaptık? Tüm kategorileri getirmek için.
                return values.ToList();
            }
        }

        public async Task<GetByIDCategoryDto> GetCategory(int id)
        {
            string query = "SELECT * FROM Category Where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("categoryID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDCategoryDto>(query, parameters);
                return values;
            }
        }

        public async void UpdateCategory(UpdateCategoryDto categoryDto)
        {
            string query = "UPDATE Category SET CategoryName=@categoryName, CategoryStatus=@categoryStatus where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", categoryDto.CategoryName);
            parameters.Add("@categoryStatus", categoryDto.CategoryStatus);
            parameters.Add("categoryId", categoryDto.CategoryID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
