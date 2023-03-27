using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordButton : MonoBehaviour
{
    public NetworkHandler networkHandler;
    bool m_bRecord = false;

    // Start is called before the first frame update
    void Start()
    {

        SetRecordingMode(m_bRecord);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        m_bRecord = !m_bRecord;

        SetRecordingMode(m_bRecord);
    }

    private void SetRecordingMode(bool bRecord)
    {
        networkHandler.SetEditMode(bRecord);
        // Update icon to reflect state
        RecordIconToggle iconToggle = GetComponentsInChildren<RecordIconToggle>()[0];
        iconToggle.SetRecordMode(m_bRecord);
    }
}
