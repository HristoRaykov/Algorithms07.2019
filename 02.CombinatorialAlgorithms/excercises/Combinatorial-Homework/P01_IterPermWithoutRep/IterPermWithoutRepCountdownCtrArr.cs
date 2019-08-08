using System;
using System.Linq;

namespace P01_IterPermWithoutRep
{
    class IterPermWithoutRepCountdownCtrArr
    {
        public static void Main()
        {
            string[] arr = Console.ReadLine().Split(" ").ToArray();
            
 
            int[] ctrArr = new int[arr.Length + 1];
            
            // using "countdown" control array 0,1,..,n http://www.quickperm.org/
            for (int i = 0; i <= arr.Length; i++)
            {
                ctrArr[i] = i;
            }
 
            PrintArray(arr);
            
            var idx = 1;
            while (idx < arr.Length)
            {
                ctrArr[idx]--;
                var j = 0;
                if (idx % 2 == 1)
                {
                    j = ctrArr[idx];
                }
 
                Swap(arr, j, idx);
                idx = 1;
                while (ctrArr[idx] == 0)
                {
                    ctrArr[idx] = idx;
                    idx++;
                }

                PrintArray(arr);
            }
 
            
        }
 
        private static void Swap(string[] arr, int j, int idx)
        {
            var temp = arr[j];
            arr[j] = arr[idx];
            arr[idx] = temp;
        }

        private static void PrintArray(string[] perm)
        {
            Console.WriteLine(string.Join(" ", perm));
        }
    }
}