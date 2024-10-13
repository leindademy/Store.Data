
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications.OrderSpec
{
    public class OrderWithItemSpecification : BaseSpecifications<Order>
    {
        public OrderWithItemSpecification(string buyerEmail) : base(o => o.BuyerEmail == buyerEmail) //Retrieve lists of orders
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderWithItemSpecification(Guid id) : base(o => o.id == id) // Retrieve one order
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);

        }
    }
}
