using System.ComponentModel.DataAnnotations;

namespace SpendSmart.Models
{
    public class Expense
    {
        public int Id { get; set; } //primary key

        public decimal Value { get; set; } //price

        [Required]
        public string? Description { get; set; } //Adding the '?' makes it nullable 
    }
}
