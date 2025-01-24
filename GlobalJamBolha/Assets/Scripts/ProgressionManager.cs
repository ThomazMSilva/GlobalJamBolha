﻿using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class ProgressionManager : MonoBehaviour
    {
        public bool isGamePaused = true;
        [SerializeField] private PlayerLife playerLife;
        [SerializeField] private TextMeshProUGUI distanceValue_TMP;
        public float DistanceRan { get; private set; }
        [SerializeField] private AnimationCurve velocityCurve;
        [SerializeField] private float maxDistance = 1000f;
        [SerializeField] private float maxMultiplier = 3f;
        [SerializeField] private float baseMetersPerSecond = 5f;
        public float BaseMPS { get => baseMetersPerSecond; private set { baseMetersPerSecond = value; } }

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

        private void Start()
        {
            playerLife.OnPlayerDeath += () => DistanceRan = 0;
        }

        private void Update()
        {
            if (isGamePaused) return;

            DistanceRan += Time.deltaTime * (BaseMPS * ProgressionMultiplier);
            DistanceRan = Mathf.Min(DistanceRan, maxDistance);
            distanceValue_TMP.text = $"Distancia: {DistanceRan:F0}m";
        }
    }
}