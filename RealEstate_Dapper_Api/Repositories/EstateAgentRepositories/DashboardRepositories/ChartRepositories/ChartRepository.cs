using Dapper;
using RealEstate_Dapper_Api.Dtos.ChartDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.EstateAgentRepositories.DashboardRepositories.ChartRepositories
{
    public class ChartRepository : IChartRepository
    {
        private readonly Context _context;
        public ChartRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultChartDto>> Get5CityForChart()
        {
            string query = "SELECT Top(5) City,Count(*) as 'CityCount' FROM Product Group By City Order By CityCount Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultChartDto>(query);//burda ne yaptık? connection nesnesi üzerinden QueryAsync metodunu çağırdık ve ResultCategoryDto türünde bir liste döndürdük. //QueryAsync ne işe yarıyor? veritabanından veri çekmek için kullanılır. //neden bunu yaptık? Tüm kategorileri getirmek için.
                return values.ToList();
            }
        }
    }
}
