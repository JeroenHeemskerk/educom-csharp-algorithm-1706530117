using BornToMove.ASPNET.Models;
using BornToMove.Business;
using BornToMove.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BornToMove.ASPNET.Controllers
{
    public class MovesController : Controller
    {
        private BuMove _buMove;
        public MovesController(BuMove buMove)
        {
            _buMove = buMove;
        }

        // GET: Moves
        public IActionResult Index()
        {
            List<MoveRating> allMoves = _buMove.GetAllMoves();
            return View(allMoves);
        }

        // GET: Moves/Details/1
        public IActionResult Details(int id, string? returnPath)
        {
            System.Console.WriteLine($"return path = {returnPath}");
            if (id == null)
            {
                return NotFound();
            }

            var move = _buMove.GiveMoveBasedOnId(id);
            if (move == null)
            {
                return NotFound();
            }

            ViewData["returnPath"] = returnPath ?? "Home";

            return View(move);
        }
        public IActionResult Random()
        {
            var move= _buMove.GenerateRandomMove();

            if (move == null)
            {
                return NotFound();
            }
            return View("Details", move);
        }










        //This action renders a form to create a new move
        public IActionResult Create()
        {
            return View();

        }

        //This action receives form data submitted to create a new move,
        //processes it, and saves the new move to the database.
        [HttpPost]

        public IActionResult Create(MoveViewModel model)
        {
            return View();

        }

        public IActionResult Edit()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Edit(MoveViewModel model)
        {
            return View();
        }


        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(MoveViewModel model)
        {
            return View();
        }

    }
}
