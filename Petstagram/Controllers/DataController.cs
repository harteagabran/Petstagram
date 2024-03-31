using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Petstagram.Models;
using Petstagram.Repositories;
using Petstagram.Services;

namespace Petstagram.Controllers
{
    public class DataController : Controller
    {
        private IRepository _db;
        private StructureService _struc;
        private UserManager<User> _user;
        public DataController(IRepository db, StructureService struc, UserManager<User> user)
        {
            _db = db;
            _struc = struc;
            _user = user;
        }

        public IActionResult ShowPics()
        {
            var user = _user.GetUserId(User);
            List<Picture> pics = _db.GetAllPicsByOwner(user);
            pics = _struc.SelectionSortByDate(pics);
            ViewBag.Pics = pics;
            return View();
        }

        public IActionResult ShowPets()
        {
            var user = _user.GetUserId(User);
            List<Pet> pets = _db.GetAllPetsByOwner(user);
            ViewBag.Pets = pets;
            return View();
        }

        public IActionResult DetailPic(int id, string prevurl)
        {
            Picture pic = _db.GetPicById(id);
            ViewBag.Pic = pic;
            ViewBag.PrevUrl = !string.IsNullOrEmpty(prevurl) ? prevurl : Url.Action("Index", "Pet");
            return View();
        }
    }
}
