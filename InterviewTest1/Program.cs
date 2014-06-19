using System;
using System.Linq;
using InterviewTest1.Models;

namespace InterviewTest1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 3%, per instructions
            const decimal COMMISSION_RATE = 0.03m;

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

            // Okay, so, I'm ignoring tax entirely when looking at the commission.  It's factored in before tax (per instructions), 
            //   and it itself doesn't get taxed (because it seemed that the commission was meant to be for the entire invoice, and tax is calculated on a per-line basis)
            Func<decimal, decimal, decimal, decimal> calculateCommission = delegate(decimal subtotal, decimal discount, decimal commissionrate)
            {
                return (subtotal * (1m - discount)) * commissionrate;
            };

            foreach (Invoice inv in data)
            {
                inv.LineItems.ToList().ForEach(item => {
                    item.SubTotal = calculateSubTotal(item.UnitPrice, item.Quantity);
                    // For some ridiculous reason, the Discount rate is a whole perecentage while the Tax Rate is a decimal.  Making it consistent for the delegate
                    item.Total = calculateTotal(item.SubTotal, item.Discount / 100m, item.Taxable ? inv.TaxRate : 0);
                    inv.Commission += calculateCommission(item.SubTotal, item.Discount / 100m, COMMISSION_RATE);
                });
                // This rounding looks odd... This is to round to the nearest 50 cents, so I double it, round to the nearest whole dollar, then cut it back in half.  Simple
                inv.Commission = Math.Round(inv.Commission * 2.0m, 0) / 2.0m;
                inv.SubTotal = inv.LineItems.Sum(x => x.Total);
                inv.Total = inv.SubTotal + inv.Shipping + inv.Commission;
            }

            data.ToList().ForEach(Console.Write);

            Console.ReadLine();
        }
    }
}