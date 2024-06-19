using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_6
{
    internal class ManageDataset
    {

        public static List<double> readDataset(string fileName)
        {
            List<double> dataset = new List<double>();
            var lines = System.IO.File.ReadLines(fileName).ToList();
            dataset = lines.Select(double.Parse).ToList();
            return dataset;
        }

        public static void writeToFitnessFile(List<double> allFitnes, string allFitnessFile)
        {

            using (StreamWriter writetext = new StreamWriter(allFitnessFile))
            {
                foreach (var fitness in allFitnes)
                {
                    writetext.WriteLine(fitness);
                }
            }

        }


    }
}
