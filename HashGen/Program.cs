using System;
using BCrypt.Net;

namespace HashGen
{
    class Program
    {
        static void Main(string[] args)
        {
            string p1 = "Admin@123";
            string p2 = "123456";
            Console.WriteLine($"Hash for '{p1}': {BCrypt.Net.BCrypt.HashPassword(p1, 11)}");
            Console.WriteLine($"Hash for '{p2}': {BCrypt.Net.BCrypt.HashPassword(p2, 11)}");
        }
    }
}
