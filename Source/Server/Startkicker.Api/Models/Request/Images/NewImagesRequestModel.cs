namespace Startkicker.Api.Models.Request.Images
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class NewImagesRequestModel
    {
        [Required]
        public string OriginalFileName { get; set; }

        [Required]
        public string FileExtension { get; set; }

        [Required]
        public string Base64Content { get; set; }

        public byte[] ByteArrayContent
        {
            get
            {
                return Convert.FromBase64String(this.Base64Content);
            }
        }
    }
}
