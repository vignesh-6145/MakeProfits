using MakeProfits.Backend.Models;
using MakeProfits.Models;

namespace MakeProfits.Backend.Repository
{
    public interface IUserDataAccess
    {
        void RegisterUser(User user);
        UserDTO getUserByUserName(string UserName);
        UserDTO GetUserById(int UserID);
        bool ValidateUser(String Username, String Password);

    }
}
