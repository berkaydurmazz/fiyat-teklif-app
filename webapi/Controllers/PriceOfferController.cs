using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Base.Base;
using webapi.Base.Base.Grid;
using webapi.Entity;
using webapi.Helper.Base;
using webapi.ViewModel.PriceOffer;
using webapi.ViewModel.General.Grid;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PriceOfferController : BaseWebApiController
    {

        public PriceOfferController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        [HttpGet("Delete")]
        public ApiResult Delete(int id)
        {
            var data = _unitOfWork.Repository<PriceOffer>().GetById(id);
            if (_unitOfWork.Repository<PriceOffer>().Any(i => i.RolId == id))
            {
                return new ApiResult { Result = false, Message = "Rol PriceOffer tarafından kullanılmaktadır." };
            }

            if (data == null)
            {
                return new ApiResult { Result = false, Message = "Belirtilen PriceOffer bulunamadı." };
            }

            _unitOfWork.Repository<PriceOffer>().SoftDelete(data.Id);
            _unitOfWork.SaveChanges();
            return new ApiResult { Result = true };
        }

        [HttpPost("CreateOrUpdatePriceOffer")]
        [AllowAnonymous]
        public ApiResult CreateOrUpdatePriceOffer([FromBody] PriceOfferCreateVM dataVM)
        {
            if (!ModelState.IsValid)
                return new ApiResult { Result = false, Message = "Form'da doldurulmayan alanlar mevcut,lütfen doldurun." };
            Product data = null;
            if (dataVM.OfferId > 0)
                data = _unitOfWork.Repository<Product>().GetById(dataVM.OfferId;
            else
                data = new Product()
                {
                    OfferId = dataVM.OfferId,
                    Date = dataVM.Date,
                    EndDate = dataVM.EndDate,
                    customer = dataVM.customer,
                    product = dataVM.product,
                    number = dataVM.number,
                    price = dataVM.price,
                    discountRate = dataVM.discountRate,
                    totalPrice = dataVM.totalPrice,
                };


            _unitOfWork.Repository<PriceOffer>().InsertOrUpdate(data);
            _unitOfWork.SaveChanges();
            return new ApiResult { Result = true };
        }

        [HttpPost("GetPriceOfferGrid")]
        [AllowAnonymous]
        public ApiResult<GridResultModel<PriceOfferGridVM>> GetPriceOfferGrid()
        {

            var query = _unitOfWork.Repository<PriceOffer>()
            .Select(x => new PriceOfferGridVM
            {
                OfferId = x.OfferId,
                Date = x.Date,
                EndDate = x.EndDate,
                customer = x.customer,
                product = x.product,
                number = x.number,
                price = x.price,
                discountRate = x.discountRate,
                totalPrice = x.totalPrice,
            });

            var rest = query.ToDataListRequest(Request.ToRequestFilter());

            return new ApiResult<GridResultModel<PriceOfferGridVM>> { Data = rest, Result = true };
        }

    }
}

