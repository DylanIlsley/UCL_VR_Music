using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectPlayer : MonoBehaviour
{

    [SerializeField] VisualEffect _hitEffect;

    private void OnTriggerEnter(Collider other) {
        PlayParticle();
    }

    private void OnMouseDown() {
        PlayParticle();
    }

    void PlayParticle(){
        _hitEffect.Play();
    }


}
