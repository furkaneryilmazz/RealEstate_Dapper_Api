using Microsoft.Data.SqlClient;
using System.Data;

namespace RealEstate_Dapper_Api.Models.DapperContext
{
    public class Context
    {
        private readonly IConfiguration _configuration; //bunlar neden private? çünkü bu değişkenler sadece bu sınıf içerisinde kullanılacak. //readonly ne işe yarar? sadece okunabilir bir değişken olmasını sağlar.
        private readonly string _connectionString;

        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("connection"); //burada ne yaptık? _configuration nesnesi üzerinden GetConnectionString metodunu çağırdık ve connection adındaki connection stringi aldık ve _connectionString değişkenine atadık.
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString); //burada ne yaptık? IDbConnection türünde bir nesne oluşturduk ve bu nesneyi geri döndürdük. Bu nesne SqlConnection türünde olacak ve connection stringi parametre olarak alacak.
        
    }
}
