using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualification.Domain.Configurations;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [ApiController]
    [Route("api/payments")]
    [Authorize(Policy = "PaymentPolicy")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        /// <summary>
        /// Admin uchun to'lov yaratish
        /// </summary>
        /// <param name="paymentDto"></param>
        /// <returns></returns>
        [HttpPost("admin")]
        public async ValueTask<IActionResult> PostPaymentAsync(
            [FromBody] PaymentForCreationDto paymentDto) =>
                Ok(await this.paymentService.AddPaymentAsync(paymentDto));

        /// <summary>
        /// Teacher uchun to'lov yaratish
        /// </summary>
        /// <param name="paymentDtoForTeacher"></param>
        /// <returns></returns>
        [HttpPost("teacher")]
        [Authorize(Policy = "TeacherPaymentPolicy")]
        public async ValueTask<IActionResult> PostPaymentAsync(
            [FromBody] PaymentDtoForTeacher paymentDtoForTeacher) =>
                Ok(await this.paymentService.AddPaymentAsync(paymentDtoForTeacher));

        /// <summary>
        /// Barcha to'lovlarni olish
        /// </summary>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllPayments(
            [FromQuery] PaginationParams @params,
            [FromQuery] Filter filter) =>
            Ok(this.paymentService.RetrieveAllPayments(@params, filter));

        /// <summary>
        /// Id bo'yicha to'lovni olish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetPaymentByIdAsync(long id) =>
            Ok(await this.paymentService.RetrievePaymentByIdAsync(id));

        /// <summary>
        /// Berilgan id bilan tolovni malumotlarini o'zgartirish
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async ValueTask<IActionResult> PutPaymentAsync(
            long id, [FromBody] PaymentForCreationDto paymentDto) =>
                Ok(await this.paymentService.ModifyPaymentAsync(paymentDto));

        /// <summary>
        /// Berilgan id bilan to'lovni o'chirib yuborish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeletePaymentByIdAsync(long id) =>
            Ok(await this.paymentService.RemovePaymentAsync(id));
    }
}
