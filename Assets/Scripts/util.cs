using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace util
{
    public class util : MonoBehaviour
    {

           

        public void checkPlacePosi(ref bool[] canPlaceArray, int[,] boardArray,int order)
        {
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < 64; i++)
            {
                if (boardArray[i / 8, i % 8] == 0)
                {
                    calculateNum(boardArray,i / 8, i % 8, order,stack);
                    if (stack.Count == 0)
                    {
                        canPlaceArray[i] = false;
                    }
                    else
                    {
                        canPlaceArray[i] = true;
                    }
                    stack.Clear();
                }
                else if (boardArray[i / 8, i % 8] != 0)
                {
                    canPlaceArray[i] = false;
                }
            }
        }

        public void calculateNum(int[,]boardArray,int column, int row, int self, Stack<int>stack)
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
    }


}