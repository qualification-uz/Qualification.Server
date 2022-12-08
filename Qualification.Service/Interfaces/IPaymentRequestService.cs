using Qualification.Service.DTOs.Payment;

namespace Qualification.Service.Interfaces;

public interface IPaymentRequestService
{
    ValueTask<PaymentRequestDto> AddPaymentRequestAsync(PaymentRequestForCreationDto paymentRequestForCreationDto);
    IEnumerable<PaymentRequestDto> RetrieveAllPaymentRequests();
    ValueTask<PaymentRequestDto> RetrievePaymentRequestByIdAsync(long paymentRequestId);
    ValueTask<PaymentRequestDto> ModifyPaymentRequestAsync(long paymentRequestId, PaymentRequestForUpdateDto paymentRequestForUpdateDto);
    ValueTask<PaymentRequestDto> RemovePaymentRequestAsync(long paymentRequestId);
    IEnumerable<PaymentAssetDto> RetrievePaymentAssets(long paymentRequestId, bool isFromAdmin);
    ValueTask<PaymentAssetDto> AddPaymentAssetAsync(long paymentRequestId, bool isFromAdmin, long assetId);
    IEnumerable<PaymentAssetDto> RetrievePaymentsForApplication(long applicationId, bool isFromAdmin);
}