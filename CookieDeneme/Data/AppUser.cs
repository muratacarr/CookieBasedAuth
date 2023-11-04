﻿namespace CustomCookieBased.Data
{
    public class AppUser
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public List<AppUserRole>? UserRoles { get; set; }
    }
}
