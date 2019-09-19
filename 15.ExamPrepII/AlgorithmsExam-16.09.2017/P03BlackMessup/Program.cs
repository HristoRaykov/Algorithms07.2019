using System;
using System.Collections.Generic;
using System.Linq;

namespace P03BlackMessup
{
    internal class Program
    {
        private static Dictionary<string, Atom> atoms;

        private static Dictionary<string, List<string>> graph;

        private static List<SortedSet<Atom>> molecules;

        public static void Main(string[] args)
        {
            ReadInput();

            // calculate connected components
            CalculateMolecules();
            
            var moleculesForce = new SortedSet<int>(Comparer<int>.Create((x1,x2)=>x2-x1));

            foreach (var molecule in molecules)
            {
                var force = CalculateMoleculeForce(molecule);
                moleculesForce.Add(force);
            }


            Console.WriteLine(moleculesForce.First());
        }

        private static int CalculateMoleculeForce(SortedSet<Atom> molecule)
        {
            var maxDecay = 1;
            var count = 0;
            var force = 0;

            foreach (var atom in molecule)
            {
                if (atom.Decay>maxDecay)
                {
                    maxDecay = atom.Decay;
                    count++;
                    force += atom.Mass;
                }
                else if (maxDecay > count)
                {
                    count++;
                    force += atom.Mass;
                }
            }

            return force;
        }

        private static void CalculateMolecules()
        {
            var visited = new HashSet<string>();
            foreach (var atom in graph.Keys)
            {
                if (!visited.Contains(atom))
                {
                    molecules.Add(new SortedSet<Atom>());
                    Dfs(atom, atom, visited);
                }
            }
        }

        private static void Dfs(string atom, string prevAtom, HashSet<string> visited)
        {
            visited.Add(atom);
            molecules.Last().Add(atoms[atom]);
            foreach (var child in graph[atom])
            {
                if (!visited.Contains(child) && child != prevAtom)
                {
                    Dfs(child, atom, visited);
                }
            }
        }

        private static void ReadInput()
        {
            var atomsCount = int.Parse(Console.ReadLine());
            var edgeCount = int.Parse(Console.ReadLine());

            atoms = new Dictionary<string, Atom>();
            graph = new Dictionary<string, List<string>>();
            molecules = new List<SortedSet<Atom>>();


            for (int i = 0; i < atomsCount; i++)
            {
                var line = Console.ReadLine().Split().ToArray();

                var atom = new Atom(line[0], int.Parse(line[1]), int.Parse(line[2]));

                atoms[line[0]] = atom;
            }

            foreach (var atom in atoms.Keys)
            {
                graph[atom] = new List<string>();
            }

            for (int i = 0; i < edgeCount; i++)
            {
                var line = Console.ReadLine().Split().ToArray();

                graph[line[0]].Add(line[1]);
                graph[line[1]].Add(line[0]);
            }
        }
    }

    internal class Atom : IComparable<Atom>
    {
        public string Name { get; }

        public int Mass { get; }

        public int Decay { get; }

        public Atom(string name, int mass, int decay)
        {
            Name = name;
            Mass = mass;
            Decay = decay;
        }

        public int CompareTo(Atom other)
        {
            return other.Mass.CompareTo(Mass);
        }
    }
}