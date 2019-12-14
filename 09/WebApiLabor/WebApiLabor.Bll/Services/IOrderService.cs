using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiLabor.Bll.Entities;

namespace WebApiLabor.Bll.Services
{
    public interface IOrderService
    {
        Order CreateOrder(int productId, Order order);
    }
}