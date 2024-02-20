using BornToMove.ASPNET.Models;
using BornToMove.Business;
using BornToMove.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var moveRating = _buMove.GiveMoveBasedOnId(id);
            if (moveRating == null)
            {
                return NotFound();
            }

            ViewData["returnPath"] = returnPath ?? "Home";

            return View(moveRating);
        }


        [HttpPost("/Moves/Details/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rate([Bind("Move,Rating,Vote")] MoveRating moveRating, int id)
        {

            Move move = _buMove.moveCrud.ReadMoveById(id);

            if(move == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _buMove.moveCrud.addRating(move, moveRating.Vote, moveRating.Rating);

            return RedirectToAction("Details", "Moves", new { id = id });

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

        public IActionResult Create()
        {
            return View();

        }

        //This action receives form data submitted to create a new move,
        //processes it, and saves the new move to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,SweatRate")] Move move)
        {
            if (ModelState.IsValid)
            {
                _buMove.moveCrud.MoveContext.Add(move);
             
                await _buMove.moveCrud.MoveContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(move);
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
