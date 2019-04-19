using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManager : MonoBehaviour
{
    public Array2D array;
    public UnityEngine.UI.Text comparisons;
    public UnityEngine.UI.Text arraySwaps;
    public UnityEngine.UI.Text arrayAccesses;

    // Start is called before the first frame update
    void Start()
    {
        array.Randomize(100);
        StartCoroutine(SortingAlgorithms.BubbleSort(array));
    }

    void Update()
    {
        comparisons.text =   "Comparisons    " + SortingAlgorithms.comparisons;
        arraySwaps.text =    "Array Swaps    " + SortingAlgorithms.arraySwaps;
        arrayAccesses.text = "Array Accesses " + SortingAlgorithms.arrayAccesses;
    }
}
