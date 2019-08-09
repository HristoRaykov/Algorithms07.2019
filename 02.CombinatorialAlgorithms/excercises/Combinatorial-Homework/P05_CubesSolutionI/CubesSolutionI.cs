using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace P05_CubesSolutionI
{
    internal class Cube
    {
        public int FrontTop { get; set; }
        public int FrontRight { get; set; }
        public int FrontBottom { get; set; }
        public int FrontLeft { get; set; }
 
        public int BackTop { get; set; }
        public int BackRight { get; set; }
        public int BackBottom { get; set; }
        public int BackLeft { get; set; }
 
        public int TopLeft { get; set; }
        public int TopRight { get; set; }
        public int BottomLeft { get; set; }
        public int BottomRight { get; set; }
    }
 
    class CubesSolutionI
    {
 
        private static int[] colors;
        private static HashSet<string> result = new HashSet<string>();
        private static HashSet<string> allPossibleCubes = new HashSet<string>();
 
 
        static void Main()
        {
            var input = Console.ReadLine()
                .Split(' ')
                .Select(s => int.Parse(s))
                .ToArray();
            colors = input.OrderBy(s => s).ToArray();
            Permute(colors, 0, 11);
            Console.WriteLine(result.Count);
        }
 
        private static void Permute(int[] arr, int start, int end)
        {
            MarkCube();
            for (int left = end - 1; left >= start; left--)
            {
                for (int right = left + 1; right <= end; right++)
                {
                    if (arr[left] == arr[right]) continue;
 
                    Swap(ref arr[left], ref arr[right]);
                    Permute(arr, left + 1, end);
                }
 
                var firstElement = arr[left];
                for (var i = left; i <= end - 1; i++)
                {
                    arr[i] = arr[i + 1];
                }
                arr[end] = firstElement;
            }
        }
 
        static void Swap<T>(ref T first, ref T second)
        {
            var oldFirst = first;
            first = second;
            second = oldFirst;
        }
 
        private static void MarkCube()
        {
            Cube newCube = new Cube()
            {
                FrontTop = colors[0],
                FrontRight = colors[1],
                FrontBottom = colors[2],
                FrontLeft = colors[3],
                BackTop = colors[4],
                BackRight = colors[5],
                BackBottom = colors[6],
                BackLeft = colors[7],
                TopLeft = colors[8],
                TopRight = colors[9],
                BottomLeft = colors[10],
                BottomRight = colors[11]
            };
            string cube = ConvertCubeToString(newCube);
 
            if (allPossibleCubes.Contains(cube))
            {
                return;
            }
            result.Add(cube);
 
            for (int az = 0; az < 3; az++)
            {
                allPossibleCubes.Add(cube);
                newCube = AzimuthRotate(newCube);
                cube = ConvertCubeToString(newCube);
                for (int el = 0; el < 3; el++)
                {
                    allPossibleCubes.Add(cube);
                    newCube = ElevationRotate(newCube);
                    cube = ConvertCubeToString(newCube);
                    for (int roll = 0; roll < 4; roll++)
                    {
                        allPossibleCubes.Add(cube);
                        newCube = RollRotate(newCube);
                        cube = ConvertCubeToString(newCube);
                    }
                }
            }
 
        }
 
        private static string ConvertCubeToString(Cube cube)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(cube.FrontTop);
            sb.Append(cube.FrontRight);
            sb.Append(cube.FrontLeft);
            sb.Append(cube.FrontBottom);
            sb.Append(cube.BackTop);
            sb.Append(cube.BackRight);
            sb.Append(cube.BackLeft);
            sb.Append(cube.BackBottom);
            sb.Append(cube.TopRight);
            sb.Append(cube.TopLeft);
            sb.Append(cube.BottomRight);
            sb.Append(cube.BottomLeft);
            return sb.ToString();
        }
 
        private static Cube AzimuthRotate(Cube cube)
        {
            var newCube = new Cube()
            {
                FrontTop = cube.TopRight,
                TopLeft = cube.FrontTop,
                BackTop = cube.TopLeft,
                TopRight = cube.BackTop,
                FrontLeft = cube.FrontRight,
                BackLeft = cube.FrontLeft,
                BackRight = cube.BackLeft,
                FrontRight = cube.BackRight,
                FrontBottom = cube.BottomRight,
                BottomLeft = cube.FrontBottom,
                BackBottom = cube.BottomLeft,
                BottomRight = cube.BackBottom
            };
 
            return newCube;
        }
 
        private static Cube ElevationRotate(Cube cube)
        {
            var newCube = new Cube()
            {
                FrontTop = cube.BackTop,
                FrontBottom = cube.FrontTop,
                BackBottom = cube.FrontBottom,
                BackTop = cube.BackBottom,
                FrontLeft = cube.TopLeft,
                BottomLeft = cube.FrontLeft,
                BackLeft = cube.BottomLeft,
                TopLeft = cube.BackLeft,
                FrontRight = cube.TopRight,
                BottomRight = cube.FrontRight,
                BackRight = cube.BottomRight,
                TopRight = cube.BackRight
            };
 
            return newCube;
        }
 
        private static Cube RollRotate(Cube cube)
        {
            var newCube = new Cube()
            {
                FrontTop = cube.FrontRight,
                FrontLeft = cube.FrontTop,
                FrontBottom = cube.FrontLeft,
                FrontRight = cube.FrontBottom,
                BackTop = cube.BackRight,
                BackLeft = cube.BackTop,
                BackBottom = cube.BackLeft,
                BackRight = cube.BackBottom,
                TopLeft = cube.TopRight,
                BottomLeft = cube.TopLeft,
                BottomRight = cube.BottomLeft,
                TopRight = cube.BottomRight
            };
 
            return newCube;
        }
 
    }
}