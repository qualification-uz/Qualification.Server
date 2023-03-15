using Qualification.Domain.Configurations;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Service.Interfaces
{
    public interface IPaymentService
    {
        ValueTask<PaymentDto> AddPaymentAsync(PaymentDtoForTeacher paymentForTeacherDto);
        ValueTask<PaymentDto> AddPaymentAsync(PaymentForCreationDto paymentForCreationDto);
        ValueTask<PaymentDto> ModifyPaymentAsync(PaymentForCreationDto paymentForCreationDto);
        ValueTask<PaymentDto> RemovePaymentAsync(long paymentId);
        IEnumerable<PaymentDto> RetrieveAllPayments(
                PaginationParams @params,
                Filter filter);
        ValueTask<PaymentDto> RetrievePaymentByIdAsync(long paymentId);
    }
}
