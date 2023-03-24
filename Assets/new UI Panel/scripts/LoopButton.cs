using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleSpinner;

public class LoopButton : MonoBehaviour
{
    public NetworkHandler networkHandler;
    bool m_bEdit = false;


    // Start is called before the first frame update
    void Start()
    {
        //SetColorsFromEditMode(m_bEdit);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnButtonClick()
    {
        networkHandler.SetEditMode(m_bEdit);
        m_bEdit = !m_bEdit;
        SimpleSpinner simpleSpinner = GetComponentsInChildren<SimpleSpinner>()[0];
        simpleSpinner.Rotation = !m_bEdit;
    }
}
