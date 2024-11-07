using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var equipments = _context.Equipments.ToList();
            ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq");
        }


        // GET: Adjustments
        public async Task<IActionResult> Index()
        {
            var adjustments = await _context.Adjustments
                .Include(a => a.Worker)
                .ToListAsync();

            // Логируем данные
            foreach (var adjustment in adjustments)
            {
                Console.WriteLine($"Adjustment ID: {adjustment.Id}, Worker: {adjustment.Worker?.WorkerName ?? "Неизвестно"}");
            }

            return View(adjustments);
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

        // POST: Adjustments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public async Task<IActionResult> Create()
        {
            // Загружаем оборудование
            var equipments = await _context.Equipments.ToListAsync(); // Загружаем оборудование ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq"); // Передаем список в ViewBag
            ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq"); // Передаем в ViewBag для использования в выпадающем списке
            // Загружаем наладчиков
            var workers = await _context.Workers.ToListAsync();
            ViewBag.Workers = new SelectList(workers, "WorkerId", "WorkerName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkerId,EquipmentId,WorkerName,Comments,Status")] Adjustment adjustment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    adjustment.Workshop = null; 
                    adjustment.NameEq = null;
                    adjustment.AcNumber = null; 
                    adjustment.Id = Guid.NewGuid();
                    _context.Add(adjustment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Выводим ошибку в консоль или сохраняем в ModelState
                    ModelState.AddModelError("", $"Ошибка при сохранении: {ex.Message}");
                }

            }
                var equipments = _context.Equipments.ToList(); // Получаем список оборудования из базы данных
                ViewBag.Equipments = new SelectList(_context.Equipments, "Id", "NameEq", adjustment.EquipmentId);
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
            var workers = await _context.Workers.ToListAsync();
            ViewBag.Workers = new SelectList(workers, "WorkerId", "WorkerName");
            var equipments = await _context.Equipments.ToListAsync(); // Загружаем оборудование ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq"); // Передаем список в ViewBag
            ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq"); // Передаем в ViewBag для использования в выпадающем списке
            return View();
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
