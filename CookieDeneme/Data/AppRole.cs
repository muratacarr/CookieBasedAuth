namespace CustomCookieBased.Data
{
    public class AppRole
    {
        public int Id { get; set; }
        public string? Definition { get; set; }

        public List<AppUserRole>? UserRoles { get; set; }

    }
}
