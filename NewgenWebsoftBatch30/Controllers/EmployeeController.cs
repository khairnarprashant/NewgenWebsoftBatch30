using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewgenWebsoftBatch30.DataContext;
using NewgenWebsoftBatch30.Models;

namespace NewgenWebsoftBatch30.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly NewgenWebsoftDbContext db;

        public EmployeeController(NewgenWebsoftDbContext dbContext)
        {
            this.db = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var EmployeelList= await db.Employees.Include(d=> d.Department).ToListAsync();

            return View(EmployeelList);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await db.Departments.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return View(employee);
            }

            await db.Employees.AddAsync(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0) 
            {
                var get = await db.Employees.FirstOrDefaultAsync(p => p.EmpId == id);
                if (get != null)
                {
                    ViewBag.Departments = new SelectList(db.Departments, "DeptId", "DeptName", get.DeptId);
                    return View(get);
                }
            }

            return NotFound(); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employeem)
        {
            if(ModelState.IsValid)
            {
                db.Employees.Update(employeem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            ViewBag.Departments = new SelectList(db.Departments, "DeptId", "DeptName", employeem.DeptId);
            return View(employeem);
        }

        [HttpGet]
        public async Task<IActionResult>Details(int id)

        {
            var getdetails = await db.Employees.Include(d => d.Department).FirstOrDefaultAsync(i => i.EmpId == id);
            if(getdetails!=null)
            {
                return View(getdetails);
            }
            return View();

        }

        [HttpGet]


        public async Task<IActionResult>Delete(int id)
        {
            var emp = await db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmpId == id);
            if(emp!=null)
            {
                return View(emp);
            }
            return View();
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult>Deletedpt(int id)
        {
            var det = await db.Employees.Include(d => d.Department).FirstOrDefaultAsync(i => i.EmpId == id);
            if (det != null)
            {
                db.Employees.RemoveRange(det);
                 await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return NotFound();
        }
    }
}
