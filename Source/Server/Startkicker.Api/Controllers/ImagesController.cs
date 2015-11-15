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
        public async Task<IHttpActionResult> Add(NewImagesRequestModel request)
        {
            string imageUrl = await images.UploadAsync(request.ImageStream);
            this.images.Add(
                new Image
                {
                    ImageUrl = imageUrl,
                    ProjectId = request.ProjectId,
                    IsRemoved = false
                });

            return this.Ok(imageUrl);
        }
    }
}
