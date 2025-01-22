using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class Parallax : MonoBehaviour
    {
        private ProgressionManager progressionManager;
        [Range(1,3)]
        [SerializeField] private int groupIndex;
        [SerializeField] private float speed;
        private float startingPosition;
        private float spriteLength;

        private void Start()
        {
            progressionManager = FindAnyObjectByType<ProgressionManager>();
            startingPosition = transform.position.x;
            spriteLength = GetComponent<SpriteRenderer>().bounds.size.x;
        }


        private void LateUpdate()
        {
            Vector3 position = transform.position;
            position.x -= progressionManager.BaseMPS * progressionManager.ProgressionMultiplier * speed * Time.deltaTime;

            if (position.x < startingPosition - (spriteLength * groupIndex)) 
            {
                position.x = startingPosition + ((3 - groupIndex) * spriteLength); 
            }

            transform.position = position;
        }
    }
}