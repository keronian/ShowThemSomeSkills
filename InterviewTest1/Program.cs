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

            foreach (Invoice inv in data)
            {
                inv.LineItems.ToList().ForEach(item => {
                    item.calculateTotals(inv.TaxRate);
                    inv.Commission += item.getCommission(COMMISSION_RATE);
                });
            }

            data.ToList().ForEach(Console.Write);

            Console.ReadLine();
        }
    }
}