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
            StartCoroutine(Flip(blackPanel));
        }
        //?F?????F
        if (index == 2)
        {
            whitePanel.SetActive(true);
            blackPanel.SetActive(false);
            canPlacePanel.SetActive(false);
            StartCoroutine(Flip(whitePanel));
        }
        if(index == 3)
        {
            whitePanel.SetActive(false);
            blackPanel.SetActive(false);
            canPlacePanel.SetActive(true);
        }
    }

    public void Place(int index)
    {
        changeColor(index);
        if (index == 1)
        {
            StartCoroutine(ColorTransform(blackPanel));
        }
        if (index == 2)
        {
            StartCoroutine(ColorTransform(whitePanel));
         }
    }

    IEnumerator ColorTransform(GameObject panel)
    {
        for(int i=0;i<30;i++)
        {
            RectTransform targetTransform = panel.GetComponent<RectTransform>();
            float width = targetTransform.sizeDelta.x + 0.4f;
            float height = targetTransform.sizeDelta.y + 0.4f;
            targetTransform.sizeDelta = new Vector2(width, height);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 30; i++)
        {
            RectTransform targetTransform = panel.GetComponent<RectTransform>();
            float width = targetTransform.sizeDelta.x - 0.4f;
            float height = targetTransform.sizeDelta.y - 0.4f;
            targetTransform.sizeDelta = new Vector2(width, height);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Flip(GameObject panel)
    {
        for (int i = 0; i < 30; i++)
        {
            RectTransform targetTransform = panel.GetComponent<RectTransform>();
            float width = targetTransform.sizeDelta.x + 0.2f;
            float height = targetTransform.sizeDelta.y + 0.2f;
            targetTransform.sizeDelta = new Vector2(width, height);
            yield return new WaitForSeconds(0.005f);
        }
        for (int i = 0; i < 30; i++)
        {
            RectTransform targetTransform = panel.GetComponent<RectTransform>();
            float width = targetTransform.sizeDelta.x - 0.2f;
            float height = targetTransform.sizeDelta.y - 0.2f;
            targetTransform.sizeDelta = new Vector2(width, height);
            yield return new WaitForSeconds(0.005f);
        }
    }

    public int getStatus()
    {
        return status;
    }
}
