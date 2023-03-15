using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Payment;

namespace Qualification.Data.Repositories;

public class PaymentRequestRepository : IPaymentRequestRepository
{
    private readonly AppDbContext appDbContext;

    public PaymentRequestRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<PaymentRequest> InsertPaymentRequestAsync(PaymentRequest paymentRequest)
    {
        EntityEntry<PaymentRequest> paymentEntityEntry =
            await this.appDbContext.PaymentRequests.AddAsync(paymentRequest);

        await this.appDbContext.SaveChangesAsync();

        return paymentEntityEntry.Entity;
    }

    public IQueryable<PaymentRequest> SelectAllPaymentRequests() =>
        this.appDbContext.PaymentRequests;

    public async ValueTask<PaymentRequest> SelectPaymentRequestByIdAsync(long paymentRequestId) =>
        await this.appDbContext.PaymentRequests.FindAsync(paymentRequestId);

    public async ValueTask<PaymentRequest> UpdatePaymentRequestAsync(PaymentRequest paymentRequest)
    {
        EntityEntry<PaymentRequest> paymentEntityEntry =
            this.appDbContext.PaymentRequests.Update(paymentRequest);

        await this.appDbContext.SaveChangesAsync();

        return paymentEntityEntry.Entity;
    }

    public async ValueTask<PaymentRequest> DeletePaymentRequestAsync(PaymentRequest paymentRequest)
    {
        EntityEntry<PaymentRequest> paymentEntityEntry =
            this.appDbContext.PaymentRequests.Remove(paymentRequest);

        await this.appDbContext.SaveChangesAsync();

        return paymentEntityEntry.Entity;
    }

    public IQueryable<PaymentAsset> SelectAllPaymentAssets(long paymentRequestId) =>
        this.appDbContext.PaymentAssets.Where(asset =>
            asset.PaymentRequestId == paymentRequestId);
}