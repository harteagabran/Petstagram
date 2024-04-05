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
            foreach(Edge e in edges)
            {
                if(id == e.Start)
                {
                    connections.Add(graph.GetVertex(e.End));
                } else
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
