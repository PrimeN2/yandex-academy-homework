using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _swingVelocityThreshold;
	[SerializeField] private float _swingForce;
	[SerializeField] private float _initialThrust;

	private Rigidbody2D _playerRigidbody;

	private Vector2 _currentRopePosition;
	private float _lastPositionX;
	private bool _isSwinging = false;

	private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
	}

	public void StartSwinging(Vector2 ropePosition)
	{
		_playerRigidbody.AddForce(transform.right * _initialThrust);
		_currentRopePosition = ropePosition;
		_isSwinging = true;
	}

	public void StopSwinging()
	{
		_isSwinging = false;
	}

	private void FixedUpdate()
	{
		if (!_isSwinging)
			return;

		_playerRigidbody.AddForce(CalculatePerpedndicularPosition() * _swingForce);
	}

	private Vector2 CalculatePerpedndicularPosition()
	{
		Vector2 playerToHookDirection = _currentRopePosition - ((Vector2)transform.position).normalized;

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
		_lastPositionX < _currentRopePosition.x && transform.position.x > _currentRopePosition.x;

	private bool HasJustCrossedCentreToLeft() =>
		_lastPositionX > _currentRopePosition.x && transform.position.x < _currentRopePosition.x;

	private bool CanIncreasePositiveVelocity() =>
		_playerRigidbody.velocity.x < _swingVelocityThreshold;
	private bool CanIncreaseNegativeVelocity() =>
		_playerRigidbody.velocity.x > -_swingVelocityThreshold;


}
