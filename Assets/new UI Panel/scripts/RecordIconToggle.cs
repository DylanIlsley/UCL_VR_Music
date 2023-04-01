using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordIconToggle : MonoBehaviour
{
    public List<Texture> m_ImageList;
    private RawImage m_Image;

    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<RawImage>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRecordMode(bool bEnable)
    {
        if (bEnable)
            m_Image.texture = m_ImageList[0];
        else
            m_Image.texture = m_ImageList[1];
    }
}
