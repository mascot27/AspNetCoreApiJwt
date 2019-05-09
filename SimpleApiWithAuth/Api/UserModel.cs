using AspNetCore.Security.Jwt;

namespace SimpleApiWithAuth.Api
{
    public class UserModel : IAuthenticationUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}