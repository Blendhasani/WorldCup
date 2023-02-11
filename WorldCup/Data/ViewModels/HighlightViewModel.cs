namespace WorldCup.Data.ViewModels
{
    public class HighlightViewModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImgUrl { get; set; }
        public string VideoUrl { get; set; }
        public int Count { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
