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
        if (m_bEnableLoop)
            networkHandler.StartPlaying();
        else
            networkHandler.StopPlaying();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ToggleLoopState();
    }

    public void OnButtonClick()
    {
        ToggleLoopState();
    }

    private void ToggleLoopState()
    {
        m_bEnableLoop = !m_bEnableLoop;
        if (m_bEnableLoop)
            networkHandler.StartPlaying();
        else
            networkHandler.StopPlaying();

        SimpleSpinner simpleSpinner = GetComponentsInChildren<SimpleSpinner>()[0];
        simpleSpinner.Rotation = m_bEnableLoop;
    }
}
