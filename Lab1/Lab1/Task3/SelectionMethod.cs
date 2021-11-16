using AForge.Genetic;
using System.Collections.Generic;

namespace Lab1
{
    class SelectionMethod : ISelectionMethod
    {
        public void ApplySelection(List<IChromosome> chromosomes, int size)
        {
            chromosomes.Sort();
            //chromosomes.Reverse();
            chromosomes.RemoveRange(size, chromosomes.Count - size);
        }
    }
}
