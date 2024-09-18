using System.ComponentModel.DataAnnotations;

namespace ExploreAPIs.API.Modals.DTOs
{
    public class LoginRequestDTOs
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
