using System;
using System.Collections.Generic;
using System.Linq;

namespace ExerIP04ShopKeeper
{
    internal class Program
    {
        private static int[] products;

        private static int[] orders;

        private static HashSet<int> filledOrders;

        private static Dictionary<int, int> unfulfilledOrders;

        private static Dictionary<int, List<int>> ordersIdxes;


        public static void Main(string[] args)
        {
            products = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            orders = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            filledOrders = new HashSet<int>();
            unfulfilledOrders = new Dictionary<int, int>();
            ordersIdxes = new Dictionary<int, List<int>>();

            for (int i = 0; i < orders.Length; i++)
            {
                var order = orders[i];
                if (!unfulfilledOrders.ContainsKey(order))
                {
                    unfulfilledOrders[order] = 0;
                }

                unfulfilledOrders[order]++;

                if (!ordersIdxes.ContainsKey(order))
                {
                    ordersIdxes[order] = new List<int>();
                }

                ordersIdxes[order].Add(i);
            }

            if (!products.Contains(orders[0]))
            {
                Console.WriteLine("impossible");
                return;
            }

            var changeCount = 0;
            for (int orderIdx = 0; orderIdx < orders.Length; orderIdx++)
            {
                var order = orders[orderIdx];
                if (!products.Contains(order))
                {
                    var filledProductIdx = GetFilledProductIdx();
                    if (filledProductIdx != -1)
                    {
                        products[filledProductIdx] = order;
                    }
                    else
                    {
                        var productIdxToChange = FindFarestProductIdx();
                        products[productIdxToChange] = order;
                    }

                    changeCount++;
                }

                unfulfilledOrders[order]--;
                if (unfulfilledOrders[order] == 0)
                {
                    filledOrders.Add(order);
                    unfulfilledOrders.Remove(order);
                }

                ordersIdxes[order].Remove(orderIdx);
            }


            Console.WriteLine(changeCount);
        }

        private static int FindFarestProductIdx()
        {
            var maxIdx = -1;
            for (int prodIdx = 0; prodIdx < products.Length; prodIdx++)
            {
                var product = products[prodIdx];
                if (!ordersIdxes.Keys.Contains(product))
                {
                    return product;
                }

                var currIdx = ordersIdxes[product][0];
                if (currIdx > maxIdx)
                {
                    maxIdx = currIdx;
                }
            }
            

            return maxIdx;
        }


        private static int GetFilledProductIdx()
        {
            for (int prodIdx = 0; prodIdx < products.Length; prodIdx++)
            {
                var product = products[prodIdx];
                if (filledOrders.Contains(product))
                {
                    return prodIdx;
                }
            }

            return -1;
        }
    }
}