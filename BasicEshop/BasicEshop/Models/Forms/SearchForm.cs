using System.ComponentModel.DataAnnotations;

namespace BasicEshop.Models.Forms
{
    public class SearchForm
    {
        [Display(Name = "Search")] [Required] public string Search { get; set; }
    }
}