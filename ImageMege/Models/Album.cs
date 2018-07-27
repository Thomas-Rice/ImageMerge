namespace ImageMege.Models
{
    public class Album
    {
        public int UserId { get; set; }
        public int AlbumId { get; set; }
        public int PhotoId { get; set; }
        public string PhotoTitle { get; set; }
        public string ThumbnailUrl { get; set; }
        public string AlbumTitle { get; set; }
        public string FullImageUrl { get; set; }
    }
}