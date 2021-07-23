using System.Text.Json.Serialization;

namespace API_OutBound.Infrastructure.Jwt
{
    public class TokenManagement
    {
        [JsonPropertyName("secret")]
        public string Secret { get; set; }

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("audience")]
        public string Audience { get; set; }

        [JsonPropertyName("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonPropertyName("refreshExpiration")]
        public int RefreshExpiration { get; set; }
        /// <summary>
        /// Danh sách tài khoản
        /// </summary>
        [JsonPropertyName("listUsers")]
        public UserLogins[] ListUsers { get; set; }
    }

    public class UserLogins
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public class CallOutManagement
    {
        [JsonPropertyName("URL")]
        public string URL { get; set; }
    }
}
