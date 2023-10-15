using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Base.Base;
using webapi.Base.Base.Grid;
using webapi.Entity;
using webapi.Helper.Base;
using webapi.ViewModel.Category;
using webapi.ViewModel.General.Grid;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : BaseWebApiController
    {

        public CategoryController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        [HttpGet("Delete")]
        public ApiResult Delete(int id)
        {
            var data = _unitOfWork.Repository<Category>().GetById(id);
            if (_unitOfWork.Repository<Category>().Any(i => i.RolId == id))
            {
                return new ApiResult { Result = false, Message = "Rol Category tarafından kullanılmaktadır." };
            }

            if (data == null)
            {
                return new ApiResult { Result = false, Message = "Belirtilen Category bulunamadı." };
            }

            _unitOfWork.Repository<Category>().SoftDelete(data.Id);
            _unitOfWork.SaveChanges();
            return new ApiResult { Result = true };
        }

        [HttpPost("CreateOrUpdateCategory")]
        [AllowAnonymous]
        public ApiResult CreateOrUpdateCategory([FromBody] CategoryCreateVM dataVM)
        {
            if (!ModelState.IsValid)
                return new ApiResult { Result = false, Message = "Form'da doldurulmayan alanlar mevcut,lütfen doldurun." };
            Product data = null;
            if (dataVM.Id > 0)
                data = _unitOfWork.Repository<Product>().GetById(dataVM.Id);
            else
                data = new Product()
                {
                    catName = dataVM.catName,
                };


            _unitOfWork.Repository<Category>().InsertOrUpdate(data);
            _unitOfWork.SaveChanges();
            return new ApiResult { Result = true };
        }

        [HttpPost("GetCategoryGrid")]
        [AllowAnonymous]
        public ApiResult<GridResultModel<CategoryGridVM>> GetCategoryGrid()
        {

            var query = _unitOfWork.Repository<Category>()
            .Select(x => new CategoryGridVM
            {
                catName = x.catName,
            });

            var rest = query.ToDataListRequest(Request.ToRequestFilter());

            return new ApiResult<GridResultModel<CategoryGridVM>> { Data = rest, Result = true };
        }

    }
}

