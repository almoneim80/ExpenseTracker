using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The title is required.")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100)]
        public string Icon { get; set; } = "";

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, MinimumLength = 3)]
        public string Type { get; set; } = "Expense";

        [NotMapped]
        public string TitleWithIcon 
        {
            get
            {
                return this.Icon + " " + this.Title; 
            }
        }
    }
}
