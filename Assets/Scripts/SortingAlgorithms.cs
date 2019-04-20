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

        int upperBound = arr.Length - 1;
        int newUpper;
        for (int i = 0; i < arr.Length; i++)
        {
            //upperBound--;
            newUpper = 0;
            for (int j = 0; j < upperBound; j++)
            {
                comparisons++;
                arrayAccesses += 2;
                if (arr.GetElement(j).Value > arr.GetElement(j + 1).Value)
                {
                    arraySwaps++;
                    arrayAccesses += 2;
                    arr.MoveSwap(j, j + 1);
                    newUpper = j;
                    yield return new WaitWhile(() => arr.GetElement(j).IsMoving);
                }
                //TODO, else, pause here w/ the markers showing above the avatars' heads
            }
            upperBound = newUpper;
        }
    }

    public static IEnumerator InsertionSort(Array2D arr)
    {
        comparisons = 0;
        arraySwaps = 0;
        arrayAccesses = 0;

        for (int i = 1; i < arr.Length; i++)
        {
            for (int j = i; (j > 0) && (arr.GetElement(j - 1).Value > arr.GetElement(j).Value); j--)
            {
                comparisons++;
                arrayAccesses += 2;
                arraySwaps++;
                arr.MoveSwap(j - 1, j);
                yield return new WaitWhile(() => arr.GetElement(j).IsMoving);
            }
            //TODO, this will overcount in the case where j > 0 caused the loop to break
            comparisons++;
            arrayAccesses += 2;
        }
    }

    public static IEnumerator MergeSort(Array2D arr)
    {
        yield return null;
    }
}
