using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApiLabor.Bll.Context;
using WebApiLabor.Bll.Entities;
using WebApiLabor.Bll.Exceptions;

namespace WebApiLabor.Bll.Services
{
    public class OrderService : IOrderService
    {
        private readonly NorthwindContext _context;

        public OrderService(NorthwindContext context)
        {
            _context = context;
        }

        public Order CreateOrder(int productId, Order order)
        {
            var product = _context.Products.Include(p => p.ProductOrders).SingleOrDefault(p => p.Id == productId) ?? throw new EntityNotFoundException("Nem található a termék");

            product.ProductOrders.Add(new ProductOrder()
            {
                Order = order,
                Product = product,
            });

            _context.SaveChanges();

            return order;
        }
    }
}
