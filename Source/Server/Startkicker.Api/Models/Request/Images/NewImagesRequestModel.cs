namespace Startkicker.Api.Models.Request.Images
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    public class NewImagesRequestModel
    {
        [Required]
        public FileStream ImageStream { get; set; }

        public int ProjectId { get; set; }
    }
}
