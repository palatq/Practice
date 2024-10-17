using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdjustmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdjustmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adjustments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Adjustments.ToListAsync());
        }

        // GET: Adjustments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adjustment = await _context.Adjustments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adjustment == null)
            {
                return NotFound();
            }

            return View(adjustment);
        }

        // GET: Adjustments/Create
        // public IActionResult Create()
        // {
        //     return View();
        // }

        // POST: Adjustments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Create()
        {
            var equipments = await _context.Equipments.ToListAsync(); // Загружаем оборудование ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq"); // Передаем список в ViewBag
            return View();
        }

        //// POST: Adjustments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Adjustment adjustment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adjustment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Повторно передаем список оборудования в случае ошибки
            var equipments = await _context.Equipments.ToListAsync(); // Исправлено: Equipment на Equipments ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq");
            return View(adjustment);
        }



        // GET: Adjustments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adjustment = await _context.Adjustments.FindAsync(id);
            if (adjustment == null)
            {
                return NotFound();
            }
            return View(adjustment);
        }

        // POST: Adjustments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,EquipmentId,NameEq,Workshop,AcNumber,Status,TechnicianName,Comments")] Adjustment adjustment)
        {
            if (id != adjustment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adjustment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdjustmentExists(adjustment.Id))
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
            return View(adjustment);
        }

        // GET: Adjustments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adjustment = await _context.Adjustments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adjustment == null)
            {
                return NotFound();
            }

            return View(adjustment);
        }

        // POST: Adjustments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var adjustment = await _context.Adjustments.FindAsync(id);
            if (adjustment != null)
            {
                _context.Adjustments.Remove(adjustment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdjustmentExists(Guid id)
        {
            return _context.Adjustments.Any(e => e.Id == id);
        }

    }
}
