using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models;
using WebApi.ResponseModels;

namespace WebApi.Services;

public interface IDatabaseService
{
    public Task<GetOrdersResponseModel> GetClientOrdersAsync(int clientId);
}

public class DatabaseService : IDatabaseService
{
    private readonly DatabaseContext _context;

    public DatabaseService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<GetOrdersResponseModel> GetClientOrdersAsync(int clientId)
    {
        var clientExists = _context.Client.Where(c => c.clientID == clientId);
        if (clientExists == null)
        {
            return null;
        }

        var ordersList = await (
            from order in _context.Order
            join client in _context.Client on order.ClientID equals client.clientID
            join status in _context.Status on order.StatusID equals status.statusID
            where order.ClientID == clientId
            select new GetOrdersResponseModel.OrdersList
            {
                orderId = order.ID,
                clientsLastName = client.LastName,
                createdAt = order.CreatedAt,
                fulfilledAt = order.FulfilledAt,
                status = status.statusName,
                products = new List<GetOrdersResponseModel.ProductList>() //puste trzeba wypelnic
            }).ToListAsync();

        foreach (var order in ordersList)
        {
            var products = await (
                from po in _context.Product_Order
                join product in _context.Product on po.ProductId equals product.productID
                where po.OrderId == order.orderId
                select new GetOrdersResponseModel.ProductList
                {
                    name = product.productName,
                    price = product.productPrice,
                    amount = po.amount
                }
            ).ToListAsync();

            order.products = products;
        }

        return new GetOrdersResponseModel
        {
            ordersList = ordersList
        };
    }
}