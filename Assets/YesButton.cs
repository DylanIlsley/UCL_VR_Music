using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesButton : MonoBehaviour
{
    public NetworkHandler networkHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnButtonClick()
    {
        if(!networkHandler)
        {
            Debug.LogError("Network handler expected!");
            return;
        }
        networkHandler.ClearRecording();
    }

}
