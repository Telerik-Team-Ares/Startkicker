﻿namespace Startkicker.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Startkicker.Api.Infrastructure.ActionFilters;
    using Startkicker.Api.Models.Request.Categories;
    using Startkicker.Api.Models.Response.Categories;
    using Startkicker.Data.Models;
    using Startkicker.Services.Data.Contracts;
    using System.Linq;

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
            var result = this.categories
                .GetById(id)
                .Select(CategoryDescriptionResponseModel.FromModel)
                .FirstOrDefault();

            if (result == null)
            {
                return this.BadRequest("Category was not found!");
            }

            return this.Ok(result);
        }

        [HttpPost]
        [ValidateModelState]
        [CheckModelForNull]
        [Authorize]
        public IHttpActionResult Add([FromBody]NewCategoryRequestModel categoryModel)
        {
            var addedCategoryId = this.categories.Add(categoryModel.Name);

            if (addedCategoryId == -1)
            {
                return this.BadRequest("Category already exists.");
            }

            return this.Ok(addedCategoryId);
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
                        Id = category.Id
                    };

                    result.Add(categoryMapped);
                }

                return this.Ok(result);
            }

            return this.BadRequest("Categories were not found!");
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult Remove(int id)
        {
            this.categories.Remove(id);

            return this.Ok("Category has been removed!");
        }
    }
}