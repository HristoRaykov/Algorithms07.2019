using System;
using System.Collections.Generic;

namespace ExerIP02Parentheses
{
    internal class Program
    {
        private static int N;

        private static char[] parentheses;
        
        private static List<string> combinations;

        // open and unclosed '(' parentheses count
        private static int openParCounter;

        private static int closeParCounter;


        public static void Main(string[] args)
        {
            N = int.Parse(Console.ReadLine());

            parentheses = new char[N * 2];
            combinations = new List<string>();
            
            openParCounter = 1;
            closeParCounter = 0;
            parentheses[0] = '(';
            CalculateValidParentheses(1);

            Console.WriteLine(string.Join(Environment.NewLine, combinations));
        }

        private static void CalculateValidParentheses(int idx)
        {
            
            if (idx == 2 * N)
            {
                combinations.Add(new string(parentheses));
                return;
            }

            if (openParCounter == closeParCounter && openParCounter < N)
            {
                parentheses[idx] = '(';
                openParCounter++;
                CalculateValidParentheses(idx + 1);
                openParCounter--;
            }
            else if (openParCounter < N && closeParCounter < N)
            {
                parentheses[idx] = '(';
                openParCounter++;
                CalculateValidParentheses(idx + 1);
                openParCounter--;

                parentheses[idx] = ')';
                closeParCounter++;
                CalculateValidParentheses(idx + 1);
                closeParCounter--;
            }
            else if (openParCounter == N)
            {
                parentheses[idx] = ')';
                closeParCounter++;
                CalculateValidParentheses(idx + 1);
                closeParCounter--;
            }

        }
        
    }
}
