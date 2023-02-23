using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Joint2D), typeof(Rigidbody2D))]
public class PlayerJointsSystem : MonoBehaviour
{
	[SerializeField] private float _swingVelocityThreshold;
	[SerializeField] private float _swingForce;
	[SerializeField] private float _thrust;

	private Rope _currentRope;
	private Joint2D _playerJoint;
	private Rigidbody2D _playerRigidbody;

	private float _lastPositionX;

	public bool IsTied { get => _currentRope != null; }

	private Vector2 _ropePosition { get => _currentRope.transform.position; }

	private void Awake()
	{
		_playerJoint = GetComponent<Joint2D>();
		_playerRigidbody = GetComponent<Rigidbody2D>();

		_lastPositionX = transform.position.x;
	}

	public Vector2 GetOnTiedThrust() =>
		_thrust * Vector2.right;

	public Vector2 GetSwingForce() =>
		CalculatePerpedndicularPosition() * _swingForce;

	public void TieTo(Rope _rope)
	{
		transform.position = new Vector3(_rope.transform.position.x - 0.05f, _rope.transform.position.y - 4.2f);

		_currentRope = _rope;
		_playerJoint.enabled = true;
		_playerJoint.connectedBody = _rope.LastRopePiece;
		_playerJoint.connectedBody.mass = 0.5f;
	}

	public void UntieFromCurrentRope()
	{
		_playerJoint.connectedBody.mass = 2;

		_playerRigidbody.angularVelocity = 0;
		_playerJoint.connectedBody = null;
		_playerJoint.enabled = false;
		_currentRope = null;
	}

	private Vector2 CalculatePerpedndicularPosition()
	{
		Vector2 playerToHookDirection = _ropePosition - ((Vector2)transform.position).normalized;

		if (HasJustCrossedCentreToRight() && CanIncreasePositiveVelocity())
		{
			Vector2 perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
			Vector2 rightPerpendicularPosition = (Vector2)transform.position + perpendicularDirection * 2f;

			return rightPerpendicularPosition;
		}
		else if (HasJustCrossedCentreToLeft() && CanIncreaseNegativeVelocity())
		{
			Vector2 perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
			Vector2 leftPerpendicularPosition = (Vector2)transform.position - perpendicularDirection * -2f;

			return leftPerpendicularPosition;
		}

		_lastPositionX = transform.position.x;
		return Vector2.zero;
	}

	private bool HasJustCrossedCentreToRight() =>
		_lastPositionX < _ropePosition.x && transform.position.x > _ropePosition.x;

	private bool HasJustCrossedCentreToLeft() =>
		_lastPositionX > _ropePosition.x && transform.position.x < _ropePosition.x;

	private bool CanIncreasePositiveVelocity() =>
		_playerRigidbody.velocity.x < _swingVelocityThreshold;
	private bool CanIncreaseNegativeVelocity() =>
		_playerRigidbody.velocity.x > -_swingVelocityThreshold;
}