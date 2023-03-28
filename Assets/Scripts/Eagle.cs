using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
	private const string HuntingAnimation = "IsHunting";
	private const string GrabTrigger = "Grab";

	[SerializeField] private GameObject _dangerSign;

	private SpriteRenderer _renderer;
	private Animator _animator;
	private Coroutine _movement;

	private List<Transform> _targets;

	private Vector3 _eagleOffset;
	private Vector3 _playerOffset;

	private int _direction;
	private float _speed;
	private bool _isAware = true;

	public void Construct(Vector2 eagleOffset, Vector2 playerOffset, float speed)
	{
		_renderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();

		_targets = new List<Transform>();

		_eagleOffset = eagleOffset;
		_playerOffset = playerOffset;
		_speed = speed;

		_isAware = true;
		_direction = -1;

		DefineMovement();
	}

	public void Attack(Transform target)
	{
		if (_isAware && !_targets.Contains(target))
		{
			StopCoroutine(_movement);
			_movement = StartCoroutine(Move(target));

			_targets.Add(target);
		}
	}

	public void Dismiss(Transform target)
	{
		if (_targets.Contains(target))
		{
			StopCoroutine(_movement);
			DefineMovement();
			_targets.Remove(target);
		}
	}

	private IEnumerator Move(Vector3 targetPosition)
	{
		Vector3 lastPosition = transform.position;

		_animator.SetBool(HuntingAnimation, false);

		while (transform.position.x != targetPosition.x)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * _speed);

			if (transform.position.x > 0 && lastPosition.x - transform.position.x < 0 ||
				transform.position.x < 0 && lastPosition.x - transform.position.x > 0)
				_isAware = false; 
			else
				_isAware = true;

			_dangerSign.SetActive(_isAware);
			lastPosition = transform.position;
			yield return null;
		}

		_direction *= -1;
		_renderer.flipX = _direction == 1;

		DefineMovement();
}

	private IEnumerator Move(Transform targetTransform)
	{
		if (targetTransform == null)
		{
			DefineMovement();
			yield break;
		}

		_animator.SetBool(HuntingAnimation, true);

		while (transform.position != targetTransform.position)
		{
			transform.position =
				Vector3.MoveTowards(
					transform.position, targetTransform.position + _playerOffset, Time.deltaTime * _speed);

			_direction = transform.position.x < targetTransform.position.x ? 1 : -1;
			_renderer.flipX = _direction == 1;

			if (Mathf.Abs(transform.position.x - targetTransform.position.x) < 0.85f)
				_animator.SetTrigger(GrabTrigger);

			yield return null;
		}

		DefineMovement();
	}

	private void DefineMovement()
	{
		_movement = StartCoroutine(Move(new Vector2(_eagleOffset.x * _direction, _eagleOffset.y)));
	}
}