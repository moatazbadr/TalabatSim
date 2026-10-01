using Talabat.Domain;
using Talabat.Domain.Entities;
using Talabat.Domain.Entities.Order;
using Talabat.Domain.Repositories;
using Talabat.Domain.Services;
using Talabat.Domain.Specifications;

namespace Talabat.Services;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;
    public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
    {
        _basketRepository = basketRepository;
        _unitOfWork = unitOfWork;



    }
    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
        //1- Get the basket from the basket repository 
        var Basket = await _basketRepository.GetBasketAsync(basketId);

        //2-Get selected Item from product Repo
        var orderItems = new List<OrderItem>();
        if (Basket?.Items.Count >0)
        {
            foreach(var item in Basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, item.Quantity, product.Price);
                orderItems.Add(orderItem);
            }
        }


        //3-calculate the subtotal
        var subtotal = orderItems.Sum(item => item.price * item.Quantity);

        //4-get the delivery method from the delivery method repo
        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

        //5-Create the order
        var order = new Order(buyerEmail,shippingAddress,orderItems,deliveryMethod,subtotal);

        //6- Save the order to the memory 
        await _unitOfWork.Repository<Order>().AddAsync(order);

         
        //7-save the order to the database 
       var result= await _unitOfWork.completeAsync();

        if(result <= 0) return null;

        return order;
    }

    public async Task<Order> GetOrderByIdAsync(string buyerEmail, int Orderid)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification (buyerEmail , Orderid);
        var Order = await _unitOfWork.Repository<Order>().GetByIdWithSpec(spec);
        return Order;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

        IReadOnlyList<Order> Orders = await _unitOfWork.Repository<Order>().GetAllWithSpec(spec);
       
        return Orders;

    }
}
