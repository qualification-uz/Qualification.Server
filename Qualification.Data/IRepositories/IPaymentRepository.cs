using Qualification.Domain.Entities.Payments;

namespace Qualification.Data.IRepositories;

public interface IPaymentRepository
{
    IQueryable<Payment> SelectAllPayments();
    ValueTask<Payment> SelectPaymentByIdAsync(long paymentId);

    ValueTask<Payment> SelectPaymentByIdAsync(
        long paymentId,
        IReadOnlyList<string> includes);

    ValueTask<Payment> InsertPaymentAsync(Payment payment);
    ValueTask<Payment> UpdatePaymentAsync(Payment payment);
    ValueTask<Payment> DeletePaymentAsync(Payment payment);
}
