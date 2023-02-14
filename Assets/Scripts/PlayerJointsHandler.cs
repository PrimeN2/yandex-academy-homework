using System;
using UnityEngine;

[RequireComponent(typeof(Joint2D), typeof(PlayerMovement), typeof(Rigidbody2D))]
public class PlayerJointsHandler : MonoBehaviour
{
	public Transform CurrentRope { get => _currentRope; }
	[SerializeField] private Transform _currentRope;

	private PlayerMovement _playerMovement;
	private Joint2D _playerJoint;
	private Rigidbody2D _playerRigidbody;

	private void Awake()
	{
		_playerJoint = GetComponent<Joint2D>();
		_playerMovement = GetComponent<PlayerMovement>();
		_playerRigidbody = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		_playerMovement.StartSwinging(_currentRope.position);
	}

	public void UntieCurrentJoint()
	{
		_playerMovement.StopSwinging();
		_playerJoint.connectedBody.mass = 2;

		_playerRigidbody.freezeRotation = true;
		_playerJoint.connectedBody = null;
		_playerJoint.enabled = false;
		_currentRope = null;
	}
}