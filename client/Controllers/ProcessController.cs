using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using Grpc.Core;

namespace client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly ILogger<ProcessController> _logger;
        public ProcessController(ILogger<ProcessController> logger)
        {
            _logger=logger;
        }
        [Route(nameof(Index))]
        [HttpGet]
        public IActionResult Index(){
   var channel = GrpcChannel.ForAddress("https://localhost:7076");
            var inventory = new Inventory.InventoryClient(channel);
            var request = new Product{Product_="rice"};
            var send = inventory.pushProduct(request);
            _logger.LogInformation((send.Status).ToString());
            return Content("hi all ");
        }
        [Route(nameof(RequestProduct))]
        [HttpGet]
        public IActionResult RequestProduct(string name,string username,double qty)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7076");
            var inventory = new Inventory.InventoryClient(channel);
            var request = new Product{Product_=name.ToString(),Quantity = qty};
            var send = inventory.pushProduct(request);
            _logger.LogInformation((send.Status).ToString());
            bool success = false;
            if (send.Status == true)
            {
                channel = GrpcChannel.ForAddress("https://localhost:7110");
                var payment = new PaymentService.PaymentServiceClient(channel);
                var recieve = payment.PaymentGateWaye(new PaymentRequest { Username = username, TotalPrice = send.TotalPrice });
                success = true;
            }
            
            return Ok((success + "  Total Price:"+send.TotalPrice).ToString());
        }
    }
}