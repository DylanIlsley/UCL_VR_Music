using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{

    public AnimationCurve FloatAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public float RotationSpeed = 1;
    private float _period;
    public float Amplitude = 0.5f;
    private Vector3 posOffset;
    public Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        _period = Random.Range(0f, 1f);
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = posOffset + Amplitude* new Vector3(0, Mathf.Sin(FloatAnimationCurve.Evaluate((RotationSpeed * Time.time + _period) % 1) ),0);
        newPos = transform.position;
    }
}
