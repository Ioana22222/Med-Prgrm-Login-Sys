namespace MedicalLoginSystemConsole.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }

        public User(string email, string name, string password, string role, string phone)
        {
            Email = email;
            Name = name;
            Password = password;
            Role = role;
            Phone = phone;
        }

        public override string ToString() => $"{Email}|{Name}|{Password}|{Role}|{Phone}";

        public static User FromString(string line)
        {
            var parts = line.Split('|');
            if (parts.Length != 5)
                throw new FormatException("Invalid user data format.");

            return new User (parts[0], parts[1], parts[2], parts[3], parts[4]);
        }
    }
}