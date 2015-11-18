namespace Startkicker.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Image
    {
        public int Id { get; set; }
                
        [Index(IsUnique = true)]
        public string ImageUrl { get; set; }

        public int ProjectId { get; set; }

        public bool IsRemoved { get; set; }

        public virtual Project Project { get; set; }
    }
}