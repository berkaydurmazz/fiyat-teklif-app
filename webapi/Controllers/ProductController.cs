using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Base.Base;
using webapi.Base.Base.Grid;
using webapi.Entity;
using webapi.Helper.Base;
using webapi.ViewModel.Product;
using webapi.ViewModel.General.Grid;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class ProductController : BaseWebApiController
    {
        public Product[] myProduct;
        private static readonly string[] Names = new[]
        {
            "Product1","Product2","Product3","Product4","Product5","Product6","Product7","Product8","Product9"
        };

        public ProductController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }


        [HttpGet(Name = "ProductController")]
        public IEnumerable<Product> Get()
        {
            var a = Enumerable.Range(1, 10).Select(index => new Product
            {
                Name = Names[Random.Shared.Next(Names.Length)],
                Id = index,
                Descp = "Test productDescp",
                Size = "123132",
                Price="100",
                Suppliers = "Test Supplier",
                KdvRate= "20",
                Category="Test Category",
            }).ToArray();

            return a;
        }
        [HttpGet("Delete")]
        public ApiResult Delete(int id)
        {
            var data = _unitOfWork.Repository<Product>().GetById(id);
            if (_unitOfWork.Repository<Product>().Any(i => i.RolId == id))
            {
                return new ApiResult { Result = false, Message = "Rol kullanıcı tarafından kullanılmaktadır." };
            }

            if (data == null)
            {
                return new ApiResult { Result = false, Message = "Belirtilen müşteri bulunamadı." };
            }

            _unitOfWork.Repository<Product>().SoftDelete(data.Id);
            _unitOfWork.SaveChanges();
            return new ApiResult { Result = true };
        }

        [HttpPost("CreateOrUpdateProduct")]
        [AllowAnonymous]
        public ApiResult CreateOrUpdateProduct([FromBody] ProductCreateVM dataVM)
        {
            if (!ModelState.IsValid)
                return new ApiResult { Result = false, Message = "Form'da doldurulmayan alanlar mevcut,lütfen doldurun." };
            Product data = null;
            if (dataVM.Id > 0)
                data = _unitOfWork.Repository<Product>().GetById(dataVM.Id);
            else
                data = new Product()
                {
                    Name = dataVM.productName,
                    Descp = dataVM.productDescp,
                    Size = dataVM.productSize,
                    Price = dataVM.price,
                    Suppliers = dataVM.suppliers,
                    KdvRate = dataVM.kdvRate,
                    Category = dataVM.category,
                };


            _unitOfWork.Repository<Product>().InsertOrUpdate(data);
            _unitOfWork.SaveChanges();
            return new ApiResult { Result = true };
        }

        [HttpPost("GetProductGrid")]
        [AllowAnonymous]
        public ApiResult<GridResultModel<ProductGridVM>> GetProductGrid()
        {

            var query = _unitOfWork.Repository<Product>()
            .Select(x => new ProductGridVM
            {
                Name = x.productName,
                Descp = x.productDescp,
                Size = x.productSize,
                Price = x.price,
                Suppliers = x.suppliers,
                KdvRate = x.kdvRate,
                Category = x.category,
            });

            var rest = query.ToDataListRequest(Request.ToRequestFilter());

            return new ApiResult<GridResultModel<ProductGridVM>> { Data = rest, Result = true };
        }

    }
}

