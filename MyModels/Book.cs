using System.ComponentModel.DataAnnotations;

namespace MyModels
{
    public class Book
    {
        public int Id { get; set; }
        [Required( ErrorMessage ="{0} is required")]
        [StringLength(50, ErrorMessage ="{0} must be less then {1}")]
        public string Title { get; set; } = null!;
        [Range(10,500, ErrorMessage ="{0} must between {1} - {2}")]
        public int Price { get; set; }
        public DateTime PublishDate { get; set; }
        public bool InStock { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string? Description { get; set; }
    }
}
