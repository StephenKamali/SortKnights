using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Order of sorts in enum should be same as order in dropdown list, for now
public enum Sort { BubbleSort, InsertionSort, MergeSort, QuickSort };

public class DemoManager : MonoBehaviour
{
    public Array2D array;

    // Algorithm choice
    public UnityEngine.UI.Text sortTitleText;
    public UnityEngine.UI.Dropdown algorithmDropdown;

    // Stats
    public UnityEngine.UI.Text comparisons;
    public UnityEngine.UI.Text arraySwaps;
    public UnityEngine.UI.Text arrayAccesses;

    // Start/stop and randomize
    public UnityEngine.UI.Button play;
    public UnityEngine.UI.Button randomize;

    // Speed
    public UnityEngine.UI.Slider speedSlider;
    public UnityEngine.UI.Text speedText;

    private bool isPlaying;

    private delegate IEnumerator Algorithm(Array2D arr);
    private Algorithm selectedAlgorithm;
    private Coroutine currAlgorithm;

    // Start is called before the first frame update
    void Start()
    {
        selectedAlgorithm = new Algorithm(SortingAlgorithms.BubbleSort);
    }

    void Update()
    {
        if (!SortingAlgorithms.isFinished)
        {
            //TODO, update text in a more efficient way than every frame
            comparisons.text = "Comparisons    " + SortingAlgorithms.comparisons;
            arraySwaps.text = "Array Swaps    " + SortingAlgorithms.arraySwaps;
            arrayAccesses.text = "Array Accesses " + SortingAlgorithms.arrayAccesses;
        }
        else if (isPlaying)
        {
            StartStop();
        }
    }

    public void StartStop()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            randomize.interactable = false;
            algorithmDropdown.interactable = false;
            play.GetComponentInChildren<UnityEngine.UI.Text>().text = "Stop";
            currAlgorithm = StartCoroutine(selectedAlgorithm(array));
        }
        else
        {
            isPlaying = false;
            randomize.interactable = true;
            algorithmDropdown.interactable = true;
            play.GetComponentInChildren<UnityEngine.UI.Text>().text = "Start";
            StopCoroutine(currAlgorithm);
        }
    }

    public void Randomize()
    {
        array.Randomize(100);
    }

    public void ChangeSort()
    {
        switch ((Sort)algorithmDropdown.value)
        {
            case Sort.BubbleSort:
                selectedAlgorithm = new Algorithm(SortingAlgorithms.BubbleSort);
                sortTitleText.text = "Bubble Sort";
                break;

            case Sort.InsertionSort:
                selectedAlgorithm = new Algorithm(SortingAlgorithms.InsertionSort);
                sortTitleText.text = "Insertion Sort";
                break;

            default:
                Debug.LogError("Unknown sort");
                break;
        }
        comparisons.text = "Comparisons    " + 0;
        arraySwaps.text = "Array Swaps    " + 0;
        arrayAccesses.text = "Array Accesses " + 0;
    }

    public void ChangeSpeed()
    {
        float speed = speedSlider.value * .25f;
        speedText.text = "Speed " + speed.ToString("N2") + "x";
        SortObject.SetTimeSpeed(speed);
    }

    public void ChangeCompareDelay()
    {
        //TODO - allow delay to be changed(or disabled completely)
    }
}
