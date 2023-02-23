using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerGravitationSystem : MonoBehaviour
{
	private const float MinMoveDistance = 0.001f;
	private const float ShellRadius = 0.01f;

	[SerializeField] private float _minGroundNormalY = .65f;
	[SerializeField] private float _gravityModifier = 1f;
	[SerializeField] private LayerMask _layerMask;
	[SerializeField] private float _jumpForce;
	[SerializeField] private float _speed;

	private Rigidbody2D _rigidbody;

	private bool _grounded;
	private Vector2 _velocity;
	private Vector2 _groundNormal;
	private Vector2 _targetVelocity;
	private ContactFilter2D _contactFilter;
	private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
	private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();

		_contactFilter.useTriggers = false;
		_contactFilter.SetLayerMask(_layerMask);
		_contactFilter.useLayerMask = true;
	}

	private void OnEnable()
	{
		_velocity = Vector2.zero;
		_groundNormal = Vector2.zero;
		_targetVelocity = Vector2.zero;
	}

	public void Jump()
	{
		if (!_grounded) return;

		_velocity += Vector2.up * _jumpForce;
		_grounded = false;
	}

	public void ApplyGravity()
	{
		CalculateVelocity();

		SetVelocityDirection();

		Vector2 deltaPosition = _velocity * Time.deltaTime;
		Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);

		Vector2 xMovement = moveAlongGround * deltaPosition.x;
		Vector2 yMovement = Vector2.up * deltaPosition.y;

		_grounded = false;

		MoveBy(xMovement, false);
		MoveBy(yMovement, true);
	}

	private void CalculateVelocity()
	{
		Vector2 alongSurface = Vector2.Perpendicular(_groundNormal);

		_targetVelocity = alongSurface * _speed;

		_velocity += _gravityModifier * Time.deltaTime * Physics2D.gravity;
	}

	private void SetVelocityDirection()
	{
		if (_groundNormal.x >= 0)
			_velocity.x = -_targetVelocity.x;
		else
			_velocity.x = _targetVelocity.x;
	}

	private void MoveBy(Vector2 move, bool yMovement)
	{
		float distance = move.magnitude;

		if (distance > MinMoveDistance)
		{
			int count = _rigidbody.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);

			_hitBufferList.Clear();

			for (int i = 0; i < count; i++)
			{
				_hitBufferList.Add(_hitBuffer[i]);
			}

			for (int i = 0; i < _hitBufferList.Count; i++)
			{
				Vector2 currentNormal = _hitBufferList[i].normal;
				if (currentNormal.y > _minGroundNormalY)
				{
					_grounded = true;
					if (yMovement)
					{
						_groundNormal = currentNormal;
						currentNormal.x = 0;
					}
				}

				float projection = Vector2.Dot(_velocity, currentNormal);
				if (projection < 0)
				{
					_velocity -= projection * currentNormal;
				}

				float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
				distance = modifiedDistance < distance ? modifiedDistance : distance;
			}
		}

		_rigidbody.position += move.normalized * distance;
	}
}