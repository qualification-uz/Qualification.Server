using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities.Payments;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class PaymentService : IPaymentService
{
    private IMapper mapper;
    private readonly IPaymentRepository paymentRepository;
    private readonly IApplicationService applicationService;
    public PaymentService(
        IMapper mapper,
        IPaymentRepository paymentRepository,
        IApplicationService applicationService)
    {
        this.mapper = mapper;
        this.paymentRepository = paymentRepository;
        this.applicationService = applicationService;
    }

    public async ValueTask<PaymentDto> AddPaymentAsync(PaymentDtoForTeacher paymentForTeacherDto)
    {
        var payment = this.mapper.Map<Payment>(paymentForTeacherDto);

        payment = await this.paymentRepository.InsertPaymentAsync(payment);

        return this.mapper.Map<PaymentDto>(payment);
    }

    public async ValueTask<PaymentDto> AddPaymentAsync(PaymentForCreationDto paymentForCreationDto)
    {
        var payment = this.mapper.Map<Payment>(paymentForCreationDto);

        var application =
            await this.applicationService.RetrieveApplicationByIdAsync(payment.ApplicationId);

        if (application is null)
            throw new NotFoundException("Coudn't find application for given id");

        payment = await this.paymentRepository.InsertPaymentAsync(payment);

        return this.mapper.Map<PaymentDto>(payment);
    }

    public async ValueTask<PaymentDto> ModifyPaymentAsync(PaymentForCreationDto paymentForCreationDto)
    {
        var application =
            await this.applicationService.RetrieveApplicationByIdAsync(paymentForCreationDto.ApplicationId);

        if (application is null)
            throw new NotFoundException("Couldn't find aplication for given id");

        var mappedPayment = mapper.Map<Payment>(paymentForCreationDto);

        var payment =
            await this.paymentRepository.UpdatePaymentAsync(mappedPayment);

        return this.mapper.Map<PaymentDto>(payment);
    }

    public async ValueTask<PaymentDto> RemovePaymentAsync(long paymentId)
    {
        var payment =
               await this.paymentRepository.SelectPaymentByIdAsync(paymentId);

        if (payment is null)
            throw new NotFoundException("Couldn't find payment for given id");

        payment = await this.paymentRepository.DeletePaymentAsync(payment);

        return this.mapper.Map<PaymentDto>(payment);
    }

    public IEnumerable<PaymentDto> RetrieveAllPayments(
        PaginationParams @params,
        Filter filter)
    {
        var payments = this.paymentRepository
            .SelectAllPayments()
            .OrderBy(filter)
            .Include(payment => payment.Application);

        return this.mapper.Map<IEnumerable<PaymentDto>>(payments)
            .ToPagedList(@params);
    }

    public async ValueTask<PaymentDto> RetrievePaymentByIdAsync(long paymentId)
    {
        var payment =
            await this.paymentRepository.SelectPaymentByIdAsync(paymentId, new[] { "Application" });

        if (payment is null)
            throw new NotFoundException("Couldn't find payment for given id");

        return this.mapper.Map<PaymentDto>(payment);
    }
}
