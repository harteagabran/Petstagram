using Microsoft.AspNetCore.Mvc;
using Petstagram.Extensions;
using Petstagram.Models;
using Petstagram.Repositories;
using Petstagram.Services;
using Petstagram.StructureFactory;

namespace Petstagram.Areas.Demo.Controllers
{
    public class DemoController : Controller
    {
        private StructureService _struc;
        private IRepository _db;
        private const string g_key = "DemoGraph";

        public DemoController(StructureService struc, IRepository db)
        {
            _struc = struc;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowPics()
        {
            List<Picture> pics = _db.GetAllPicsByOwner("Demo");
            pics = _struc.SelectionSortByDate(pics);
            ViewBag.Pics = pics;
            return View();
        }

        public IActionResult ShowPets()
        {
            List<Pet> pets = _db.GetAllPetsByOwner("Demo");
            ViewBag.Pets = pets;
            return View();
        }

        public IActionResult DetailPic(int id, string prevurl)
        {
            Picture pic = _db.GetPicById(id);
            ViewBag.Pic = pic;
            ViewBag.PrevUrl = !string.IsNullOrEmpty(prevurl) ? prevurl : Url.Action("Index");
            return View();
        }

        public IActionResult GraphSelect()
        {
            List<Pet> pets = _db.GetAllPetsByOwner("Demo");
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

            //get random vertex and go to display page
            Vertex currVertex = graph.GetRandomVertex();
            graph.SetVisited(currVertex);
            Picture pic = currVertex.Pic;
            int vid = currVertex.Id;
            List<Edge> edges = graph.GetEdgesOfVertex(currVertex);
            List<Vertex> connections = new List<Vertex>();

            //Loop through edges and add the vertex to the list
            foreach (Edge e in edges)
            {
                if (vid == e.Start)
                {
                    connections.Add(graph.GetVertex(e.End));
                }
                else
                {
                    connections.Add(graph.GetVertex(e.Start));
                }
            }
            //save graph to session
            ViewBag.Specs = graph.GetVisited();
            HttpContext.Session.SetObject(g_key, graph);

            ViewBag.Connections = connections;
            List<Pet> pets = _db.GetAllPetsFromPic(pic);
            ViewBag.Pets = string.Join(" ", pets.Select(p => p.Name));

            return View("Display", pic);
        }

        public IActionResult Display(int id)
        {
            Graph graph = HttpContext.Session.GetObject<Graph>(g_key);
            Vertex currVertex = graph.GetVertex(id);
            List<Edge> edges = graph.GetEdgesOfVertex(currVertex);
            List<Vertex> connections = new List<Vertex>();
            //if already visited, skip showing story
            graph.SetVisited(currVertex);

            //Loop through edges and add the vertex to the list
            foreach (Edge e in edges)
            {
                if (id == e.Start)
                {
                    connections.Add(graph.GetVertex(e.End));
                }
                else
                {
                    connections.Add(graph.GetVertex(e.Start));
                }
            }

            //save graph to session
            HttpContext.Session.SetObject(g_key, graph);

            var model = currVertex.Pic;
            ViewBag.Specs = graph.GetVisited();
            ViewBag.Connections = connections;
            return View(model);
        }
    }
}
