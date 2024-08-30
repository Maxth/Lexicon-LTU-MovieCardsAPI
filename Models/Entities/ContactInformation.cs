using System.ComponentModel.DataAnnotations;

namespace MovieCardsApi.Entities
{
#nullable disable
    public class ContactInformation
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
    }
}
