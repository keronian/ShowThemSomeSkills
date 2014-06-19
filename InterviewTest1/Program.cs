using System;
using System.Linq;
using InterviewTest1.Models;

namespace InterviewTest1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var data = new Repo().All();

            Func<decimal, decimal, decimal> calculateSubTotal = delegate(decimal unitprice, decimal quantity)
            {
                return unitprice * quantity;
            };

            Func<decimal, decimal, decimal, decimal> calculateTotal = delegate(decimal subtotal, decimal discount, decimal taxrate)
            {
                // Applying tax after the discount is applied
                return (subtotal * (1m - discount)) * (1m + taxrate);
            };

            foreach (Invoice inv in data)
            {
                inv.LineItems.ToList().ForEach(item => {
                    item.SubTotal = calculateSubTotal(item.UnitPrice, item.Quantity);
                    // For some ridiculous reason, the Discount rate is a whole perecentage while the Tax Rate is a decimal.
                    item.Total = calculateTotal(item.SubTotal, item.Discount / 100m, item.Taxable ? inv.TaxRate : 0);
                });
                inv.SubTotal = inv.LineItems.Sum(x => x.Total);
                inv.Total = inv.SubTotal + inv.Shipping;
            }

            data.ToList().ForEach(Console.Write);

            Console.ReadLine();
        }
    }
}