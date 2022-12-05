using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardManager : MonoBehaviour
{
    GameObject[] eachPanels;
    eachPanelController[] panelControllers;
    int[,] boardArray;
    Stack<int> stack = new Stack<int>();
    // Start is called before the first frame update
    void Start()
    { 
        eachPanels = new GameObject[64];
        eachPanels = GameObject.FindGameObjectsWithTag("eachPanel");
        panelControllers = new eachPanelController[64];
        
        
        for(int i = 0; i < eachPanels.Length; i++)
        {
            panelControllers[i] = eachPanels[i].GetComponent<eachPanelController>();
        }
        boardArray = new int[8,8];
        place(3, 3, 2);
        place(3, 4, 2);
        place(4, 3, 1);
        place(4, 4, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void place(int row,int column, int type)
    {
        Debug.Log("place");
        boardArray[row,column] = type;
        panelControllers[row * 8 + column].changeColor(type);
        int target=0;
        if(type == 1){ target = 2; }
        if (type == 2) { target = 1; }
        calculateNum(row, column, target);
        flip(type);
        clearData();
    }

    void flip(int index)
    {
        while(stack.Count > 0)
        {
            panelControllers[stack.Pop()].changeColor(index);
        }
        
    }

    void clearData()
    {
        stack.Clear();
    }
    void calculateNum(int row,int column,int target)
    {
        int type = target;
        int targetColumn = column;
        int targetRow = row;
        //‰E•ûŒü
        do
        {
            targetColumn += 1;
            if (boardArray[targetRow, targetColumn] == type)
            {
                stack.Push(targetRow * 8 + targetColumn);
            }
            else
            {
                break;
            }
        }
        while (targetColumn < 8 && targetRow < 8);

        //  ¶•ûŒü
        do
        {
            targetColumn -= 1;
            if (boardArray[targetRow, targetColumn] == type)
            {
                stack.Push(targetRow * 8 + targetColumn);
            }
            else
            {
                break;
            }
        }
        while (targetColumn < 8 && targetRow < 8);

        //  ã•ûŒü
        do
        {
            targetRow -= 1;
            if (boardArray[targetRow, targetColumn] == type)
            {
                stack.Push(targetRow * 8 + targetColumn);
            }
            else
            {
                break;
            }
        }
        while (targetColumn < 8 && targetRow < 8);

        //  ‰º•ûŒü
        do
        {
            targetRow += 1;
            if (boardArray[targetRow, targetColumn] == type)
            {
                stack.Push(targetRow * 8 + targetColumn);
            }
            else
            {
                break;
            }
        }
        while (targetColumn < 8 && targetRow < 8);
    }

    
}
