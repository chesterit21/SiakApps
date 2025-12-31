using SiakWebApps.DataAccess;
using SiakWebApps.Models;
using BCrypt.Net;

namespace SiakWebApps.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<int> CreateUserAsync(User user)
        {
            // Hash password before saving
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            // Hash password if it's being updated
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        // Membuat user awal jika belum ada user dalam sistem
        public async Task CreateInitialUserAsync()
        {
            var users = await GetAllUsersAsync();
            if (!users.Any())
            {
                var adminUser = new User
                {
                    Username = "admin",
                    Email = "admin@example.com",
                    Password = "admin123", // Ini akan di-hash saat disimpan
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await CreateUserAsync(adminUser);
            }
        }
    }
}