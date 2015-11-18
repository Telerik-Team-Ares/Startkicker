namespace Startkicker.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Startkicker.Api.Models.Request.Images;
    using Startkicker.Data.Models;
    using Startkicker.Services.Data.Contracts;

    public class ImagesController : ApiController
    {
        private readonly IImagesService images;

        public ImagesController(IImagesService images)
        {
            this.images = images;
        }

        [HttpPost]
        //[Authorize]
        public async Task<IHttpActionResult> Add(NewImagesRequestModel model)
        {
            string imageUrl = await images.UploadAsync(model.ByteArrayContent, model.FileExtension);
            this.images.Add(
                new Image
                {
                    ImageUrl = imageUrl,
                    //ProjectId = model.ProjectId,
                    IsRemoved = false
                });

            return this.Ok(imageUrl);
        }

        [HttpDelete]
        //[Authorize]
        public async Task<IHttpActionResult> Remove(DeleteImageRequestModel model)
        {
            var image = this.images.GetByUrl(model.ImageUrl);
            if (image != null)
            {
                this.images.Remove(image);
                return this.Ok();
            } else
            {
                return this.NotFound();
            }
        }
    }
}
