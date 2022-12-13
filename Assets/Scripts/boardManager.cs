using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class boardManager : MonoBehaviour
{
    GameObject[] eachPanels;
    eachPanelController[] panelControllers;
    int pOrder = 1;
    int order;
    int[,] boardArray;
    Button[] btns;
    Stack<int> stack = new Stack<int>();
    [SerializeField]TextMeshProUGUI blackText;
    [SerializeField]TextMeshProUGUI whiteText;
    [SerializeField] TextMeshProUGUI msg;
    [SerializeField] TextMeshProUGUI endMsg;
    [SerializeField]bool[] canPlace;
    [SerializeField] int GameMode;
    [SerializeField] GameObject basePanel;
    CPU cpu;


    // Start is called before the first frame update
    void Start()
    {
        order = 1;
        eachPanels = new GameObject[64];
        btns = new Button[64];
        eachPanels = GameObject.FindGameObjectsWithTag("eachPanel");
        panelControllers = new eachPanelController[64];
        canPlace = new bool[64];
        for (int i = 0; i < eachPanels.Length; i++)
        {
            eachPanels[i] = basePanel.transform.GetChild(i).gameObject;
        }


            for (int i = 0; i < eachPanels.Length; i++)
        {
            panelControllers[i] = eachPanels[i].GetComponent<eachPanelController>();
            btns[i] = eachPanels[i].GetComponentInChildren<Button>();
            //????????????????????????????????
            int tmp = i;
            btns[i].onClick.AddListener( () => { StartCoroutine(place(tmp / 8, tmp % 8, order)); });
            
        }
        boardArray = new int[8,8];
       
        changePanel(3, 3, 2);
        changePanel(3, 4, 1);
        changePanel(4, 3, 1);
        changePanel(4, 4, 2);
        predictNum();
        enableBtn();
        placeAssist();
        if (pOrder == 1)
        {
            cpu = new CPU(2);
        }
        else if (pOrder == 2)
        {
            cpu = new CPU(1);
        }
        /*
        for(int i=0; i<64; i++)
        {
            place(i/8,i%8,1);
        }
        */



    }

    public void enableBtn()
    {
        for (int i = 0; i < 64; i++)
        {
            if (canPlace[i] == true)
            {
                btns[i].enabled = true;
            }
            else if (canPlace[i] == false)
            {
                btns[i].enabled = false;
            }
        }
    }

    public void unableBtn()
    {
        for (int i = 0; i < 64; i++)
        {

                btns[i].enabled = false;

        }
    }

    public void predictNum()
    {
        for(int i=0;i<64;i++)
        {
            if (boardArray[i/8,i%8] == 0)
            {
                calculateNum(i/8,i%8,order);
                if (stack.Count == 0)
                {
                    canPlace[i] = false;
                }
                else
                {
                    canPlace[i] = true;
                }
                stack.Clear();
            }
            else if (boardArray[i / 8, i % 8] != 0)
            {
                canPlace[i] = false;
            }
        }
    }




    void changePanel(int column ,int row,int type)
    {
        boardArray[column, row] = type;
        panelControllers[column * 8 + row].changeColor(type);
    }

    IEnumerator place(int column, int row, int type)
    {
        cpu.addStack(column * 8 + row);
        boardArray[column, row] = type;
        panelControllers[column * 8 + row].Place(type);
        unableBtn();
        placeAssistInvisible();
        int target=0;
        if(type == 1){ target = 2; }
        if (type == 2) { target = 1; }
        yield return new WaitForSeconds(0.2f);
        calculateNum(column, row, type);
        flip(type);
        clearData();
        orderChange();
        calculate();
        predictNum();
        
        skipCheck();
        if(endMsg.enabled == true)
        {
            yield break;
        }
        else if (GameMode == 1&&order!=pOrder)
        {
            Debug.Log("AITurn");
            cpu.updateArray(boardArray);
            int[] position;
            position = new int[2];
            position = cpu.place();
            StartCoroutine(AIPlace(position[0], position[1], order));
        }
        else
        {
            placeAssist();
            enableBtn();
        }
    }

    IEnumerator AIPlace(int row ,int column, int order)
    {
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(place(row, column, order));
    }

    void flip(int index)
    {
        while(stack.Count > 0)
        {
            int changedNum = stack.Pop();
            panelControllers[changedNum].changeColor(index);
            boardArray[changedNum/8,changedNum%8] = index;
        }
        
    }


    void skipCheck()
    {
        foreach (var elem in canPlace)
        {
            if (elem == true) { return; };
        }
        Debug.Log("Nowhere can you place");
        orderChange();
        calculate();
        predictNum();
        enableBtn();
        placeAssist();
        foreach (var elem in canPlace)
        {
            if (elem == true) { return; };
        }
        gameEnd();

    }

    void gameEnd()
    {
        endMsg.enabled = true;
        int blackNum = 0;
        int whiteNum = 0;
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
        }
        if (blackNum > whiteNum)
        {
            endMsg.SetText($"Winner: Black");
        }
        else if (blackNum < whiteNum)
        {
            endMsg.SetText($"Winner: White");
        }
        else if(blackNum == whiteNum)
        {
            endMsg.text = $"Draw";
        }
    }

    void calculate()
    {
        int blackNum = 0;
        int whiteNum = 0;
        int blankNum = 0;
        foreach (var panel in boardArray)
        {
            if (panel == 1)
            {
                blackNum += 1;
          
            }
            else if(panel == 2)
            {
                whiteNum += 1;
            }
            else if(panel == 0)
            {
                blankNum += 1;
            }
        }
        blackText.text = $"Black:{blackNum}";
        whiteText.text = $"White:{whiteNum}";
    }

    void orderChange()
    {
        if(order == 1)
        {
            order = 2;
        }
        else if(order == 2)
        {
            order = 1;
        }
        if (order == 1)
        {
            msg.text = $"Black Turn";
            msg.color = new Vector4(0f, 0f, 0f, 1f);
        }
        if (order == 2)
        {
            msg.text = $"White Turn";
            msg.color = new Vector4(1f, 1f, 1f, 1f);
        }

    }

    void placeAssist()
    {
        for(int i = 0; i < 64; i++)
        {
            int target = panelControllers[i].getStatus();
            if (target == 0 || target == 3)
            {
                if (canPlace[i])
                {
                    panelControllers[i].changeColor(3);
                }
                else if (!canPlace[i])
                {
                    panelControllers[i].changeColor(0);
                }

            }
        }
    }

    void placeAssistInvisible()
    {
        for (int i = 0; i < 64; i++)
        {
            int target = panelControllers[i].getStatus();
            if (target == 0 || target == 3)
            {

                    panelControllers[i].changeColor(0);
                

            }
        }
    }

    void clearData()
    {
        stack.Clear();
    }
    void calculateNum(int column, int row,int self)
    {
        int target = 0;
        
        if (self == 1) { target = 2; }
        if (self == 2) { target = 1; }

        int targetColumn = column+1;
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
        targetRow = row-1;
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
        targetRow = row+1;
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
        targetColumn = column-1;
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
            targetColumn  -= 1;
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

    IEnumerator waitTimer(float time)
    {
        unableBtn();
        yield return new WaitForSeconds(time);
        enableBtn();
    }
    
}
