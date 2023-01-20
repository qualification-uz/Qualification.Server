using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities.Payment;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Payment;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class PaymentRequestService : IPaymentRequestService
{
    private readonly IPaymentRequestRepository paymentRequestRepository;
    private readonly IApplicationRepository applicationRepository;
    private readonly IMapper mapper;

    public PaymentRequestService(
        IPaymentRequestRepository paymentRequestRepository,
        IMapper mapper,
        IApplicationRepository applicationRepository)
    {
        this.paymentRequestRepository = paymentRequestRepository;
        this.mapper = mapper;
        this.applicationRepository = applicationRepository;
    }

    public async ValueTask<PaymentRequestDto> AddPaymentRequestAsync(
        PaymentRequestForCreationDto paymentRequestForCreationDto)
    {

        var application = await this.applicationRepository
            .SelectApplicationByIdAsync(paymentRequestForCreationDto.ApplicationId);

        if (application is null)
        {
            throw new NotFoundException("Application is not found with given id");
        }

        var paymentRequest = this.mapper
            .Map<PaymentRequest>(paymentRequestForCreationDto);

        application.Status = ApplicationStatus.TolovKutilmoqda;

        paymentRequest.Assets = new List<PaymentAsset>();

        foreach (var assetId in paymentRequestForCreationDto.AssetIds)
        {
            paymentRequest.Assets.Add(new PaymentAsset
            {
                AssetId = assetId,
                IsFromAdmin = true
            });
        }

        await this.paymentRequestRepository
            .InsertPaymentRequestAsync(paymentRequest);

        return this.mapper.Map<PaymentRequestDto>(paymentRequest);
    }

    public IEnumerable<PaymentRequestDto> RetrieveAllPaymentRequests(PaginationParams @params, Filters filters)
    {
        var paymentRequests = this.paymentRequestRepository.SelectAllPaymentRequests();

        paymentRequests = filters.Aggregate(paymentRequests, (current, filter) => current.Filter(filter));

        return this.mapper.Map<IEnumerable<PaymentRequestDto>>(paymentRequests);
    }

    public async ValueTask<PaymentRequestDto> RetrievePaymentRequestByIdAsync(long paymentRequestId)
    {
        var paymentRequest = await this.paymentRequestRepository
            .SelectPaymentRequestByIdAsync(paymentRequestId);

        if(paymentRequest is null)
            throw new NotFoundException("Couldn't find payment request for given id");

        return this.mapper.Map<PaymentRequestDto>(paymentRequest);
    }

    public ValueTask<PaymentRequestDto> ModifyPaymentRequestAsync(long paymentRequestId, PaymentRequestForUpdateDto paymentRequestForUpdateDto)
    {
        throw new NotImplementedException();
    }

    public ValueTask<PaymentRequestDto> RemovePaymentRequestAsync(long paymentRequestId)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<PaymentAssetDto> AddPaymentAssetAsync(long applicationId, bool isFromAdmin, long assetId)
    {
        var paymentRequests = this.paymentRequestRepository.SelectAllPaymentRequests();

        var paymentRequest = await paymentRequests
            .Include(request => request.Assets)
            .Include(request => request.Application)
            .Where(payment => payment.ApplicationId == applicationId)
            .FirstOrDefaultAsync();
        
        if (paymentRequest is null)
            throw new NotFoundException("Couldn't find payment request for given id");

        var paymentAsset = new PaymentAsset
        {
            IsFromAdmin = isFromAdmin,
            AssetId = assetId,
        };

        if (!isFromAdmin)
        {
            paymentRequest.Application.Status = ApplicationStatus.TolovQilindi;
        }

        paymentRequest.Assets.Add(paymentAsset);

        await this.paymentRequestRepository.UpdatePaymentRequestAsync(paymentRequest);

        return this.mapper.Map<PaymentAssetDto>(paymentAsset);
    }

    public IEnumerable<PaymentAssetDto> RetrievePaymentAssets(long paymentRequestId, bool isFromAdmin)
    {
        var paymentRequests = this.paymentRequestRepository.SelectAllPaymentRequests();

        var paymentAssets = paymentRequests
            .Include(request => request.Assets)
            .Where(payment => payment.Id == paymentRequestId)
            .OrderByDescending(request => request.CreatedAt)
            .SelectMany(payment => payment.Assets);

        return this.mapper.Map<IEnumerable<PaymentAssetDto>>(paymentAssets);
    }

    public IEnumerable<PaymentAssetDto> RetrievePaymentsForApplication(long applicationId, bool isFromAdmin)
    {
        var paymentRequests = this.paymentRequestRepository.SelectAllPaymentRequests();

        var paymentAssets = paymentRequests
            .Include(request => request.Assets)
            .Include(request => request.Application)
            .Where(request => request.ApplicationId == applicationId)
            .SelectMany(request => request.Assets)
            .Include(payment => payment.PaymentRequest)
            .Where(asset => asset.IsFromAdmin == isFromAdmin)
            .OrderByDescending(request => request.CreatedAt);

        return this.mapper.Map<IEnumerable<PaymentAssetDto>>(paymentAssets);
    }
}
