using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    GameObject[] eachPanel;
    [SerializeField] GameObject panelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform[] panelPosition = new RectTransform[64];
        eachPanel = new GameObject[64];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                eachPanel[i * 8 + j] = Instantiate(panelPrefab);
                eachPanel[i * 8 + j].transform.SetParent(this.gameObject.transform,true);
                panelPosition[i * 8 + j] = eachPanel[i * 8 + j].GetComponent<RectTransform>();
                panelPosition[i * 8 + j].localPosition = new Vector3(-100f + 100f * i, -100f - 100f * j, 0f);
            }


        }
    }

    private void OnGUI()
    {
        


        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
