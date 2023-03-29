namespace RecipeFinderAPI.Models
{
    public class UpdateUserInfoDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
