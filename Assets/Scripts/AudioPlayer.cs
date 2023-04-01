using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Equals("Manipulator"))
        {
            playAudio();
        }
    }

    private void OnMouseDown() {
        playAudio();
    }

    void playAudio(){
        source.Play();
    }
}