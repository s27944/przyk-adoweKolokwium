namespace WebApi.ResponseModels;

public class GetOrdersResponseModel
{
    public List<OrdersList> ordersList { get; set; }

    
    public class OrdersList
    {
        public int orderId { get; set; }
        public string clientsLastName { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? fulfilledAt { get; set; }
        public string status { get; set; }
        public List<ProductList> products { get; set; }
    }

    public class ProductList
    {
        public string name { get; set; }
        public decimal price { get; set; }
        public int amount { get; set; }
    }
}

