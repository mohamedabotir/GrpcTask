using Grpc.Core;
using web.server2;

namespace web.server2.Services;

public class PaymentService :server2.PaymentService.PaymentServiceBase
{
    private readonly ILogger<PaymentService> _logger;
    public static Dictionary<string, double> users = new Dictionary<string, double>()
    {
        {"mohamed",12.0},
        {"ali",500},
        {"mona",700},
        {"adam",200}

    }; 
    public PaymentService(ILogger<PaymentService> logger)
    {
        _logger = logger;
    }

    public override Task<PaymentResponse> PaymentGateWaye(PaymentRequest request, ServerCallContext context)
    {
        bool success = false;
        foreach (var user in users)
        {
            if (user.Key == request.Username)
            {
                if (user.Value >= request.TotalPrice)
                {
                    success = true;
                }
            }
        }

        return Task.FromResult(new PaymentResponse()
        {
            Success = success
        });
    }
}
