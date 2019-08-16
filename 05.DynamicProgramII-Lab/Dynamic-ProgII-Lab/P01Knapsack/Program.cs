using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace P01Knapsack
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var maxCapacity = int.Parse(Console.ReadLine());

            var items = ReadItems();

            var maxPrice = new int[items.Count + 1, maxCapacity + 1];
            var isItemTaken = new bool[items.Count + 1, maxCapacity + 1];

            FillKnapsack(items, maxCapacity, maxPrice, isItemTaken);

            var maxTotalValue = maxPrice[maxPrice.GetLength(0) - 1, maxPrice.GetLength(1) - 1];

            var itemsTaken = ReconstructTakenItems(items, maxPrice, isItemTaken);

            Console.WriteLine($"Total Weight: {itemsTaken.Sum(i => i.Weight)}");
            Console.WriteLine($"Total Value: {maxTotalValue}");
            foreach (var item in itemsTaken)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static void FillKnapsack(IList<Item> items, int maxCapacity, int[,] maxPrice, bool[,] isItemTaken)
        {
            // filling max price matrix
            for (int row = 0; row < maxPrice.GetLength(0); row++)
            {
                for (int col = 0; col < maxPrice.GetLength(1); col++)
                {
                    if (row == 0)
                    {
                        maxPrice[row, col] = 0;
                        continue;
                    }

                    var item = items[row - 1];
                    var currCapacity = col;
                    var valueExcluding = maxPrice[row - 1, col];
                    if (item.Weight > currCapacity)
                    {
                        // taking best previous max price if can not put item into knapsack
                        maxPrice[row, col] = valueExcluding;
                    }
                    else
                    {
                        var valueIncluding = item.Price + maxPrice[row - 1, col - item.Weight];
                        if (valueExcluding >= valueIncluding)
                        {
                            maxPrice[row, col] = valueExcluding;
                        }
                        else
                        {
                            maxPrice[row, col] = valueIncluding;
                            isItemTaken[row, col] = true;
                        }
                    }
                }
            }
        }

        private static IList<Item> ReconstructTakenItems(IList<Item> items, int[,] maxPrice, bool[,] isItemTaken)
        {
            var itemsTaken = new List<Item>();

            var row = maxPrice.GetLength(0) - 1;
            var col = maxPrice.GetLength(1) - 1;

            var totalPrice = maxPrice[row, col];

            while (totalPrice > 0)
            {
                if (isItemTaken[row, col])
                {
                    var item = items[row - 1];
                    itemsTaken.Add(item);
                    totalPrice -= item.Price;
                    col -= item.Weight;
                }
                row--;
                
            }

            itemsTaken = itemsTaken.OrderBy(i=>i.Name).ToList();
            return itemsTaken;
        }

        private static IList<Item> ReadItems()
        {
            var items = new List<Item>();

            while (true)
            {
                var input = Console.ReadLine();

                if (input.Equals("end"))
                {
                    break;
                }

                var itemArr = input.Split(' ');

                var item = new Item(itemArr[0], int.Parse(itemArr[1]), int.Parse(itemArr[2]));
                items.Add(item);
            }

            return items;
        }
    }

    class Item
    {
        public string Name { get; }

        public int Weight { get; }

        public int Price { get; }

        public Item(string name, int weight, int price)
        {
            Name = name;
            Weight = weight;
            Price = price;
        }
    }
}