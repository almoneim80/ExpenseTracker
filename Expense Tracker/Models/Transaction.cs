using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The amount can not be less than 1.")]
        public int Amount { get; set; }

        
        [Column(TypeName = "nvarchar(1000)")]
        [StringLength(1000)]
        public string? Note { get; set; }

        [Required(ErrorMessage = "The date is required.")]
        public DateTime Date { get; set; } = DateTime.Now;

        [ForeignKey("CategoryId")]
        [Required(ErrorMessage = "The category is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The category can not be empty.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [NotMapped]
        public string CategoryTitleWithIcon 
        {
            get
            {
                return Category == null ? "" : Category.Icon + " " + Category.Title;
            }
        }

        [NotMapped]
        public string formattedAmount
        {
            get
            {
                return ((Category == null || Category.Type == "Expense") ? " - " : " + ") + Amount.ToString();
            }
        }
    }
}
