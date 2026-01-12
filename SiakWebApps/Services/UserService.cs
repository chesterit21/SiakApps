using SiakWebApps.DataAccess;
using SiakWebApps.Models;
using BCrypt.Net;
using Microsoft.Extensions.Logging;

namespace SiakWebApps.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(UserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
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
            const string initialUsername = "admin_akademik";
            const string initialPassword = "password123";

            var existingUser = await _userRepository.GetUserByUsernameAsync(initialUsername);
            if (existingUser != null) return;

            // Ensure bcrypt hash is properly formatted (60 chars)
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(initialPassword);
            if (hashedPassword.Length != 60)
            {
                throw new InvalidOperationException("Invalid bcrypt hash length. Expected 60 characters.");
            }

            var user = new User
            {
                Username = initialUsername,
                Password = hashedPassword,
                Email = "admin@sekolah.edu",
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                await _userRepository.CreateUserAsync(user);
            }
            catch (Exception ex) when (ex.Message.Contains("duplicate") || ex.Message.Contains("unique"))
            {
                // Abaikan error duplikat karena user sudah ada
                _logger.LogWarning("User {Username} sudah ada di database.", initialUsername);
            }
        }
    }
}