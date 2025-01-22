using UnityEngine;

namespace Assets.Scripts
{
    public class ProgressionManager : MonoBehaviour
    {
        public float DistanceRan { get; private set; }
        [SerializeField] private AnimationCurve velocityCurve;
        [SerializeField] private float maxDistance = 1000f;
        [SerializeField] private float maxMultiplier = 3f;
        public float NormalizedDistance {  get; private set; }

        public float ProgressionMultiplier 
        {
            get
            {
                NormalizedDistance = Mathf.Clamp01(DistanceRan / maxDistance);
                return Mathf.Clamp(velocityCurve.Evaluate(NormalizedDistance * maxMultiplier), 1, maxMultiplier);
            }
            private set {}
        }

        private void Update()
        {
            DistanceRan += Time.deltaTime * (10 * ProgressionMultiplier);
            DistanceRan = Mathf.Min(DistanceRan, maxDistance);
        }
    }
}