using BETApplicationMVC.Shopify.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETApplicationMVC.Shopify.Logic
{
    public class Payment_Service
    {
        private Data_Context dataContext;

        public Payment_Service()
        {
            this.dataContext = new Data_Context();
        }

        public List<Payment> GetPayments()
        {
            return dataContext.Payments.ToList();
        }
        public Payment GetOrderPayment(string order_Id)
        {
            return GetPayments().FirstOrDefault(x => x.Order_ID == order_Id);
        }
    }
}
