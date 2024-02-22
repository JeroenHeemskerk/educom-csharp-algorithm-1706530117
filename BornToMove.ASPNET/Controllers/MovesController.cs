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

        //--------------------------------------------------------------------------------

        // GET: Moves/Details/1
        public IActionResult Details(int? id, string? returnPath)
        {
            Console.WriteLine($"return path = {returnPath}");
           
            if (id == null)
            {
                return NotFound();
            }

            var moveRating = _buMove.GiveMoveBasedOnId(id.Value);
            if (moveRating == null)
            {
                return NotFound();
            }

            ViewData["returnPath"] = returnPath ?? "Home";

            return View(moveRating);
        }


        [HttpPost("/Moves/Details/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Rate([Bind("Move,Rating,Vote")] MoveRating moveRating, int id)
        {

            Move move = _buMove.moveCrud.ReadMoveById(id);

            if(move == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _buMove.moveCrud.addRating(move, moveRating.Vote, moveRating.Rating);

            return RedirectToAction("Details", "Moves", new { id = id });

        }

        //--------------------------------------------------------------------------------

        public IActionResult Random()
        {
            var move= _buMove.GenerateRandomMove();

            if (move == null)
            {
                return NotFound();
            }
            return View("Details", move);
        }

        //--------------------------------------------------------------------------------

        public IActionResult Create()
        {
            return View();

        }

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

        //--------------------------------------------------------------------------------

        // GET: Moves/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var move = await _buMove.moveCrud.MoveContext.Move.FindAsync(id);
            if (move == null)
            {
                return NotFound();
            }
            return View(move);
        }

        // POST: Moves/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,SweatRate")] Move move)
        {
            if (id != move.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _buMove.moveCrud.MoveContext.Update(move);
                    await _buMove.moveCrud.MoveContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoveExists(move.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(move);
        }



        //--------------------------------------------------------------------------------


        // GET: Moves/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var move = await _buMove.moveCrud.MoveContext.Move
                .FirstOrDefaultAsync(m => m.Id == id);
            if (move == null)
            {
                return NotFound();
            }

            return View(move);
        }

        // POST: Moves/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var move = await _buMove.moveCrud.MoveContext.Move.FindAsync(id);
            if (move != null)
            {
                _buMove.moveCrud.MoveContext.Move.Remove(move);
            }

            await _buMove.moveCrud.MoveContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoveExists(int id)
        {
            return _buMove.moveCrud.MoveContext.Move.Any(e => e.Id == id);
        }

    }
}
