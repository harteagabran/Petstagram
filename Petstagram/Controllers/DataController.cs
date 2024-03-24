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
            return View(pics);
        }
    }
}
