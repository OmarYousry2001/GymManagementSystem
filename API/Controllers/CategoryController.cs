using API.Base;
using BL.Contracts.Services.Custom;
using BL.DTO.Entities;
using Domains.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    public class CategoryController : AppControllerBase
    {
        #region Fields 
        private readonly ICategoryService _categoryService; 
        #endregion

        #region Constructor
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        } 
        #endregion

        #region Apis
        [HttpGet(Router.CategoryRouting.GetAll)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _categoryService.GetAllAsync();
            return NewResult(entities);
        }

        [HttpGet(Router.CategoryRouting.GetById)]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _categoryService.FindByIdAsync(id);
            return NewResult(entity);
        }

        [HttpPost(Router.CategoryRouting.Create)]
        public async Task<IActionResult> Create(CategoryDTO product)
        {
            return NewResult(await _categoryService.SaveAsync(product, GuidUserId));
        }

        [HttpPut(Router.CategoryRouting.Update)]
        public async Task<IActionResult> Update(CategoryDTO product)
        {
            return NewResult(await _categoryService.SaveAsync(product, GuidUserId));
        }

        [HttpDelete(Router.CategoryRouting.Delete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return NewResult(await _categoryService.DeleteAsync(id, GuidUserId));
        } 
        #endregion

    }
}
