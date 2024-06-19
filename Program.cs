
namespace Project_6
{


    class Program
    {
        static void Main(string[] args)
        {
            // CHANGE TO CORRECT FILE PATH
            string dataFile = "FILEPATH TO... ->  \\dataset1.txt";
            string allFitnessFile = "FILEPATH TO... -> \\allFitness.txt";

            List<double> data = ManageDataset.readDataset(dataFile);

            int numOfLorries = 3;

            int RRHCRestarts = 100;
            int RMHCIterations = 10000;

            RandomRestartHC.runRandomRestart(RMHCIterations, RRHCRestarts, data, numOfLorries, allFitnessFile);

        }
    }
}
