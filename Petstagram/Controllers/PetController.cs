using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IdentityModel.Tokens;
using Petstagram.Models;
using Petstagram.Repositories;
using Petstagram.Services;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Petstagram.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        private IRepository _db;
        private HtmlSanitizerService _html;
        private IWebHostEnvironment _wwwroot;
        private UserManager<User> _user;
        public PetController(IRepository repo, HtmlSanitizerService html, IWebHostEnvironment wwwroot, UserManager<User> user)
        {
            _db = repo;
            _html = html;
            _wwwroot = wwwroot;
            _user = user;
        }
        public IActionResult Index()
        {
            var user = _user.GetUserId(User);
            bool noob = _db.HasPetData(user);
            ViewBag.empty = noob;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddPet()
        {
            ViewBag.Action = "Add";
            var model = new FormPet
            {
                Previous = Url.Action("Index", "Pet")
            };
            var user = _user.GetUserId(User);
            model.OwnerId = user;
            return View("EditPet", model);
        }
    
        [HttpGet]
        public IActionResult EditPet(int id, string prevUrl)
        {
            var pet = _db.GetPetById(id);
            var model = new FormPet
            {
                Id = pet.Id,
                Name = pet.Name,
                OwnerId = pet.OwnerId,
                Previous = !string.IsNullOrEmpty(prevUrl) ? prevUrl : Url.Action("Index", "Pet")
            };
            ViewBag.Action = "Edit";
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
                    Name = _html.Sanitize(fpet.Name),
                    OwnerId = fpet.OwnerId
                };

                if(pet.Id == 0)
                {
                    _db.AddPet(pet);
                } else
                {
                    _db.UpdatePet(pet);
                }

                return Redirect(fpet.Previous);
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
            var user = _user.GetUserId(User);
            //if no pets redirect to Pets form
            if (!_db.HasPetData(user))
            {
                return RedirectToAction("AddPet");
            }

            var model = new FormPicture();

            ViewBag.Action = "Add";
            
            List<Pet> pets = _db.GetAllPetsByOwner(user);
            pets = pets.Where(o => o.OwnerId != "Demo").ToList();
            ViewBag.Pets = pets;
            ViewBag.PathUrl = Url.Action("ShowPics", "Data");
            model.Previous = Url.Action("ShowPics", "Data");

            return View("EditPic", model);
        }

        [HttpGet]
        public IActionResult EditPic(int id, string prevUrl = "", string pathurl = "")
        {
            ViewBag.Action = "Edit";
            ViewBag.Pets = _db.GetAllPets();
            ViewBag.PathUrl = pathurl;
            var pic = _db.GetPicById(id);
            var model = new FormPicture
            {
                Id = pic.Id,
                DisplayName = WebUtility.HtmlDecode(pic.DisplayName),
                Story = WebUtility.HtmlDecode(pic.Story),
                PetIds = pic.Pets.Select(p => p.Id).ToList(),
                UploadDate = pic.UploadDate,
                Replace = false,
                Previous = !string.IsNullOrEmpty(prevUrl) ? prevUrl : Url.Action("Index", "Pet")
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPic(FormPicture fpic, string pathurl)
        {
            if (ModelState.IsValid)
            {
                Picture pic;

                //if new make new
                if(fpic.Id == 0)
                {
                    pic = new Picture();
                } else
                {
                    pic = _db.GetPicById(fpic.Id);
                }

                //set properties
                pic.Id = fpic.Id;
                pic.DisplayName = _html.Sanitize(fpic.DisplayName);
                pic.Story = _html.Sanitize(fpic.Story);
                pic.FileName = (pic.Id == 0) ? Guid.NewGuid().ToString() + Path.GetExtension(fpic.Picture.FileName) : pic.FileName;

                //add all Pets from ids to make connection
                List<Pet> petsToAdd = new List<Pet>();
                foreach (var petId in fpic.PetIds)
                {
                    petsToAdd.Add(_db.GetPetById(petId));
                }
                pic.Pets = petsToAdd;

                //if id is 0 or replace is true
                if (pic.Id == 0 || fpic.Replace)
                {
                    //add picture to system
                    if (_html.AlllowedPicture(fpic.Picture))
                    {
                        string image = pic.FileName;
                        //change image extension to new picture if replaced
                        if(fpic.Replace)
                        {
                            string rext = Path.GetExtension(fpic.Picture.FileName);
                            image = Path.ChangeExtension(image, rext);
                        }
                        string folder = "petpictures/";

                        string path = Path.Combine(_wwwroot.WebRootPath, folder + image);

                        //check if file exists and delete if any
                        string jpgPath = Path.ChangeExtension(path, ".jpg");
                        string pngPath = Path.ChangeExtension(path, ".png");

                        if (System.IO.File.Exists(jpgPath))
                        {
                            await Task.Run(() => System.IO.File.Delete(jpgPath));
                        }

                        if (System.IO.File.Exists(pngPath))
                        {
                            await Task.Run(() => System.IO.File.Delete(pngPath));
                        }

                        using (var targetStream = System.IO.File.Create(path))
                        {
                            await fpic.Picture.CopyToAsync(targetStream);
                        }
                    }
                }

                //picture manager

                if (pic.Id == 0)
                {
                    //make uploaddate now
                    pic.UploadDate = DateTime.Now;
                    pic.LastModified = DateTime.Now;
                    _db.AddPic(pic);
                }
                else
                {
                    //keep uplaod date
                    pic.LastModified = DateTime.Now;
                    _db.UpdatePic(pic);
                }
                return Redirect($"{fpic.Previous}?prevurl={pathurl}");
            }

            ViewBag.Pets = _db.GetAllPets().ToList();
            ViewBag.Action = (fpic.Id == 0) ? "Add" : "Edit";
            if(fpic.Picture == null && fpic.Replace)
            {
                ModelState.AddModelError("Picture", "Picture cannot be empty");
            }

            return View(fpic);
        }

        //Deletes make sure to prompt if they are sure to delete it
        [HttpGet]
        public IActionResult DeletePic(int id)
        {
            var model = _db.GetPicById(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult DeletePet(int id)
        {
            var model = _db.GetPetById(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult DeletePet(Pet pet)
        {
            _db.DeletePet(pet);
            return RedirectToAction("ShowPets", "Data");
        }
        [HttpPost]
        public async Task<IActionResult> DeletePic(Picture pic)
        {

            string image = pic.FileName;
            string folder = "petpictures/";

            string path = Path.Combine(_wwwroot.WebRootPath, folder + image);

            if (System.IO.File.Exists(path))
            {
                await Task.Run(() => System.IO.File.Delete(path));
            }

            _db.DeletePic(pic);
            return RedirectToAction("ShowPics", "Data");
        }
    }
}
