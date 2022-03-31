using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository _repo { get; set; }

        //Constructor
        public HomeController(IBowlersRepository temp)
        {
            _repo = temp;
        }
        public IActionResult Index(string team)
        {
            var bowlers = _repo.Bowlers
                .Where(b => b.Team.TeamName == team || team == null)
                .ToList();

            //To display the team name when a team is selected.
            if (team == null || team == "")
                {
                ViewBag.teamName = "";
                }
            else
            {
                ViewBag.teamName = "For " + team;
            }

            return View(bowlers);
        }

        //Form Controller
        [HttpGet]
        public IActionResult BowlerForm()
        {
            return View();
        }

        //On Form Submission
        [HttpPost]
        public IActionResult BowlerForm(Bowler b)
        {
            _repo.AddBowler(b);
            return RedirectToAction("Index");
        }
        
        //Edit Bowler
        [HttpGet]
        public IActionResult EditBowler(int bowlerid)
        {
            var application = _repo.Bowlers.Single(x => x.BowlerID == bowlerid);
            return View("BowlerForm", application);
        }

        [HttpPost]
        public IActionResult EditBowler(Bowler b)
        {
            _repo.EditBowler(b);
            return RedirectToAction("Index");
        }

        //Delete Bowler
        [HttpGet]
        public IActionResult Delete(int bowlerid)
        {
            var application = _repo.Bowlers.Single(x => x.BowlerID == bowlerid);
            return View(application);
        }

        [HttpPost]
        public IActionResult Delete (Bowler b)
        {
            _repo.RemoveBowler(b);
            return RedirectToAction("Index");
        }
    }
}
