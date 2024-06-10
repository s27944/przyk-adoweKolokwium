using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Exceptions;
using WebApi.Models;
using WebApi.RequestModels;
using WebApi.ResponseModels;

namespace WebApi.Services;

public interface IDatabaseService
{
    public Task<GetOrdersResponseModel> GetClientOrdersAsync(int clientId);
    public Task<int> PostClientOrder(int clientId, PostClientOrderModel request);
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
        var clientExists = await _context.Client.FirstOrDefaultAsync(c => c.clientID == clientId);
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

    public async Task<int> PostClientOrder(int clientId, PostClientOrderModel request)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                //sprawdzenie statusu   
                var statusId = await _context.Status
                    .Where(s => s.statusName == "Utworzone")
                    .Select(s => s.statusID)
                    .FirstOrDefaultAsync();
                if (statusId == null)
                {
                    throw new NotFoundException($"Brak statusu Utworzone w bazie danych");
                }

                //sprawdzenie klienta
                var client = await _context.Client
                    .FirstOrDefaultAsync(c => c.clientID == clientId);
                if (client == null)
                {
                    throw new NotFoundException($"Brak klienta o id {clientId}!");
                }

                //utworzenie nowego zamówienia
                var order = new Order
                {
                    CreatedAt = request.createdAt,
                    FulfilledAt = request.fulfilledAt,
                    ClientID = clientId,
                    StatusID = statusId
                };

                //dodanie zamówienia
                _context.Order.Add(order);
                
                //zapisanie zmian
                await _context.SaveChangesAsync();
                
                //dodanie produktów do zamówienia
                foreach (var product in request.products)
                {
                    var product_order = new Product_Order
                    {
                        ProductId = product.productId,
                        OrderId = order.ID,
                        amount = product.amount
                    };

                    _context.Product_Order.Add(product_order);
                }
                
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return order.ID;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}