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

        public IActionResult Index(int TeamId, string TeamName)
        {

            //var dataset = _repo.Bowlers.Include("Team").ToList();
            if(TeamId == 0)
            {
                ViewBag.teamname = "Bowlers";
            }
            else
            {
            ViewBag.teamname = _repo.Teams
                .Single(x => x.TeamID == TeamId).TeamName.ToString();
            }

            var dataset = _repo.Bowlers
                .Include("Team")
                .Where(t => t.TeamID == TeamId || TeamId == 0)
                .OrderBy(t => t.BowlerLastName)
                .ToList();
            return View(dataset);
        }
        [HttpGet]
        public IActionResult Delete( int bowlerid)
        {
           

            var deleteapp = _repo.Bowlers.Single(x => x.BowlerID == bowlerid);

            _repo.DeleteBowler(deleteapp);

            return View("Delete", deleteapp);
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
       
       
    }
}
