using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : MonoBehaviour
{
    int[,] boardArray;
    int blackNum = 0;
    int whiteNum = 0;
    int blankNum = 0;
    int order = 0;
    Stack<int> stack = new Stack<int>();
    Stack<int> recordStack = new Stack<int>();
    // Start is called before the first frame update

    public CPU(int order)
    {
        this.order = order; 
        boardArray = new int[8, 8];
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[] place()
    {
        int targetIndex = CalculateMaxNum();
        int[] result;
        result = new int[2];
        result[0] = targetIndex/8;
        result[1] = targetIndex%8;
        return result;
    }

    void predict()
    {

    }


    int CalculateMaxNum()
    {
        int maxNum = 0;
        int maxIndex = 0;
        for (int i = 0; i < 64; i++)
        {
            clearData();
            if (boardArray[i / 8, i % 8] == 0)
            {
               
                calculateNum(i, order);
                int num = getStuckNum();
                clearData();

                if (num > maxNum)
                {
                    maxNum = num;
                    maxIndex = i;
                    
                }
            }
            
            
        }
        return maxIndex;
    }

    void clearData()
    {
        stack.Clear();
    }

    public void addStack(int index)
    {
        stack.Push(index);
    }

    public void updateArray(int[,] array)
    {
        boardArray = array;
    }

    void reCalculate()
    {
        blackNum = 0;
        whiteNum = 0;
        blankNum = 0;
        foreach (var panel in boardArray)
        {
            if (panel == 1)
            {
                blackNum += 1;

            }
            else if (panel == 2)
            {
                whiteNum += 1;
            }
            else if (panel == 0)
            {
                blankNum += 1;
            }
        }

    }

    void calculateNum(int column, int row, int self)
    {
        int target = 0;

        if (self == 1) { target = 2; }
        if (self == 2) { target = 1; }

        int targetColumn = column + 1;
        int targetRow = row;
        Stack<int> temporaryStack = new Stack<int>();
        //?E????
        while (targetColumn < 8 && targetRow < 8 && targetColumn > -1 && targetRow > -1)
        {

            if (boardArray[targetColumn, targetRow] == target)
            {
                temporaryStack.Push(targetColumn * 8 + targetRow);
            }
            else if (boardArray[targetColumn, targetRow] == 0)
            {
                break;
            }
            else if (boardArray[targetColumn, targetRow] == self)
            {
                while (temporaryStack.Count > 0)
                {
                    stack.Push(temporaryStack.Pop());
                }
                break;
            }
            targetColumn += 1;
        }

        //???
        targetColumn = column - 1;
        targetRow = row;
        temporaryStack.Clear();
        while (targetColumn < 8 && targetRow < 8 && targetColumn > -1 && targetRow > -1)
        {

            if (boardArray[targetColumn, targetRow] == target)
            {
                temporaryStack.Push(targetColumn * 8 + targetRow);
            }
            else if (boardArray[targetColumn, targetRow] == 0)
            {
                break;
            }
            else if (boardArray[targetColumn, targetRow] == self)
            {
                while (temporaryStack.Count > 0)
                {
                    stack.Push(temporaryStack.Pop());
                }
                break;
            }
            targetColumn -= 1;
        }
        //????
        targetColumn = column;
        targetRow = row - 1;
        temporaryStack.Clear();
        //  ??????
        while (targetColumn < 8 && targetRow < 8 && targetColumn > -1 && targetRow > -1)
        {

            if (boardArray[targetColumn, targetRow] == target)
            {
                temporaryStack.Push(targetColumn * 8 + targetRow);
            }
            else if (boardArray[targetColumn, targetRow] == 0)
            {
                break;
            }
            else if (boardArray[targetColumn, targetRow] == self)
            {
                while (temporaryStack.Count > 0)
                {
                    stack.Push(temporaryStack.Pop());
                }
                break;
            }
            targetRow -= 1;
        }


        //????????
        targetColumn = column;
        targetRow = row + 1;
        temporaryStack.Clear();
        while (targetColumn < 8 && targetRow < 8 && targetColumn > -1 && targetRow > -1)
        {

            if (boardArray[targetColumn, targetRow] == target)
            {
                temporaryStack.Push(targetColumn * 8 + targetRow);
            }
            else if (boardArray[targetColumn, targetRow] == 0)
            {
                break;
            }
            else if (boardArray[targetColumn, targetRow] == self)
            {
                while (temporaryStack.Count > 0)
                {
                    stack.Push(temporaryStack.Pop());
                }
                break;
            }
            targetRow += 1;
        }

        //?????????
        targetColumn = column - 1;
        targetRow = row - 1;
        temporaryStack.Clear();
        while (targetColumn < 8 && targetRow < 8 && targetColumn > -1 && targetRow > -1)
        {

            if (boardArray[targetColumn, targetRow] == target)
            {
                temporaryStack.Push(targetColumn * 8 + targetRow);
            }
            else if (boardArray[targetColumn, targetRow] == 0)
            {
                break;
            }
            else if (boardArray[targetColumn, targetRow] == self)
            {
                while (temporaryStack.Count > 0)
                {
                    stack.Push(temporaryStack.Pop());
                }
                break;
            }
            targetRow -= 1;
            targetColumn -= 1;
        }

        //????
        targetColumn = column + 1;
        targetRow = row + 1;
        temporaryStack.Clear();
        while (targetColumn < 8 && targetRow < 8 && targetColumn > -1 && targetRow > -1)
        {

            if (boardArray[targetColumn, targetRow] == target)
            {
                temporaryStack.Push(targetColumn * 8 + targetRow);
            }
            else if (boardArray[targetColumn, targetRow] == 0)
            {
                break;
            }
            else if (boardArray[targetColumn, targetRow] == self)
            {
                while (temporaryStack.Count > 0)
                {
                    stack.Push(temporaryStack.Pop());
                }
                break;
            }
            targetRow += 1;
            targetColumn += 1;
        }

        //????
        targetColumn = column + 1;
        targetRow = row - 1;
        temporaryStack.Clear();
        while (targetColumn < 8 && targetRow < 8 && targetColumn > -1 && targetRow > -1)
        {

            if (boardArray[targetColumn, targetRow] == target)
            {
                temporaryStack.Push(targetColumn * 8 + targetRow);
            }
            else if (boardArray[targetColumn, targetRow] == 0)
            {
                break;
            }
            else if (boardArray[targetColumn, targetRow] == self)
            {
                while (temporaryStack.Count > 0)
                {
                    stack.Push(temporaryStack.Pop());
                }
                break;
            }
            targetRow -= 1;
            targetColumn += 1;
        }

        // ??
        targetColumn = column - 1;
        targetRow = row + 1;
        temporaryStack.Clear();
        while (targetColumn < 8 && targetRow < 8 && targetColumn > -1 && targetRow > -1)
        {

            if (boardArray[targetColumn, targetRow] == target)
            {
                temporaryStack.Push(targetColumn * 8 + targetRow);
            }
            else if (boardArray[targetColumn, targetRow] == 0)
            {
                break;
            }
            else if (boardArray[targetColumn, targetRow] == self)
            {
                while (temporaryStack.Count > 0)
                {
                    stack.Push(temporaryStack.Pop());
                }
                break;
            }
            targetRow += 1;
            targetColumn -= 1;
        }
    }


    void calculateNum(int index,int self)
    {
        int column = index / 8;
        int row = index % 8;
        calculateNum(column, row, self);
       
    }

    int getStuckNum()
    {
        return stack.Count;
    }
}
