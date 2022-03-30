using Grpc.Core;
using web.server1;

namespace web.server1.Services;

public class InventoryService:Inventory.InventoryBase
{
    public static Product[]Data={new Product{Product_="rice",Quantity=11,Price=15},
    new Product{Product_="suger",Quantity=200,Price=10},
    new Product{Product_="met",Quantity=500,Price=170}
    };
    private readonly ILogger<InventoryService> _logger;
    public InventoryService(ILogger<InventoryService> logger)
    {
        _logger = logger;
    }
    
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    } 
     public override Task<Response> pushProduct(Product request,ServerCallContext context)
      {
          Product product;
          bool isMatch=false;
          double totalPrice = 0.0;
          foreach(var item in Data){
          if(request.Product_ == item.Product_)
           {
               if(request.Quantity<= item.Quantity){
                   isMatch=true;
                  product = item;
                  totalPrice = request.Quantity * item.Price;
                  _logger.LogInformation($"Product can be sell:{product.Product_} with price:{product.Price}");
                  break;
               }
           }
          }
        return Task.FromResult(new Response{
            Status=isMatch,
            TotalPrice = totalPrice
        });
      }
}
