using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Models;
using SiakWebApps.Services;

namespace SiakWebApps.Controllers
{
    public class RoomsController : Controller
    {
        private readonly RoomService _roomService;

        public RoomsController(RoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAllAsync();
            return View(rooms);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var room = await _roomService.GetByIdAsync(id.Value);
            if (room == null) return NotFound();
            return PartialView("Details", room);
        }

        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nama,Kapasitas,Lokasi,TipeRuangan")] Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.CreateAsync(room);
                return Json(new { success = true });
            }
            return PartialView("_CreateModal", room);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var room = await _roomService.GetByIdAsync(id.Value);
            if (room == null) return NotFound();
            return PartialView("_EditModal", room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nama,Kapasitas,Lokasi,TipeRuangan,CreatedAt")] Room room)
        {
            if (id != room.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _roomService.UpdateAsync(room);
                return Json(new { success = true });
            }
            return PartialView("_EditModal", room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
