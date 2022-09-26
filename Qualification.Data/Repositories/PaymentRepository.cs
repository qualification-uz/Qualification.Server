using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Payments;

namespace Qualification.Data.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext appDbContext;
    public PaymentRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<Payment> DeletePaymentAsync(Payment payment)
    {
        EntityEntry<Payment> paymentEntityEntry =
            this.appDbContext.Payments.Remove(payment);

        await this.appDbContext.SaveChangesAsync();

        return paymentEntityEntry.Entity;
    }

    public async ValueTask<Payment> InsertPaymentAsync(Payment payment)
    {
        EntityEntry<Payment> paymentEntityEntry =
            await this.appDbContext.AddAsync(payment);

        await this.appDbContext.SaveChangesAsync();

        return paymentEntityEntry.Entity;
    }

    public IQueryable<Payment> SelectAllPayments() => this.appDbContext.Payments;

    public async ValueTask<Payment> SelectPaymentByIdAsync(long paymentId) =>
        await this.appDbContext.Payments.FindAsync(paymentId);

    public async ValueTask<Payment> SelectPaymentByIdAsync(
        long paymentId,
        IReadOnlyList<string> includes)
    {
        IQueryable<Payment> payments = this.appDbContext.Payments;

        foreach (var include in includes)
            payments = payments.Include(include);

        return await payments
            .FirstOrDefaultAsync(payment => payment.Id == paymentId);
    }

    public async ValueTask<Payment> UpdatePaymentAsync(Payment payment)
    {
        EntityEntry<Payment> paymentEntityEntry =
             this.appDbContext.Payments.Update(payment);

        await this.appDbContext.SaveChangesAsync();

        return paymentEntityEntry.Entity;
    }
}
