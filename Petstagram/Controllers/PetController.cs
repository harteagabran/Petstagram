using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Petstagram.Models;
using Petstagram.Repositories;
using Petstagram.Services;

namespace Petstagram.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        private IRepository _db;
        private readonly UserManager<User> _userManager;
        private HtmlSanitizerService _html;
        private IWebHostEnvironment _wwwroot;
        public PetController(IRepository repo, UserManager<User> user, HtmlSanitizerService html, IWebHostEnvironment wwwroot)
        {
            _db = repo;
            _userManager = user;
            _html = html;
            _wwwroot = wwwroot;
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
            if(ModelState.IsValid && _html.IsValid(fpet.Name))
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
                if(!_html.IsValid(fpet.Name))
                {
                    ModelState.AddModelError("Name", "Name cannot contain  <, >, &, ', or \"");
                }

                return View(fpet);
            }
        }

        [HttpGet]
        public IActionResult AddPic()
        {
            //if no pets redirect to Pets form
            if(!_db.HasPetData())
            {
                return RedirectToAction("AddPet");
            }

            var model = new FormPicture();

            ViewBag.Action = "Add";
            ViewBag.Pets = _db.GetAllPets();

            return View("EditPic", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPic(FormPicture fpet)
        {
            if (ModelState.IsValid)
            {
                Picture pic;

                //if new make new
                if(fpet.Id == 0)
                {
                    pic = new Picture();
                } else
                {
                    pic = _db.GetPicById(fpet.Id);
                }

                //set properties
                pic.Id = fpet.Id;
                pic.DisplayName = _html.Sanitize(fpet.DisplayName);
                pic.Story = _html.Sanitize(fpet.Story);
                pic.FileName = (pic.Id == 0) ? Guid.NewGuid().ToString() + Path.GetExtension(fpet.Picture.FileName) : pic.FileName;

                //add all Pets from ids to make connection
                List<Pet> petsToAdd = new List<Pet>();
                foreach (var petId in fpet.PetIds)
                {
                    petsToAdd.Add(_db.GetPetById(petId));
                }
                pic.Pets = petsToAdd;

                //add picture to system
                if(_html.AlllowedPicture(fpet.Picture))
                {
                    string image = pic.FileName;
                    string folder = "petpictures/";

                    string path = Path.Combine(_wwwroot.WebRootPath, folder + image);

                    await fpet.Picture.CopyToAsync(new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));
                }

                //picture manager

                if (pic.Id == 0)
                {
                    //make uploaddate now
                    pic.UploadDate = DateTime.Now;
                    _db.AddPic(pic);
                }
                else
                {
                    //keep uplaod date
                    _db.UpdatePic(pic);
                }
                return RedirectToAction("Index");
            }

            ViewBag.Pets = _db.GetAllPets().ToList();
            ViewBag.Action = (fpet.Id == 0) ? "Add" : "Edit";

            return View(fpet);
        }
    }
}
