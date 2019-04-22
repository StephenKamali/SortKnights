﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sort { BubbleSort, SelectionSort, MergeSort, QuickSort };

public class DemoManager : MonoBehaviour
{
    public Array2D array;
    
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

    private Coroutine currAlgorithm;

    // Start is called before the first frame update
    void Start()
    {
        //array.Randomize(100);
        //StartCoroutine(SortingAlgorithms.BubbleSort(array));
    }

    void Update()
    {
        comparisons.text =   "Comparisons    " + SortingAlgorithms.comparisons;
        arraySwaps.text =    "Array Swaps    " + SortingAlgorithms.arraySwaps;
        arrayAccesses.text = "Array Accesses " + SortingAlgorithms.arrayAccesses;
    }

    public void StartStop()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            randomize.interactable = false;
            play.GetComponentInChildren<UnityEngine.UI.Text>().text = "Stop";
            currAlgorithm = StartCoroutine(SortingAlgorithms.BubbleSort(array));
        }
        else
        {
            isPlaying = false;
            randomize.interactable = true;
            play.GetComponentInChildren<UnityEngine.UI.Text>().text = "Start";
            StopCoroutine(currAlgorithm);
        }
    }

    public void Randomize()
    {
        array.Randomize(100);
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
