using InterviewTest1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewTest1
{
    public class InvoiceHelper
    {
        // 3%, per instructions
        const decimal COMMISSION_RATE = 0.03m;

        static void calculateSubTotalForItem(InvoiceItem item)
        {
            item.SubTotal = item.UnitPrice * item.Quantity;
        }

        // Assumption: Subtotal has been calculated already
        static void calculateTotalForItem(InvoiceItem item, decimal TaxRate)
        {
            // TaxRate and Discount should both be decimals, but they're not, so, whee, dumb math to do.
            item.Total = (item.SubTotal * (1m - item.Discount / 100m)) * (1m + (item.Taxable ? TaxRate : 0m));
        }

        // Assumption: Line item totals have been calculated
        static void calculateSubTotal(Invoice inv)
        { 
            inv.SubTotal = inv.LineItems.Sum(x => { calculateTotalForItem(x, inv.TaxRate); return x.Total; });
        }

        // Assumption: Total has been calculated
        static void calculateTotal(Invoice inv)
        { 
            inv.Total = inv.SubTotal + inv.Shipping;// +inv.Commission;
        }

        static decimal getCommissionForItem(InvoiceItem item, decimal CommissionRate)
        { 
            // Okay, so, I'm ignoring tax entirely when looking at the commission.  It's factored in before tax (per instructions), 
            //   and it itself doesn't get taxed (because it seemed that the commission was meant to be for the entire invoice, and tax is calculated on a per-line basis)
            return (item.SubTotal * (1m - item.Discount/100m)) * CommissionRate;
        }

        // Assumption: Subtotal and Discount have been calculated for each line item
        static void calculateCommission(Invoice inv)
        { 
            decimal commission;
            commission = inv.LineItems.Sum(x => getCommissionForItem(x, COMMISSION_RATE));
            // This rounding looks odd... This is to round to the nearest 50 cents, so I double it, round to the nearest whole dollar, then cut it back in half.  Simple
            inv.Commission = Math.Round(commission * 2.0m, 0) / 2.0m;
        }

        public static void ExerciseOne(Invoice inv)
        {
            foreach (InvoiceItem item in inv.LineItems)
            {
                calculateSubTotalForItem(item);
                calculateTotalForItem(item, inv.TaxRate);
            }
        }

        public static void ExerciseTwo(Invoice inv)
        {
            calculateSubTotal(inv);
            calculateTotal(inv);
        }

        public static void ExerciseThree(Invoice inv)
        {
            calculateCommission(inv);
        }
    }
}
