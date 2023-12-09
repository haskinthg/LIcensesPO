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

    public string Register(string login, string password, string lname, string fname, string role)
    {
        User user = new User
        {
            Login = login, 
            Password = password,
            URole = role,
            FName = fname,
            LName = lname
        };
        using (AppDbContext db = new AppDbContext())
        {
            db.Users.Add(user);
        }

        return user.Login;
    }
}