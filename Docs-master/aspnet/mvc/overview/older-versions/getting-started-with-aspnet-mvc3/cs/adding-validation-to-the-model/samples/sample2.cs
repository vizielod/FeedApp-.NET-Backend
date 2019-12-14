public class Movie
{
    public int ID { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Date is required")]
    public DateTime ReleaseDate { get; set; }

    [Required(ErrorMessage = "Genre must be specified")]
    public string Genre { get; set; }

    [Range(1, 100, ErrorMessage = "Price must be between $1 and $100")]
    public decimal Price { get; set; }

    [StringLength(5)]
    public string Rating { get; set; }
}