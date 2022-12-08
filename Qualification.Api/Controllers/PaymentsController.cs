﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Payment;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRequestService paymentRequestService;

        public PaymentsController(IPaymentRequestService paymentRequestService)
        {
            this.paymentRequestService = paymentRequestService;
        }
        [HttpGet]
        public IActionResult GetAllPaymentRequests() =>
            Ok(this.paymentRequestService.RetrieveAllPaymentRequests());

        [HttpPost]
        public async ValueTask<IActionResult> PostPaymentRequest(
            [FromBody] PaymentRequestForCreationDto paymentDto) =>
            Ok(await this.paymentRequestService.AddPaymentRequestAsync(paymentDto));

        [HttpPost("{id}/assets")]
        public async ValueTask<IActionResult> PostPaymentAsset(
            long id, bool isFromAdmin, long assetId) =>
            Ok(await this.paymentRequestService.AddPaymentAssetAsync(id, isFromAdmin, assetId));

        [HttpGet("{id}/assets")]
        public IActionResult GetAllPaymentAssets(long id, bool isFromAdmin) =>
            Ok(this.paymentRequestService.RetrievePaymentAssets(id, isFromAdmin));

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetPaymentRequestByIdAsync(long id) =>
            Ok(await this.paymentRequestService.RetrievePaymentRequestByIdAsync(id));

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeletePaymentRequestAsync(long id) =>
            Ok(await this.paymentRequestService.RemovePaymentRequestAsync(id));
    }
}