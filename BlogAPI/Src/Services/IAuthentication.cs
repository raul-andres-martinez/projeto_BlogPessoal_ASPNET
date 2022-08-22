using BlogAPI.Src.Models;
using System.Threading.Tasks;

namespace BlogAPI.Src.Services
{
    public interface IAuthentication
    {
        string EncodePassword(string password);
        Task CreateNoDuplicateUserAsync(User user);
        string GenerateToken(User user);
    }
}
