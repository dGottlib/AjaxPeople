using Homework52.Data;
using Homework52.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Homework52.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString =
           @"Data Source=.\sqlexpress;Initial Catalog=ClassWorkTables;Integrated Security=true;";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var repo = new PeopleRepository(_connectionString);
            var people = repo.GetPeople();
            return Json(people);
        }
        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PeopleRepository(_connectionString);
            if(person == null)
            {
                return;
            }
            repo.AddPerson(person);
        }       
    }
}
