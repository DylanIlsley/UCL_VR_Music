using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private NetworkHandler NHandle;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name.Equals("Manipulator") == true)
        {
            playAudio();
        }
    }

    private void OnMouseDown() {
        playAudio();
    }

    void playAudio(){
        source.Play();
        NHandle.TriggerSound(source, (int)Time.time);
    }
}
