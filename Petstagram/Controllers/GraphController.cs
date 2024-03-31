using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Petstagram.Extensions;
using Petstagram.Models;
using Petstagram.Repositories;
using Petstagram.StructureFactory;

namespace Petstagram.Controllers
{
    public class GraphController : Controller
    {
        private IRepository _db;
        private UserManager<User> _user;
        private const string g_key = "Graph";

        public GraphController(IRepository db, UserManager<User> user)
        {
            _db = db;
            _user = user;
        }

        public IActionResult Index()
        {
            var user = _user.GetUserId(User);
            //return List of Pets essentially being a graph
            List<Pet> pets = _db.GetAllPetsByOwner(user);
            ViewBag.Pets = pets;

            return View();
        }

        public IActionResult StartGraph(int id)
        {
            //Get Pet
            Pet pet = _db.GetPetById(id);
            //Get All Pictures by pet
            List<Picture> pics = _db.GetAllPicsByPet(pet);

            //Create a graph using the factory
            var factory = new GraphFactory();
            Graph graph = factory.CreateGraph(pics);

            //save graph to session
            HttpContext.Session.SetObject(g_key, graph);

            //get random vertex and go to display page
            Picture pic = graph.GetRandomPic();

            return View("Display", pic);
        }

        public IActionResult Display(int id)
        {
            var model = _db.GetPicById(id);
            return View(model);
        }
    }
}
