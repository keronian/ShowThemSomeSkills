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

            foreach (Invoice inv in data)
            {
                inv.LineItems.ToList().ForEach(item => {
                    item.calculateTotals(inv.TaxRate);
                });
            }

            data.ToList().ForEach(Console.Write);

            Console.ReadLine();
        }
    }
}