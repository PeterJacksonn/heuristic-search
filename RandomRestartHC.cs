using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_6
{
    class RandomRestartHC
    {
        
        public static void runRandomRestart(int RMHCIterations, int restarts, List<double> data, int numOfLorries, string allFitnessFile) 
        {

            List<int[]> allSolutions= new List<int[]>();
            List<double> allFitness = new List<double>();

            Console.WriteLine("Experiments starting...");


            for(int z = 0; z < restarts; z++)
            {

                Console.WriteLine("Restart #"+ (z+1));
                
                // Initialise random solution for RMHC
                RandomMutationHC sol = new RandomMutationHC(data, numOfLorries);

                Console.WriteLine("Initial fitness: " + sol.fitness);
                Console.WriteLine("Initial solution: ");
                sol.printSolution();

                for (int i = 0; i < RMHCIterations; i++)
                {

                    RandomMutationHC newSol = new RandomMutationHC(data, numOfLorries);
                    newSol.copySolution();
                    newSol.smallChange();


                    if (newSol.fitness < sol.fitness)
                    {
                        sol = newSol.copySolution();
                    }

                }
                allSolutions.Add(sol.solution);
                allFitness.Add(sol.fitness);
            }


            Console.WriteLine("#### END OF EXPERIMENTS ####\n\n");

            // find best solution
            int bestFitIndex = allFitness.IndexOf(allFitness.Min());
            int[] bestSolution = allSolutions[bestFitIndex];

            Console.WriteLine("Best solution: ");
            foreach(int i in bestSolution)
            {
                Console.Write(i);
            }
            Console.WriteLine("\nWith fitness: " + allFitness[bestFitIndex]);


            List<List<double>> finalLorries = translateToLorries(bestSolution,data,  numOfLorries);

            Console.WriteLine("\nWeights loaded on each lorry:");
            for(int i = 0; i < finalLorries.Count;i++)
            {
                Console.Write($"Lorry {i}: ");
                foreach (double value in finalLorries[i])
                {
                    Console.Write(value + ", ");
                }
                Console.WriteLine("\n");
            }

            // write all fitness values to file
            ManageDataset.writeToFitnessFile(allFitness, allFitnessFile);

        }

        public static List<List<double>> translateToLorries(int[] solution, List<double> data, int truckAmount)
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
