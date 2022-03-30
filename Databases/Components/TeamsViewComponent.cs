using Databases.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Databases.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private IBowlerRepository repo { get; set; }

        public TeamsViewComponent (IBowlerRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke ()
        {
            ViewBag.SelectedType = RouteData?.Values["TeamName"];

            var teams = repo.Teams
                .Select(x => x.TeamName)
                .Distinct()
                .OrderBy(x => x);
            return View(teams);
        }
    }
}
