using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewgenWebsoftBatch30.DataContext;
using System.Diagnostics;

namespace NewgenWebsoftBatch30.Controllers
{
    public class DemoEmpController : Controller
    {
        private readonly NewgenWebsoftDbContext db;
        
        public DemoEmpController(NewgenWebsoftDbContext dbContext)
        {
            this.db = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var empList = await db.Employees.Include(e => e.Department).ToListAsync();
            return View(empList);
        }


    }
}
