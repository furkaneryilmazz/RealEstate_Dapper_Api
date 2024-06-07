using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.WhoWeAreDetailDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.WhoWeAreRepository
{
    public class WhoWeAreDetailRepository : IWhoWeAreDetailRepository
    {
        private readonly Context _context;
        public WhoWeAreDetailRepository(Context context)
        {
            _context = context;
        }
        public async Task CreateWhoWeAreDetail(CreateWhoWeAreDetailDto createWhoWeAreDetailDto)
        {
            string query = "INSERT INTO WhoWeAreDetail (Tittle,Subtitle,Description1,Description2) VALUES (@tittle,@subtitle,@description1,@description2)";
            var parameters = new DynamicParameters(); //burda ne yaptık? DynamicParameters nesnesi oluşturduk. //neden bunu yaptık? parametre eklemek için.
            parameters.Add("@tittle", createWhoWeAreDetailDto.Tittle);
            parameters.Add("@subtitle", createWhoWeAreDetailDto.Subtitle);
            parameters.Add("@description1", createWhoWeAreDetailDto.Description1);
            parameters.Add("@description2", createWhoWeAreDetailDto.Description2);
            using (var connection = _context.CreateConnection()) //burda ne yaptık? connection nesnesi oluşturduk ve using bloğu içerisine aldık. //neden bunu yaptık? using bloğu içerisine aldık çünkü connection nesnesi işi bitince bellekten silinsin diye.
            {
                await connection.ExecuteAsync(query, parameters); //burda ne yaptık? connection nesnesi üzerinden ExecuteAsync metodunu çağırdık ve query ve parameters parametrelerini verdik. //neden bunu yaptık? Kategori oluşturmak için. //await neden kullandık? async işlemler için kullanılır.//ExecuteAsync neden kullandık? veritabanına insert işlemi yapmak için.
            }
        }

        public async Task DeleteWhoWeAreDetail(int id)
        {
            string query = "DELETE FROM WhoWeAreDetail Where WhoWeAreDetailID=@whoWeAreDetailID";
            var parameters = new DynamicParameters(); //burda ne yaptık? DynamicParameters nesnesi oluşturduk. //neden bunu yaptık? parametre eklemek için.
            parameters.Add("@whoWeAreDetailID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultWhoWeAreDetailDto>> GetAllWhoWeAreDetailAsync()
        {
            string query = "SELECT * FROM WhoWeAreDetail";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultWhoWeAreDetailDto>(query);//burda ne yaptık? connection nesnesi üzerinden QueryAsync metodunu çağırdık ve ResultCategoryDto türünde bir liste döndürdük. //QueryAsync ne işe yarıyor? veritabanından veri çekmek için kullanılır. //neden bunu yaptık? Tüm kategorileri getirmek için.
                return values.ToList();
            }
        }

        public async Task<GetByIDWhoWeAreDetailDto> GetWhoWeAreDetail(int id)
        {
            string query = "SELECT * FROM WhoWeAreDetail Where WhoWeAreDetailID=@whoWeAreDetailID";
            var parameters = new DynamicParameters();
            parameters.Add("whoWeAreDetailID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDWhoWeAreDetailDto>(query, parameters);
                return values;
            }
        }

        public async Task UpdateWhoWeAreDetail(UpdateWhoWeAreDetailDto updateWhoWeAreDetailDto)
        {
            string query = "UPDATE WhoWeAreDetail SET Tittle=@tittle, Subtitle=@subtitle, Description1=@description1, Description2=@description2 where WhoWeAreDetailID=@whoWeAreDetailID";
            var parameters = new DynamicParameters();
            parameters.Add("@tittle", updateWhoWeAreDetailDto.Tittle);
            parameters.Add("@subtitle", updateWhoWeAreDetailDto.Subtitle);
            parameters.Add("description1", updateWhoWeAreDetailDto.Description1);
            parameters.Add("description2", updateWhoWeAreDetailDto.Description2);
            parameters.Add("whoWeAreDetailID", updateWhoWeAreDetailDto.WhoWeAreDetailID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
