
using HospitalManagementSystem.Application;
using HospitalManagementSystem.Application.Abstraction.Services.Stripe;
using HospitalManagementSystem.Application.Stripe;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe;

namespace HospitalManagementSystem.Infrastructure.Implementations.Services;

public class StripeService : IStripeService
{
    private readonly StripeSettings _stripeSettings;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public StripeService(IOptions<StripeSettings> stripeSettings, IUnitOfWork unitOfWork, 
        IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _stripeSettings = stripeSettings.Value;
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    public async Task<string> CreatePaymentIntentAsync(decimal amount, string currency, Guid appointmentId)
    {
        amount = amount * 100;
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount),
            Currency = currency,
            PaymentMethodTypes = new List<string> { "card" }
        };

        var service = new PaymentIntentService();
        var paymentIntent = await service.CreateAsync(options);

        var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) throw new Exception("User not found!");

        Billing billing = new Billing()
        {
            User = user,
            AppointmentId = appointmentId,
            Amount = amount,
            Currency = currency,
            PaymentIntentId = paymentIntent.Id,
            ClientSecret = paymentIntent.ClientSecret
        };

        await _unitOfWork.BillingWriteRepository.AddAsync(billing);
        await _unitOfWork.SaveChangesAsync();

        return paymentIntent.ClientSecret;
    }
}
