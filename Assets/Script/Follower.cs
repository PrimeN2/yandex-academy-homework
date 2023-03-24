using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class Follower : MonoBehaviour
    {
		[SerializeField] private Transform target;
		[SerializeField] private float _lerpSpeed = 1.0f;

		private Vector3 offset;
        private Vector3 targetPos;


        private void Awake()
        {
            offset = transform.position - target.position;
		}

        private void LateUpdate()
        {
            targetPos = target.position + offset;

			transform.position = Vector3.Lerp(transform.position, targetPos, _lerpSpeed * Time.deltaTime);
        }

    }
}
