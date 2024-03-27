using Microsoft.AspNetCore.Mvc;
using Petstagram.Models;
using Petstagram.Repositories;

namespace Petstagram.Controllers
{
    public class DataController : Controller
    {
        private IRepository _db;
        public DataController(IRepository db)
        {
            _db = db;
        }

        public IActionResult ShowPics()
        {
            List<Picture> pics = _db.GetAllPics();
            ViewBag.Pics = pics;
            return View();
        }

        public IActionResult DetailPic(int id)
        {
            Picture pic = _db.GetPicById(id);
            ViewBag.Pic = pic;
            return View();
        }
    }
}
