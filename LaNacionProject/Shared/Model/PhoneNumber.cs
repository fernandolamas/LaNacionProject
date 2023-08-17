using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaNacionProject.Shared.Models
{
    public class PhoneNumber : BaseEntity
    {
        [StringLength(30)]
        public string Type { get; set; } = null!;
        [Phone]
        [StringLength(60)]
        public string Number { get; set; } = null!;
        public long ContactId { get; set; }
    }
}
