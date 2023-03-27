using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleSpinner
{
    [RequireComponent(typeof(RawImage))]
    public class SimpleSpinner : MonoBehaviour
    {
        [Header("Rotation")]
        public bool Rotation = true;
        [Range(-10, 10), Tooltip("Value in Hz (revolutions per second).")]
        public float RotationSpeed = 1;
        public AnimationCurve RotationAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);


        [Header("Options")]
        public bool RandomPeriod = true;
        
        private RawImage _image;
        private float _period;

        public void Start()
        {
            _image = GetComponent<RawImage>();
            _period = RandomPeriod ? Random.Range(0f, 1f) : 0;
        }

        public void Update()
        {
            if (Rotation)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 180 * RotationAnimationCurve.Evaluate((RotationSpeed * Time.time + _period) % 1));
            }

        }
    }
}