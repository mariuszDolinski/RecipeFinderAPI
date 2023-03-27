namespace RecipeFinderAPI.Models
{
    public class RegisterUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; } = 3;
    }
}
