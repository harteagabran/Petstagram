using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Petstagram.Models;
using Petstagram.Repositories;

namespace Petstagram.Controllers
{
    public class PetController : Controller
    {
        private IRepository _db;
        private readonly UserManager<User> _userManager;
        public PetController(IRepository repo, UserManager<User> user)
        {
            _db = repo;
            _userManager = user;
        }
        public IActionResult Index()
        {
            bool noob = _db.HasPetData();
            ViewBag.empty = noob;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddPet()
        {
            ViewBag.Action = "Add";
            var model = new FormPet();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            model.OwnerId = user.Id;
            return View("EditPet", model);
        }

        [HttpPost]
        public IActionResult EditPet(FormPet fpet)
        {
            if(ModelState.IsValid)
            {
                Pet pet = new Pet()
                {
                    Id = fpet.Id,
                    Name = fpet.Name,
                    OwnerId = fpet.OwnerId,
                };

                if(pet.Id == 0)
                {
                    _db.AddPet(pet);
                } else
                {
                    _db.UpdatePet(pet);
                }

                return RedirectToAction("Index");
            } else
            {
                ViewBag.Action = (fpet.Id == 0) ? "Add" : "Edit";

                return View(fpet);
            }
        }
    }
}
