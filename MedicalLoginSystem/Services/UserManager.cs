using MedicalLoginSystemConsole.Models;

namespace MedicalLoginSystemConsole.Services
{
    public static class UserManager
    {
        private static string filePath = "users.txt";

        public static List<User> LoadUsers()
        {
            if (!File.Exists(filePath)) return new();
            return File.ReadAllLines(filePath)
                       .Select(User.FromString)
                       .Where(u => u != null)
                       .ToList();
        }

        public static void SaveUser(User user)
        {
            File.AppendAllText(filePath, user + Environment.NewLine);
        }

        public static bool EmailExists(string email)
        {
            return LoadUsers().Any(u => u.Email == email);
        }

        public static User ValidateLogin(string email, string password)
        {
            return LoadUsers().FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}