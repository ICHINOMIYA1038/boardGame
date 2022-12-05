using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eachPanelController : MonoBehaviour
{
    [SerializeField] int status = 0;
    bool canPlace = false;
    int changeableNum;

    [SerializeField] GameObject whitePanel;
    [SerializeField] GameObject blackPanel;
    [SerializeField] GameObject canPlacePanel;
    // Start is called before the first frame update
    void Awake()
    {
        changeColor(status);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColor(int index)
    {
        //置かれていない
        if (index == 0)
        {
            whitePanel.SetActive(false);
            blackPanel.SetActive(false);
            canPlacePanel.SetActive(false);
        }
        //色が黒色
        if(index == 1)
        {
            whitePanel.SetActive(true);
            blackPanel.SetActive(false);
            canPlacePanel.SetActive(false);
        }
        //色が白色
        if (index == 2)
        {
            whitePanel.SetActive(false);
            blackPanel.SetActive(true);
            canPlacePanel.SetActive(false);
        }
        if(index == 3)
        {
            whitePanel.SetActive(false);
            blackPanel.SetActive(false);
            canPlacePanel.SetActive(true);
        }
    }
}
