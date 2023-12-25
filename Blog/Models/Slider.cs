using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Slider
    {
        [Required]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "The length should not be less than 4"), MaxLength(20, ErrorMessage = "The length should not be greater than 20")]
        public string MainText { get; set; }
        [Required, MinLength(4, ErrorMessage = "The length should not be less than 4"), MaxLength(20, ErrorMessage = "The length should not be greater than 15")]
        public string Text { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
