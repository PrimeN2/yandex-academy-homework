using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _swingVelocityThreshold;
	[SerializeField] private float _swingForce;
	[SerializeField] private float _initialThrust;

	private Rigidbody2D _playerRigidbody;
	private Vector2 currentRopePosition;
	private float _lastPositionX;
	private bool _isSwinging = false;

	private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
	}

    private void Start()
    {
		
	}

	public void StartSwinging(Vector2 ropePosition)
	{
		_playerRigidbody.AddForce(transform.right * _initialThrust);
		currentRopePosition = ropePosition;
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

		Vector2 playerToHookDirection = currentRopePosition - ((Vector2)transform.position).normalized;

		if (_lastPositionX < currentRopePosition.x &&
			transform.position.x > currentRopePosition.x &&
			_playerRigidbody.velocity.x < _swingVelocityThreshold)
		{
			Vector2 perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
			Vector2 rightPerpendicularPosition = (Vector2)transform.position + perpendicularDirection * 2f;

			_playerRigidbody.AddForce(rightPerpendicularPosition * _swingForce);
		}
		else if (_lastPositionX > currentRopePosition.x &&
			transform.position.x < currentRopePosition.x &&
			_playerRigidbody.velocity.x > -_swingVelocityThreshold)
		{
			Vector2 perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
			Vector2 leftPerpendicularPosition = (Vector2)transform.position - perpendicularDirection * -2f;

			_playerRigidbody.AddForce(leftPerpendicularPosition * _swingForce);
		}
		_lastPositionX = transform.position.x;
	}
}
