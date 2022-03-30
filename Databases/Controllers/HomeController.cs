using Databases.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Databases.Controllers
{
    public class HomeController : Controller
    {
        // Underscore  = private
        private IBowlerRepository _repo { get; set; } 

        public HomeController(IBowlerRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {

            var dataset = _repo.Bowlers.Include("Team").ToList();

            // create new Team  with SQL that connects with the route of Default buttons


            return View(dataset);
        }
        [HttpGet]
        public IActionResult Delete( int bowlerid)
        {
           

            var deleteapp = _repo.Bowlers.Single(x => x.BowlerID == bowlerid);

            _repo.DeleteBowler(deleteapp);

            return View("Delete");
        }

     
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Teams = _repo.Teams.ToList();

            Bowler bowler = new Bowler();

            return View("Edit", bowler);
        }

        [HttpPost]
        public IActionResult Add(Bowler b)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveBowler(b);
                

                return View("Index", b);
            }
            else
            {
                ViewBag.Teams = _repo.Teams.ToList();
                return View("Edit");
            }
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Teams = _repo.Teams.ToList();
            var editapp = _repo.Bowlers.Single(x => x.BowlerID== bowlerid);
            return View("Edit", editapp);
        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
                _repo.SaveBowler(b);

            return RedirectToAction("Index");

        }
        public IActionResult TeamButtons (string TeamName)
        {
            var x = new Team
            {
                TeamName = _repo.Bowlers
                .Where (t => t.TeamName)
            }
        }
       
    }
}
