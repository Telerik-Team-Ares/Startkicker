namespace Startkicker.Api.Models.Response.Images
{
    using System.ComponentModel.DataAnnotations;

    public class NewImageResponseModel
    {
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string ImageId { get; set; }
    }
}
