using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project_6
{
    internal class RandomMutationHC
    {
        public int[] solution; // solution stored as each group of bricks mapped to either truck 0, 1, 2 (A,B,C)

        public double fitness;

        public List<double> data = new List<double>();

        public int truckAmount;

        // RMHC Constructor for initial solution
        public RandomMutationHC(List<double> dataset, int L)
        {
            truckAmount = L;
            data = dataset;
            solution = new int[data.Count];
            randomStart();
            calCurrentFit();
        }


        public RandomMutationHC copySolution()
        {
            RandomMutationHC res = (RandomMutationHC)this.MemberwiseClone();
            res.solution = new int[solution.Length];
            // Copy the contents of the solution array to the new array
            for (int i = 0; i < solution.Length; i++)
            {
                res.solution[i] = solution[i];
            }
            res.fitness = fitness;
            return res;

        }


        private void randomStart()
        {
            // make a random start and return the solution
            
            Random rand = new Random();

            for (int t = truckAmount - 1; t>=0 ; t--)
            {
                int i = 0;
                while (i < data.Count() / truckAmount)
                {
                    int randIndex = rand.Next(0, data.Count());
                    if (solution[randIndex] == 0)
                    {
                        solution[randIndex] = t;
                        i++;
                    }
                }
            }
        }

        private double getFitness()
        {
            calCurrentFit();
            return fitness;
        }


        public void printSolution()
        {
            foreach(int x in solution)
            {
                Console.Write(x);
            }
            Console.WriteLine("\n");
        }

        public int[] getSolution()
        {
            return solution;
        }

        private void calCurrentFit()
        {
            double[] totalWeights = findTotalWeights();

            fitness = totalWeights.Max() - totalWeights.Min();
        }


        public double[] findTotalWeights()
        {
            double[] truckTotalWeights = new double[truckAmount];

            for (int t = 0; t < truckAmount; t++)
            {
                for (int i = 0; i < solution.Length; i++)
                {
                    if (solution[i] == t)
                    {
                        truckTotalWeights[t] += data[i];
                    }
                }
                
            }
            return truckTotalWeights;
        }


        public void smallChange()
        {
            double[] totalWeights = findTotalWeights();

            Random rand = new Random();

            int maxIndex = Array.IndexOf(totalWeights, totalWeights.Max());
            int minIndex = Array.IndexOf(totalWeights, totalWeights.Min());

            bool changed = false;
            while(!changed)
            {
                int randIndex = rand.Next(0, solution.Length);
                if (solution[randIndex] == maxIndex)
                {
                    solution[randIndex] = minIndex;
                    changed = true;
                }
            }
            calCurrentFit();
        }

        public List<List<double>> translateToLorries()
        {
            List<List<double>> lorries = new List<List<double>>();


            for (int t = 0; t < truckAmount; t++)
            {
                List<double> temp = new List<double>();

                for (int i = 0; i < solution.Length; i++)
                {
                    if (solution[i] == t)
                    {
                        temp.Add(data[i]);
                    }

                }
                lorries.Add(temp);
            }
            return lorries;
        } 

    }
}
