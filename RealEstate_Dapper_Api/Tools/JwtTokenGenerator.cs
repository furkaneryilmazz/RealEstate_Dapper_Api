using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate_Dapper_Api.Tools
{
    public class JwtTokenGenerator
    {
        //statik bir metot oluşturduk newlemeden direkt sınıf üzerinden erişebilelim diye
        public static TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel model)
        {
            var claims = new List<Claim>();
            if (!string.IsNullOrWhiteSpace(model.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, model.Role));
            }
            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()));

            if (!string.IsNullOrWhiteSpace(model.UserName))
            {
                claims.Add(new Claim("UserName", model.UserName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);
            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer, audience: JwtTokenDefaults.ValidAudience, claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: signinCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return new TokenResponseViewModel(tokenHandler.WriteToken(token), expireDate);

        }
    }
}

//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace RealEstate_Dapper_Api.Tools
//{
//    public class JwtTokenGenerator
//    {
//        // Token oluşturmak için nesne oluşturmadan doğrudan sınıf üzerinden erişebilelim diye statik bir metot
//        public static TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel model)
//        {
//            // 1. Talepler (Token'a Dahil Edilecek Veriler) Oluşturma
//            var claims = new List<Claim>();

//            // Modelde Rol özelliği varsa Rol talebi ekle
//            if (!string.IsNullOrWhiteSpace(model.Role))
//            {
//                claims.Add(new Claim(ClaimTypes.Role, model.Role));
//            }

//            // Zorunlu talepler ekle: Kullanıcı Kimliği ve (isteğe bağlı) Kullanıcı Adı
//            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()));
//            if (!string.IsNullOrWhiteSpace(model.UserName))
//            {
//                claims.Add(new Claim("UserName", model.UserName));
//            }

//            // 2. Güvenlik Anahtarı Oluşturma (Token'ı İmzalaymak için Kullanılır)
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

//            // 3. İmza Bilgileri Oluşturma (Anahtarı ve imza algoritmasını birleştirir)
//            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            // 4. Token Süresi Ayarlama (İstenilen süre için JwtTokenDefaults.Expire'ı ayarlayın)
//            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

//            // 5. JWT Token Oluşturma
//            JwtSecurityToken token = new JwtSecurityToken(
//                issuer: JwtTokenDefaults.ValidIssuer, // Token'ı veren taraf (örneğin, uygulamanız)
//                audience: JwtTokenDefaults.ValidAudience, // Token'ın hedef kitlesi (örneğin, API'niz)
//                claims: claims, // Dahil edilecek talepler (veri) listesi
//                notBefore: DateTime.UtcNow, // Token'ın geçerli olma tarihi
//                expires: expireDate, // Token'ın süresi dolma tarihi
//                signingCredentials: signinCredentials);

//            // 6. Token İşleyicisi Oluşturma ve Token Dizgisi Yazma
//            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
//            return new TokenResponseViewModel(tokenHandler.WriteToken(token), expireDate);
//        }
//    }
//}
