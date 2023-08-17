using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaNacionProject.Shared.Models
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
