using System.Collections.Generic;
using System.Linq;
using LIcensesPO.DbConfig;
using LIcensesPO.Models;
using Microsoft.EntityFrameworkCore;

namespace LIcensesPO.Services;

public class LicenseService: BaseService<License>
{
    public  IEnumerable<License> GetAll()
    {
        using (var dbContext = new AppDbContext())
        {
            return dbContext.Licenses
                .Include(l => l.Computer)
                .Include(l => l.Licensor)
                .Include(l => l.Prog)
                .ToList();
        }
    }

    public License GetById(long id)
    {
        using (var db = new AppDbContext())
        {
            return db.Licenses.Where(u => u.Id == id)
                .Include(l => l.Computer)
                .Include(l => l.Licensor)
                .Include(l => l.Prog)
                .FirstOrDefault();
        }
    }
}