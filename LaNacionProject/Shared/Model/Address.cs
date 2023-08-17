using System.ComponentModel.DataAnnotations;

namespace LaNacionProject.Shared.Models
{
    public class Address : BaseEntity
    {
        [StringLength(100)]
        public string State
        {
            get => _state;
            set => _state = value.ToLower();
        }
        [StringLength(100)]
        public string City
        {
            get => _city; 
            set => _city = value.ToLower();
        }
        private string _state = null!;
        private string _city = null!;
        public long ContactId { get; set; }
    }
}