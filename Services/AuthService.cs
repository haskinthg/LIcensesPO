using System.Linq;
using LIcensesPO.DbConfig;
using LIcensesPO.Models;

namespace LIcensesPO.Services;

public class AuthService
{
    public bool Login(string login, string password)
    {
        bool isLogin;
        using (AppDbContext db = new AppDbContext())
        {
            var user = db.Users.SingleOrDefault(u => u.Login == login);;
            return user != null;
        }
    }

    public string Register(User user)
    {
        user.URole = "Пользователь";
        using (AppDbContext db = new AppDbContext())
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        return user.Login;
    }
}