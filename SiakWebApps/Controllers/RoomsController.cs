using Microsoft.AspNetCore.Mvc;
using SiakWebApps.Filters;
using SiakWebApps.Models;
using SiakWebApps.Services;
using System.Threading.Tasks;

namespace SiakWebApps.Controllers
{
    [MenuAuthorize("MSTR-ROOM")]

    public class RoomsController : BaseController
    {
        private readonly RoomService _roomService;

        public RoomsController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomService.GetAllAsync();
            return View(rooms);
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var room = await _roomService.GetByIdAsync(id.Value);
            if (room == null) return NotFound();
            return PartialView("Details", room);
        }

        [MenuActionAuthorize("VIEW")]
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("ADD")]
        public async Task<IActionResult> Create([Bind("Nama,Kapasitas,Lokasi,TipeRuangan")] Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.CreateAsync(room);
                return Json(new { success = true });
            }
            return PartialView("_CreateModal", room);
        }

        [MenuActionAuthorize("VIEW")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var room = await _roomService.GetByIdAsync(id.Value);
            if (room == null) return NotFound();
            return PartialView("_EditModal", room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MenuActionAuthorize("EDIT")]
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
        [MenuActionAuthorize("DELETE")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}
