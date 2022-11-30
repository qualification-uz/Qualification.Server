using Qualification.Domain.Entities.Payment;

namespace Qualification.Data.IRepositories;

public interface IPaymentRequestRepository
{
    ValueTask<PaymentRequest> InsertPaymentRequestAsync(PaymentRequest paymentRequest);
    IQueryable<PaymentRequest> SelectAllPaymentRequests();
    ValueTask<PaymentRequest> SelectPaymentRequestByIdAsync(long paymentRequestId);
    ValueTask<PaymentRequest> UpdatePaymentRequestAsync(PaymentRequest paymentRequest);
    ValueTask<PaymentRequest> DeletePaymentRequestAsync(PaymentRequest paymentRequest);
    IQueryable<PaymentAsset> SelectAllPaymentAssets(long paymentRequestId);
}