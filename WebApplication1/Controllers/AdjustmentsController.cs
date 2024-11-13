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
        .Include(a => a.Equipment) // Загрузка оборудования
        .Include(a => a.Worker) // Загрузка работника
        .ToListAsync();
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
        public async Task<IActionResult> Create([Bind("EquipmentId,WorkerId,Comments,Status")] Adjustment adjustment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    adjustment.Id = Guid.NewGuid(); // Генерация нового идентификатора
                    Console.WriteLine($"WorkerId: {adjustment.WorkerId}");
                    _context.Add(adjustment); // Добавление в контекст
                    await _context.SaveChangesAsync(); // Сохранение в базе данных
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ошибка при сохранении: {ex.Message}");
                }
            }

            // Если модель не валидна, загружаем данные для ViewBag
            var workers = await _context.Workers.ToListAsync();
            ViewBag.Workers = new SelectList(workers, "WorkerId", "WorkerName", adjustment.WorkerId);
            var equipments = await _context.Equipments.ToListAsync();
            ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq", adjustment.EquipmentId);
            return View(adjustment);
        }





        // GET: Adjustments/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var adjustment = await _context.Adjustments.FindAsync(id);
        //    if (adjustment == null)
        //    {
        //        return NotFound();
        //    }
        //    var workers = await _context.Workers.ToListAsync();
        //    ViewBag.Workers = new SelectList(workers, "WorkerId", "WorkerName");
        //    var equipments = await _context.Equipments.ToListAsync(); // Загружаем оборудование ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq"); // Передаем список в ViewBag
        //    ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq"); // Передаем в ViewBag для использования в выпадающем списке
        //    return View();
        //}

        // POST: Adjustments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,EquipmentId,NameEq,Workshop,AcNumber,Status,,Comments,WorkerName")] Adjustment adjustment)
        //{
        //    if (id != adjustment.Id)
        //    {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(adjustment);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (Exception Ex)
        //        {
        //            if (!AdjustmentExists(adjustment.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(adjustment);
        //}
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

            // Загружаем оборудование и работников для выпадающих списков
            var workers = await _context.Workers.ToListAsync();
            ViewBag.Workers = new SelectList(workers, "WorkerId", "WorkerName");
            var equipments = await _context.Equipments.ToListAsync();
            ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq", adjustment.EquipmentId); // Передаем в ViewBag для использования в выпадающем списке

            return View(adjustment);
        }

        // POST: Adjustments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,EquipmentId,WorkerId,Comments,Status,Workshop,AcNumber")] Adjustment adjustment)
        {
            if (id != adjustment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Проверка существования EquipmentId
                    var equipmentExists = await _context.Equipments.AnyAsync(e => e.Id == adjustment.EquipmentId);
                    if (!equipmentExists)
                    {
                        ModelState.AddModelError("EquipmentId", "Выбранное оборудование не существует.");
                        LoadViewBagData(adjustment);
                        return View(adjustment);
                    }

                    var existingAdjustment = await _context.Adjustments.FindAsync(id);
                    if (existingAdjustment == null)
                    {
                        return NotFound();
                    }

                    // Обновление полей
                    existingAdjustment.EquipmentId = adjustment.EquipmentId;
                    existingAdjustment.WorkerId = adjustment.WorkerId;
                    existingAdjustment.Comments = adjustment.Comments;
                    existingAdjustment.Status = adjustment.Status;
                    existingAdjustment.Workshop = adjustment.Workshop;
                    existingAdjustment.AcNumber = adjustment.AcNumber;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    ModelState.AddModelError("", $"Ошибка при сохранении: {dbEx.Message}");
                    if (dbEx.InnerException != null)
                    {
                        ModelState.AddModelError("", $"Внутренняя ошибка: {dbEx.InnerException.Message}");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ошибка при сохранении: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        ModelState.AddModelError("", $"Внутренняя ошибка: {ex.InnerException.Message}");
                    }
                }
            }

            LoadViewBagData(adjustment);
            return View(adjustment);
        }

        private async Task LoadViewBagData(Adjustment adjustment)
        {
            var workers = await _context.Workers.ToListAsync();
            ViewBag.Workers = new SelectList(workers, "WorkerId", "WorkerName", adjustment.WorkerId);
            var equipments = await _context.Equipments.ToListAsync();
            ViewBag.Equipments = new SelectList(equipments, "Id", "NameEq", adjustment.EquipmentId);
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
