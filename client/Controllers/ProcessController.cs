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
        public IActionResult RequestProduct(string name,string username){
            var channel = GrpcChannel.ForAddress("https://localhost:7076");
            var inventory = new Inventory.InventoryClient(channel);
            var request = new Product{Product_=name};
            var send = inventory.pushProduct(request);
            _logger.LogInformation((send.Status).ToString());
            
            return Ok((send.Status).ToString());
        }
    }
}