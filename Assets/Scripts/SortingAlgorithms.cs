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

    private static float compareDelay = 0.5f;

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

                if (compareDelay > 0)
                {
                    arr.GetElement(j).SetMarkerActive(true);
                    arr.GetElement(j + 1).SetMarkerActive(true);
                    yield return new WaitForSeconds(compareDelay / SortObject.GetTimeSpeed());
                    arr.GetElement(j).SetMarkerActive(false);
                    arr.GetElement(j + 1).SetMarkerActive(false);
                }

                if (arr.GetElement(j).Value > arr.GetElement(j + 1).Value)
                {
                    arraySwaps++;
                    arrayAccesses += 2;
                    arr.MoveSwap(j, j + 1);
                    newUpper = j;
                    yield return new WaitWhile(() => arr.GetElement(j).IsMoving);
                }
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
            for (int j = i; j > 0; j--)
            {
                comparisons++;
                arrayAccesses += 2;

                if (compareDelay > 0)
                {
                    arr.GetElement(j - 1).SetMarkerActive(true);
                    arr.GetElement(j).SetMarkerActive(true);
                    yield return new WaitForSeconds(compareDelay / SortObject.GetTimeSpeed());
                    arr.GetElement(j - 1).SetMarkerActive(false);
                    arr.GetElement(j).SetMarkerActive(false);
                }

                if (arr.GetElement(j - 1).Value > arr.GetElement(j).Value)
                {
                    arraySwaps++;
                    arr.MoveSwap(j - 1, j);
                    yield return new WaitWhile(() => arr.GetElement(j).IsMoving);
                }
                else
                {
                    //TODO, is there a cleaner way to do this without a break?
                    break;
                }
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
