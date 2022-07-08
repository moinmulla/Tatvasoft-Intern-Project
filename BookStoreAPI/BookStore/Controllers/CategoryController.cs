using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/Category")]
    public class CategoryController : ControllerBase
    {
        CategoryRepository _categoryRepository=new CategoryRepository();

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>),(int)HttpStatusCode.OK)]
        public IActionResult GetCategories(string? keyword ,int pageIndex=1,int pageSize=10)
        {
            var categories=_categoryRepository.GetCategories(keyword,pageIndex, pageSize);
            ListResponse<CategoryModel> listResponse = new ListResponse<CategoryModel>()
            {
                Results = categories.Results.Select(c => new CategoryModel(c)),
                TotalRecords = categories.TotalRecords,
            };
            return Ok(listResponse);

        }

        [HttpGet]
        [Route("getCategory")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            CategoryModel categoryModel = new CategoryModel(category);
            return Ok(categoryModel);

        }

        [HttpPost]
        [Route("AddCategories")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCategory(CategoryModel model)
        {
            if (model == null)
            {
                return BadRequest("Model is null");
            }
            Category category = new Category()
            {
                Name = model.Name,
                Id = model.Id,
            };
            var categories = _categoryRepository.AddCategory(category);
            CategoryModel categoryModel=new CategoryModel(category);
            
            return Ok(categoryModel);

        }

        [HttpPut]
        [Route("updateCtegories")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            if (model == null)
            {
                return BadRequest("Model is null");
            }
            Category category = new Category()
            {
                Name = model.Name,
                Id = model.Id,
            };
            var categories = _categoryRepository.UpdateCategory(category);
            CategoryModel categoryModel = new CategoryModel(category);

            return Ok(categoryModel);

        }

        [HttpDelete]
        [Route("deleteCategories/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            if (id<=0)
            {
                return BadRequest("id is null");
            }
            var result = _categoryRepository.DeleteCategory(id);

            return Ok(result);

        }
    }
}
