using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EFCoreDemo.Models;

namespace EFCoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DemoDbContext _dbContext;
        public HomeController(ILogger<HomeController> logger, DemoDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            _dbContext.Students.Add(new Domain.Student() {  Name = "暂时", Sex = "女" });
            var students1 = _dbContext.Students.Find(1);
            students1.Name = students1.Name + "追加";
            // var students=  _dbContext.Students.Find(3);

            //_dbContext.Remove(students);
            _dbContext.SaveChanges();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
