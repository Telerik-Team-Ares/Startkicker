namespace Startkicker.Data.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public int ProjectId { get; set; }

        public bool IsRemoved { get; set; }

        public virtual Project Project { get; set; }
    }
}