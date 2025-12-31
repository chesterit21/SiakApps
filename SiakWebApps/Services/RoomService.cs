using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class RoomService
    {
        private readonly RoomRepository _roomRepository;

        public RoomService(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Room room)
        {
            return await _roomRepository.CreateAsync(room);
        }

        public async Task<bool> UpdateAsync(Room room)
        {
            return await _roomRepository.UpdateAsync(room);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _roomRepository.DeleteAsync(id);
        }
    }
}