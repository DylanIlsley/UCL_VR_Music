using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioPlayer), typeof(EffectPlayer))]
public class NetworkPanel : MonoBehaviour
{
    public AudioPlayer m_AudioPlayer;
    public EffectPlayer m_EffectPlayer;
    [SerializeField] private NetworkHandler NHandle;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioPlayer = GetComponent<AudioPlayer>();
        m_EffectPlayer = GetComponent<EffectPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Manipulator") == true)
        {
            UpdateNetwork();
        }
    }

    private void OnMouseDown()
    {
       UpdateNetwork();
    }

    private void UpdateNetwork()
    {
        if (!NHandle)
            return;
        float fCurrentTime = (float)Time.time;
        Debug.Log(m_AudioPlayer.source.name);
        NHandle.TriggerNetworkPanel(m_AudioPlayer.source, m_EffectPlayer._audioSyncColor, fCurrentTime);
    }


}
