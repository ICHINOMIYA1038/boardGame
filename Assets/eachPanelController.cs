using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eachPanelController : MonoBehaviour
{

    int row;
    int column;
    [SerializeField] int status = 0;
    bool canPlace = false;
    int changeableNum;

    [SerializeField] GameObject whitePanel;
    [SerializeField] GameObject blackPanel;
    [SerializeField] GameObject canPlacePanel;
    Button btn;
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
        status = index;
        //?u????????????
        if (index == 0)
        {
            whitePanel.SetActive(false);
            blackPanel.SetActive(false);
            canPlacePanel.SetActive(false);
        }
        //?F?????F
        if(index == 1)
        {
            whitePanel.SetActive(false);
            blackPanel.SetActive(true);
            canPlacePanel.SetActive(false);
        }
        //?F?????F
        if (index == 2)
        {
            whitePanel.SetActive(true);
            blackPanel.SetActive(false);
            canPlacePanel.SetActive(false);
        }
        if(index == 3)
        {
            whitePanel.SetActive(false);
            blackPanel.SetActive(false);
            canPlacePanel.SetActive(true);
        }
    }

    public int getStatus()
    {
        return status;
    }
}
