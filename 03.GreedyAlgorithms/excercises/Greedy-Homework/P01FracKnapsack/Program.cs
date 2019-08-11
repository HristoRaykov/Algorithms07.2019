using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace P01FractKnapsack
{
    class Program
    {
        static void Main(string[] args)
        {
            var capacity = int.Parse(Console.ReadLine().Substring(10));
            int itemsCount = int.Parse(Console.ReadLine().Substring(7));

            IList<Item> items = new List<Item>();
            for (var i = 0; i < itemsCount; i++)
            {
                var properties = Console.ReadLine().Split(" -> ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Item item = new Item(double.Parse(properties[0]), double.Parse(properties[1]));
                items.Add(item);
            }

            var knapsack = LoadKnapsack(items, capacity);

            var totalPrice = 0.0;
            foreach (var item in knapsack)
            {
                Console.WriteLine(item);
                totalPrice += item.PricePerKg * item.Weight * item.PercentTaken;
            }

            Console.WriteLine($"Total price: {totalPrice:F2}");
        }

        private static IEnumerable<Item> LoadKnapsack(IList<Item> items, int capacity)
        {
            var knapsack = new List<Item>();

            items = items.OrderByDescending(item => item.PricePerKg).ToList();

            var idx = 0;
            Item currItem;
            while (idx < items.Count && capacity > 0)
            {
                currItem = items[idx++];
                var itemWeight = currItem.Weight;

                if (itemWeight <= capacity)
                {
                    capacity -= (int) itemWeight;
                    currItem.PercentTaken = 1.0;
                }
                else
                {
                    currItem.PercentTaken = capacity / itemWeight;
                    capacity = 0;
                }

                knapsack.Add(currItem);

                if (capacity == 0)
                {
                    break;
                }
            }

            return knapsack;
        }
    }

    class Item
    {
        public double Price { get; }

        public double Weight { get; }

        public double PricePerKg => this.Price / this.Weight;

        public double PercentTaken { get; set; }

        public Item(double price, double weight)
        {
            Price = price;
            Weight = weight;
        }

        public override string ToString()
        {
            var percent = this.PercentTaken == 1.00 ? "100" : $"{this.PercentTaken * 100:F2}";
            return $"Take {percent}% of item with price {this.Price:F2} and weight {this.Weight:F2}";
        }
    }
}