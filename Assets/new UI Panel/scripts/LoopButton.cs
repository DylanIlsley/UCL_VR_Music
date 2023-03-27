using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleSpinner;

public class LoopButton : MonoBehaviour
{
    public NetworkHandler networkHandler;
    
    bool m_bEnableLoop = false;


    // Start is called before the first frame update
    void Start()
    {
        //SetColorsFromEditMode(m_bEdit);
        SimpleSpinner simpleSpinner = GetComponentsInChildren<SimpleSpinner>()[0];
        simpleSpinner.Rotation = m_bEnableLoop;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnButtonClick()
    {
        
        m_bEnableLoop = !m_bEnableLoop;
        if(m_bEnableLoop)
            networkHandler.StartPlaying();
        else
            networkHandler.StopPlaying();

        SimpleSpinner simpleSpinner = GetComponentsInChildren<SimpleSpinner>()[0];
        simpleSpinner.Rotation = m_bEnableLoop;
    }
}
