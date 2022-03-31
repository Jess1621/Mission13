using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mission13.Models;

namespace Mission13.Components
{
    public class TypesViewComponent : ViewComponent
    {
        private IBowlersRepository _repo { get; set; }

        public TypesViewComponent (IBowlersRepository temp)
        {
            _repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["team"];

            var types = _repo.Teams
                .Select(x => x.TeamName)
                .Distinct()
                .OrderBy(x => x);

            return View(types);
        }
    }
}
