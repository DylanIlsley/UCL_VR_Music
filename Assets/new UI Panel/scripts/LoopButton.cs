using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleSpinner;
using TMPro;

public class LoopButton : MonoBehaviour
{
    public NetworkHandler networkHandler;
    private TextMeshProUGUI m_txtCounter;
    bool m_bEnableLoop = false;


    // Start is called before the first frame update
    void Start()
    {
        //SetColorsFromEditMode(m_bEdit);
        SimpleSpinner simpleSpinner = GetComponentsInChildren<SimpleSpinner>()[0];
        m_txtCounter = GetComponentsInChildren<TextMeshProUGUI>()[0];
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

        if(m_bEnableLoop)
        {
            m_txtCounter.text = networkHandler.GetCurrentLoopTime().ToString();
        }
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
