using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySound : MonoBehaviour
{
    private AudioSource source;
    public NetworkHandler NHandle;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //do stuff
            source.Play();
            NHandle.TriggerSound(source, (int)Time.time);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
    }
}
