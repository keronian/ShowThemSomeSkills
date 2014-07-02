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

            // Exercise 1
            data.ToList().ForEach(InvoiceHelper.ExerciseOne);
            data.ToList().ForEach(x => { 
                Console.WriteLine(string.Format("Invoice #:{0}", x.InvoiceNo)); 
                x.LineItems.ToList().ForEach(Console.WriteLine);
                Console.WriteLine();
            });
            Console.ReadLine();

            // Exercise 2
            data.ToList().ForEach(InvoiceHelper.ExerciseTwo);
            data.ToList().ForEach(Console.Write);
            Console.ReadLine();

            // Exercise 3
            data.ToList().ForEach(InvoiceHelper.ExerciseThree);
            data.ToList().ForEach(Console.Write);
            Console.ReadLine();
        }
    }
}