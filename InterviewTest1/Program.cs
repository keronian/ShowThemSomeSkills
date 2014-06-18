﻿using System;
using System.Linq;

namespace InterviewTest1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var repo = new Repo();

            repo.All()
                .ToList()
                .ForEach(invoice => invoice.LineItems.ToList()
                                           .ForEach(Console.WriteLine));

            Console.ReadLine();
        }
    }
}