using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] VisualEffect _hitEffect;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name.Equals("Manipulator") == true)
        {
            PlayParticle();
        }
    }

    private void OnMouseDown() {
        PlayParticle();
    }

    void PlayParticle(){
        _hitEffect.Play();
    }

}
