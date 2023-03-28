using UnityEngine;

public class Follower : MonoBehaviour
{
	[SerializeField] private float _lerpSpeed = 1.0f;

	private Transform _target;

	private Vector3 _offset;
	private Vector3 _targetPos;


	public void Construct(Transform target)
	{
		_target = target;
		_offset = transform.position - _target.position;
	}

	private void LateUpdate()
	{
		if (_target == null)
			return;
		_targetPos = _target.position + _offset;

		transform.position = Vector3.Lerp(transform.position, _targetPos, _lerpSpeed * Time.deltaTime);
	}
}
