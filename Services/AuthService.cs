using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Bookliz_Backend.Data;
using Bookliz_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Bookliz_Backend.Services
{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(UserLoginModel userLogin)
        {
            if (userLogin == null)
                return "user";
            if (userLogin.Role == null || userLogin.Role == "" || userLogin.Role != "Student" && userLogin.Role != "Librarian" && userLogin.Role != "Teacher")
                return "role";

            User? user = null;
            if (userLogin.Role == "Student")
                user = await _context.Students.FirstOrDefaultAsync(s => s.RegNumber == userLogin.Username) ?? throw new InvalidOperationException("User not found.");
            else if (userLogin.Role == "Librarian")
                user = await _context.Librarians.FirstOrDefaultAsync(s => s.EmpNumber == userLogin.Username) ?? throw new InvalidOperationException("User not found.");
            else if (userLogin.Role == "Teacher")
                user = await _context.Teachers.FirstOrDefaultAsync(s => s.EmpNumber == userLogin.Username) ?? throw new InvalidOperationException("User not found.");

            if (user == null)
                return "username";
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password!, userLogin.Password!)
                == PasswordVerificationResult.Failed)
                return "password";

            return GenerateJwtToken(user);
        }

        public async Task<bool> LogoutAsync()
        {
            // No server-side logic needed for stateless JWT logout
            return await Task.FromResult(true);
        }

        public async Task<User> RegisterAsync(UserRegistrationModel RegUser)
        {
            if (RegUser == null)
                return null!;

            if (RegUser.Role == "Student")
            {
                var student = new Student
                {
                    RegNumber = RegUser.Username,
                    FirstName = RegUser.FirstName,
                    LastName = RegUser.LastName,
                    Email = RegUser.Email,
                    Department = RegUser.Department,
                    PhoneNumber = RegUser.PhoneNumber,
                    Address = RegUser.Address,
                    Role = RegUser.Role
                };
                student.Password = new PasswordHasher<Student>().HashPassword(student, RegUser.Password!);

                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return student;
            }

            if (RegUser.Role == "Librarian")
            {
                Console.WriteLine("Librarian");
                var librarian = new Librarian
                {
                    EmpNumber = RegUser.Username,
                    FirstName = RegUser.FirstName,
                    LastName = RegUser.LastName,
                    Email = RegUser.Email,
                    PhoneNumber = RegUser.PhoneNumber,
                    Address = RegUser.Address,
                    Role = RegUser.Role
                };

                librarian.Password = new PasswordHasher<Librarian>().HashPassword(librarian, RegUser.Password!);

                await _context.Librarians.AddAsync(librarian);
                await _context.SaveChangesAsync();
                return librarian;
            }

            if (RegUser.Role == "Teacher")
            {
                var teacher = new Teacher
                {
                    EmpNumber = RegUser.Username,
                    FirstName = RegUser.FirstName,
                    LastName = RegUser.LastName,
                    Email = RegUser.Email,
                    PhoneNumber = RegUser.PhoneNumber,
                    Address = RegUser.Address,
                    Designation = RegUser.Desgnatation,
                    Department = RegUser.Department,
                    Role = RegUser.Role
                };
                teacher.Password = new PasswordHasher<Teacher>().HashPassword(teacher, RegUser.Password!);

                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync();
                return teacher;
            }

            Console.WriteLine("userdto is null");
            return null!;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            if (user is Student student)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, student.RegNumber!));
                claims.Add(new Claim(ClaimTypes.Role, "Student"));
            }
            else if (user is Librarian librarian)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, librarian.EmpNumber!));
                claims.Add(new Claim(ClaimTypes.Role, "Librarian"));
            }

            else if (user is Teacher teacher)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, teacher.EmpNumber!));
                claims.Add(new Claim(ClaimTypes.Role, "Teacher"));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}