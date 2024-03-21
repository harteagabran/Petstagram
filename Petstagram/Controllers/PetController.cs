using Microsoft.AspNetCore.Mvc;
using Petstagram.Repositories;

namespace Petstagram.Controllers
{
    public class PetController : Controller
    {
        private IRepository _repo;
        public PetController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            List<string> options = new List<string>
            {
                "Add Photo",
                "Add Pet",
                "View all Pets",
                "View all Photos"
            };
            ViewBag.options = options;
            ViewBag.petExists = _repo.hasPetData();
            return View();
        }
    }
}
