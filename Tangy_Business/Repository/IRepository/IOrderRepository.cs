using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_DataAccess;
using Tangy_Models;

namespace Tangy_Business.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<OrderDTO> Get(int id);
        /// <summary>
        /// Get all the orders
        /// </summary>
        /// <param name="userId">If we want to retrieve all the orders from a specific user</param>
        /// <param name="status">If we want to retrieve all the orders with a specific status</param>
        /// <returns></returns>
        public Task<IEnumerable<OrderDTO>> GetAll(string? userId=null,string? status = null);
        public Task<OrderDTO> Create(OrderDTO objDTO);
        public Task<int> Delete(int id);

        public Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO objDTO);
        /// <summary>
        /// We update the status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<OrderHeaderDTO> MarkPaymentSuccessful(int id);
        public Task<bool> UpdateOrderStatus(int orderId, string status);

        public Task<OrderHeaderDTO> CancelOrder(int id);
    }
}
