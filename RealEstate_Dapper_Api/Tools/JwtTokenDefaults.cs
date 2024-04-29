namespace RealEstate_Dapper_Api.Tools
{
    public class JwtTokenDefaults
    {
        public const string ValidAudience = "https://localhost"; //audience: dinleyici
        public const string ValidIssuer = "https://localhost"; //bu adreslerin gerçek olması gerekiyor //issuer: yayıncı
        public const string Key = "Realestate..0102030405Asp.NetCore8.0.1+-*/"; //bunun burada olmaması gerekiyor veritabanında tutulmalı
        public const int Expire = 5; //5 dakika //token süresi
    }
}
