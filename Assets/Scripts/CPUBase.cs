using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CPUBase : MonoBehaviour
{
    int[,] boardArray;


    public void updateArray(int[,] array)
    {
        boardArray = array;
    }

    public int[] place()
    {
        int targetIndex = 0;
        int[] result;
        result = new int[2];
        result[0] = targetIndex / 8;
        result[1] = targetIndex % 8;
        return result;
    }
}