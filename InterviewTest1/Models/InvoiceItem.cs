namespace InterviewTest1.Models
{
    public class InvoiceItem
    {
        /// <summary>
        /// Short Description of purchased item
        /// </summary>
        public string LineText { get; set; }

        /// <summary>
        /// Item is subject to tax
        /// </summary>
        public bool Taxable { get; set; }

        /// <summary>
        /// Number of units purchased
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Random price per unit
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Discount in whole percent, ie: 55 %
        /// </summary>
        public byte Discount { get; set; }

        /// <summary>
        /// Total for line item not including tax or discount
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Total for line item including tax
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Fills in subtotal and total from other data
        /// </summary>
        public void calculateTotals(decimal TaxRate)
        {
            SubTotal = UnitPrice * Quantity;
            // TaxRate and Discount should both be decimals, but they're not, so, whee, dumb math to do.
            Total = (SubTotal * (1m - Discount/100m)) * (1m + (Taxable ? TaxRate : 0m));
        }

        /// <summary>
        /// Returns the line item's total commission based on the rate
        /// </summary>
        /// <param name="CommissionRate"></param>
        /// <returns>Total commission for the line</returns>
        public decimal getCommission(decimal CommissionRate)
        {
            // Okay, so, I'm ignoring tax entirely when looking at the commission.  It's factored in before tax (per instructions), 
            //   and it itself doesn't get taxed (because it seemed that the commission was meant to be for the entire invoice, and tax is calculated on a per-line basis)
            return (SubTotal * (1m - Discount/100m)) * CommissionRate;
        }

        /// <summary>
        /// Example ToString implementation, update/replace as desired
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}Quantity: {1:00}\tPer Unit: $ {2:#,0.00}{5}\tDiscount: {3:00} %\tSubTotal: $ {4:#,0.00}\tTotal: $ {6:#,0.00}",
                                 LineText.PadRight(20),
                                 Quantity,
                                 UnitPrice,
                                 Discount,
                                 SubTotal,
                                 Taxable ? "T" : null,
                                 Total);
        }
    }
}
