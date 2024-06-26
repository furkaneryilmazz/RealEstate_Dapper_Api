﻿using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.ProductDetailDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;
        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "SELECT * FROM Product";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDto>(query);//burda ne yaptık? connection nesnesi üzerinden QueryAsync metodunu çağırdık ve ResultProductDto türünde bir liste döndürdük. //QueryAsync ne işe yarıyor? veritabanından veri çekmek için kullanılır. //neden bunu yaptık? Tüm ürünleri getirmek için.
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            string query = "SELECT ProductID,Tittle,Price,City,District,CategoryName,CoverImage,SlugUrl,Type,Address,DealOfTheDay  FROM Product inner join Category on Product.ProductKategory=Category.CategoryID";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync()
        {
            string query = "Select Top(5) ProductID, Tittle,Price,City,District,ProductKategory,CategoryName,AdvertisementDate From Product Inner Join Category on Product.ProductKategory=Category.CategoryID Where Type='Kiralık' Order By ProductID Desc ";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDto>(query);//burda ne yaptık? connection nesnesi üzerinden QueryAsync metodunu çağırdık ve ResultProductDto türünde bir liste döndürdük. //QueryAsync ne işe yarıyor? veritabanından veri çekmek için kullanılır. //neden bunu yaptık? Tüm ürünleri getirmek için.
                return values.ToList();
            }
        }

        public async Task ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            string query = "Update Product Set DealOfTheDay=0 where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task ProductDealOfTheDayStatusChangeToTrue(int id)
        {
            string query = "Update Product Set DealOfTheDay=1 where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByTrue(int id)
        {
            string query = "SELECT ProductID,Tittle,Price,City,District,CategoryName,CoverImage,Type,Address,DealOfTheDay  FROM Product inner join Category on Product.ProductKategory=Category.CategoryID where EmployeeID=@employeeID and ProductStatus=1";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByFalse(int id)
        {
            string query = "SELECT ProductID,Tittle,Price,City,District,CategoryName,CoverImage,Type,Address,DealOfTheDay  FROM Product inner join Category on Product.ProductKategory=Category.CategoryID where EmployeeID=@employeeID and ProductStatus=0";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task CreateProduct(CreateProductDto createProductDto)
        {
            string query = "insert into Product (Tittle,Price,City,District,CoverImage,Address,Description,Type,DealOfTheDay,AdvertisementDate,ProductStatus,ProductKategory,EmployeeID) values (@Title,@Price,@City,@District,@CoverImage,@Address,@Description,@Type,@DealOfTheDay,@AdvertisementDate,@ProductStatus,@ProductCategory,@EmployeeID)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", createProductDto.Tittle);
            parameters.Add("@Price", createProductDto.Price);
            parameters.Add("@City", createProductDto.City);
            parameters.Add("@District", createProductDto.District);
            parameters.Add("@CoverImage", createProductDto.CoverImage);
            parameters.Add("@Address", createProductDto.Address);
            parameters.Add("@Description", createProductDto.Description);
            parameters.Add("@Type", createProductDto.Type);
            parameters.Add("@DealOfTheDay", createProductDto.DealOfTheDay);
            parameters.Add("@AdvertisementDate", createProductDto.AdvertisementDate);
            parameters.Add("@ProductStatus", createProductDto.ProductStatus);
            parameters.Add("@ProductCategory", createProductDto.ProductKategory);
            parameters.Add("@EmployeeID", createProductDto.EmployeeID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetProductByProductIdDto> GetProductByProductId(int id)
        {
            string query = "SELECT ProductID,Tittle,Description,Price,City,District,CategoryName,CoverImage,Type,Address,DealOfTheDay,AdvertisementDate,SlugUrl FROM Product inner join Category on Product.ProductKategory=Category.CategoryID where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductByProductIdDto>(query, parameters);
                return values.FirstOrDefault();
            }
        }

        public async Task<GetProductDetailByIdDto> GetProductDetailByProductId(int id)
        {
            string query = "Select * From ProductDetails Where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductDetailByIdDto>(query, parameters);
                return values.FirstOrDefault();
            }
        }

		public async Task<List<ResultProductWithSearchListDto>> ResultProductWithSearchList(string searchKeyValue, int propertyCategoryId, string city)
		{
			string query = "Select * From Product Where Tittle Like '%"+ searchKeyValue + "%' And ProductKategory=@propertyCategoryId And City=@city";
			var parameters = new DynamicParameters();
			parameters.Add("@searchKeyValue", searchKeyValue);
			parameters.Add("@propertyCategoryId", propertyCategoryId);
			parameters.Add("@city", city);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultProductWithSearchListDto>(query, parameters);
				return values.ToList();
			}
		}

		public async Task<List<ResultProductWithCategoryDto>> GetProductByDealOftheDayTrueWithCategoryAsync()
		{
			string query = "SELECT ProductID,Tittle,Price,City,District,CategoryName,CoverImage,Type,Address,DealOfTheDay  FROM Product inner join Category on Product.ProductKategory=Category.CategoryID where DealOfTheDay=1";
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
				return values.ToList();
			}
		}

        public async Task<List<ResultLast3ProductWithCategoryDto>> GetLast3ProductAsync()
        {
            string query = "Select Top(3) ProductID, Tittle,Price,City,District,CoverImage,ProductKategory,CategoryName,Description,AdvertisementDate From Product Inner Join Category on Product.ProductKategory=Category.CategoryID Order By ProductID Desc ";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast3ProductWithCategoryDto>(query);//burda ne yaptık? connection nesnesi üzerinden QueryAsync metodunu çağırdık ve ResultProductDto türünde bir liste döndürdük. //QueryAsync ne işe yarıyor? veritabanından veri çekmek için kullanılır. //neden bunu yaptık? Tüm ürünleri getirmek için.
                return values.ToList();
            }
        }
    }
}
