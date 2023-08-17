using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaNacionProject.Shared.Models
{
    public class Contact : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Company { get; set; } = null!;
        [Url]
        [StringLength(300)]
        public string? ProfileImage { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(60)]
        public string Email {
            get => _email;
            set => _email = value.ToLower();
        }

        private string _email = null!;
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [Required]
        public List<PhoneNumber> PhoneNumber { get; set; } = null!;
        [Required]
        public Address Address { get; set; } = null!;
    }
}
