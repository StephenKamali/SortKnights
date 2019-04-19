using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SortingAlgorithms
{
    public static int comparisons;
    public static int arraySwaps;

    // In MoveSwap, arrayAccesses only counts accesses not used for movement
    // An access counts as reading a value from the array
    public static int arrayAccesses;

    public static IEnumerator BubbleSort(Array2D arr)
    {
        comparisons = 0;
        arraySwaps = 0;
        arrayAccesses = 0;

        int upperBound = arr.Length;
        for (int i = 0; i < arr.Length; i++)
        {
            upperBound--;
            for (int j = 0; j < upperBound; j++)
            {
                comparisons++;
                arrayAccesses += 2;
                if (arr.GetElement(j).Value > arr.GetElement(j + 1).Value)
                {
                    arraySwaps++;
                    arrayAccesses += 2;
                    arr.MoveSwap(j, j + 1);
                    yield return new WaitWhile(() => arr.GetElement(j).IsMoving);
                }
            }
        }
    }
}
