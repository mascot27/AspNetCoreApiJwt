using System.Threading.Tasks;
using AspNetCore.Security.Jwt;

namespace SimpleApiWithAuth.Api
{
    public class Authenticator : IAuthentication<UserModel>
    {
        /// <summary>
        /// Verify that the user credentials are ok
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true if the user is valid</returns>
        public async Task<bool> IsValidUser(UserModel user)
        {
            // TODO: !!! implement user credential verification here
            if (user.Name == "admin" && user.Password == "Admin123")
            {
                return true;
            }

            return false;
        }
    }
}