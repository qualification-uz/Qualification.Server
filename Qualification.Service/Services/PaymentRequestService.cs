using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Payment;
using Qualification.Service.DTOs.Payment;
using Qualification.Service.Exceptions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class PaymentRequestService : IPaymentRequestService
{
    private readonly IPaymentRequestRepository paymentRequestRepository;
    private readonly IMapper mapper;

    public PaymentRequestService(
        IPaymentRequestRepository paymentRequestRepository,
        IMapper mapper)
    {
        this.paymentRequestRepository = paymentRequestRepository;
        this.mapper = mapper;
    }

    public async ValueTask<PaymentRequestDto> AddPaymentRequestAsync(
        PaymentRequestForCreationDto paymentRequestForCreationDto)
    {
        var paymentRequest = this.mapper
            .Map<PaymentRequest>(paymentRequestForCreationDto);

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

    public IEnumerable<PaymentRequestDto> RetrieveAllPaymentRequests()
    {
        var paymentRequests = this.paymentRequestRepository.SelectAllPaymentRequests();

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

    public async ValueTask<PaymentAssetDto> AddPaymentAssetAsync(long paymentRequestId, bool isFromAdmin, long assetId)
    {
        var paymentRequests = this.paymentRequestRepository.SelectAllPaymentRequests();

        var paymentRequest = await paymentRequests
            .Include(request => request.Assets)
            .Where(payment => payment.Id == paymentRequestId)
            .FirstAsync();

        var paymentAsset = new PaymentAsset
        {
            IsFromAdmin = false,
            AssetId = assetId,
        };

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
            .Where(asset => asset.IsFromAdmin == isFromAdmin);

        return this.mapper.Map<IEnumerable<PaymentAssetDto>>(paymentAssets);
    }
}
