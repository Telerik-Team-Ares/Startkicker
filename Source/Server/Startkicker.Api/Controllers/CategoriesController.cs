namespace Startkicker.Api.Controllers
{
    using System.Web.Http;

    using Startkicker.Api.Infrastructure.ActionFilters;
    using Startkicker.Api.Models.Request.Categories;
    using Startkicker.Api.Models.Response.Categories;
    using Startkicker.Data.Models;
    using Startkicker.Services.Data.Contracts;
    using System.Collections.Generic;

    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        [HttpGet]
        //[Authorize]
        public IHttpActionResult GetById(int id)
        {
            var categoryDataModel = this.categories.GetById(id);
            if (categoryDataModel != null)
            {
                var result = new CategoryDescriptionResponseModel
                {
                    Name = categoryDataModel.Name,
                    Projects = categoryDataModel.Projects,
                    Id = categoryDataModel.Id
                };
                return this.Ok(result);
            }

            return this.BadRequest("Category was not found!");
        }

        [HttpGet]
        //[Authorize]
        public IHttpActionResult GetAll()
        {
            var categoryDataModel = this.categories.GetAll();
            var result = new List<CategoryDescriptionResponseModel>();

            if (categoryDataModel != null)
            {
                foreach (var category in categoryDataModel)
                {
                    var categoryMapped = new CategoryDescriptionResponseModel
                    {
                        Name = category.Name,
                        Projects = category.Projects,
                        Id = category.Id
                    };

                    result.Add(categoryMapped);
                }

                return this.Ok(result);
            }

            return this.BadRequest("Categories were not found!");
        }

        [HttpPost]
        [ValidateModelState]
        [CheckModelForNull]
        //[Authorize]
        public IHttpActionResult Add([FromBody]NewCategoryRequestModel categoryModel)
        {
            var addedCategoryId = this.categories.Add(
                new Category
                {
                    Name = categoryModel.Name,
                    Projects = categoryModel.Projects
                });

            if (addedCategoryId == -1)
            {
                return this.BadRequest("Category already exists.");
            }

            return this.Ok("Id of the added category is: " + addedCategoryId);
        }
    }
}
