using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        Debug.Log("Start"); 
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Equals("Manipulator"))
        {
            playAnimation();
        }
    }

    private void OnMouseDown() {
        playAnimation();
    }

    void playAnimation(){
        _animator.SetTrigger("HitKey");
        Debug.Log("Played animation"); 
    }
}
