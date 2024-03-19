using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : Controller
{
    [HttpPost]
    public ActionResult<PaymentResponse> ProcessPayment(PaymentRequest request)
    {
        // Your payment processing logic goes here

        // Example: Simulate successful payment
        var response = new PaymentResponse
        {
            Success = true,
            Message = "Payment processed successfully",
            TransactionId = Guid.NewGuid().ToString() // Generate a unique transaction ID
        };

        return Ok(response);
    }
}
