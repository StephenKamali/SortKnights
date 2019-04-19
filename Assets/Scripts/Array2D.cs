using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ColorAndValue
{
    public int count;
    public Color color;
    public int value;
}

public class Array2D : MonoBehaviour
{
    public SortObject spawnObject;
    public ColorAndValue[] objInfo;

    public int perRow;

    public float spread;

    private int size;
    private int rows;
    private int columns;

    SortObject[] objectArray;

    // Start is called before the first frame update
    void Awake()
    {
        size = 0;
        for (int i = 0; i < objInfo.Length; i++)
        {
            size += objInfo[i].count;
        }
        objectArray = new SortObject[size];

        /*
        int currIndex = 0;
        int currCount = objInfo[currIndex].count;
        for (int i = 0; i < size; i++)
        {
            SortObject created = Instantiate(spawnObject, transform.position + (Vector3.left * spread * (i % perRow)) + (Vector3.forward * spread * (i / perRow)), Quaternion.identity);

            objectArray[i] = created;
            created.SetColor(objInfo[currIndex].color);
            created.Value = objInfo[currIndex].value;
            if ((--currCount) == 0)
            {
                currIndex++;
                if (currIndex < objInfo.Length)
                    currCount = objInfo[currIndex].count;
            }
        }
        */

        columns = perRow;
        rows = size / perRow;

        int currIndex = 0;
        int currCount = objInfo[0].count;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                SortObject created = Instantiate(spawnObject,
                    transform.position + (Vector3.left * spread * col) + (Vector3.forward * spread * row),
                    Quaternion.identity);

                objectArray[row * columns + col] = created;
                created.SetColor(objInfo[currIndex].color);
                created.Value = objInfo[currIndex].value;
                if ((--currCount) == 0)
                {
                    currIndex++;
                    if (currIndex < objInfo.Length)
                        currCount = objInfo[currIndex].count;
                }
            }
        }
    }

    public void InstantSwap(int i, int j)
    {
        SortObject temp = objectArray[i];
        Vector3 tempPos = temp.transform.position;
        temp.transform.position = objectArray[j].transform.position;
        objectArray[j].transform.position = tempPos;
        objectArray[i] = objectArray[j];
        objectArray[j] = temp;
    }

    public void MoveSwap(int i, int j)
    {
        SortObject temp = objectArray[i];
        temp.MoveTo(objectArray[j].transform.position);
        objectArray[j].MoveTo(temp.transform.position);
        objectArray[i] = objectArray[j];
        objectArray[j] = temp;
    }

    public void Randomize(int quality)
    {
        int one, two;
        for (int i = 0; i < quality; i++)
        {
            one = Random.Range(0, size);
            two = Random.Range(0, size);
            if (one != two)
                InstantSwap(one, two);
        }
    }

    public int GetIndex(int row, int col)
    {
        return row * columns + col;
    }

    public SortObject GetElement(int i)
    {
        return objectArray[i];
    }

    public int Length
    {
        get { return size; }
    }
}
