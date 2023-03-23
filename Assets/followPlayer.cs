using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3  offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + player.TransformDirection(offset);
        transform.rotation = Quaternion.Euler(15, 0, 0); 
        transform.rotation = player.rotation * Quaternion.Euler(15, 0, 0);
    }
}
