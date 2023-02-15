using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Joint2D), typeof(PlayerMovement), typeof(Rigidbody2D))]
public class PlayerJointsHandler : MonoBehaviour
{
	[SerializeField] private Rope _currentRope;

	private PlayerMovement _playerMovement;

	private Joint2D _playerJoint;

	private Rigidbody2D _playerRigidbody;

	public bool IsTied { get; private set; }

	private void Awake()
	{
		_playerJoint = GetComponent<Joint2D>();
		_playerMovement = GetComponent<PlayerMovement>();
		_playerRigidbody = GetComponent<Rigidbody2D>();

		IsTied = true;
	}

	private void Start()
	{
		_playerMovement.StartSwinging(_currentRope.transform.position);
	}

	public void TieJoint(Rope _rope)
	{
		transform.position = new Vector3(_rope.transform.position.x - 0.05f, _rope.transform.position.y - 4.2f);

		_playerRigidbody.freezeRotation = false;
		_playerRigidbody.velocity = Vector2.zero;
		_playerRigidbody.angularVelocity = 0;
		_currentRope = _rope;
		_playerJoint.enabled = true;
		_playerJoint.connectedBody = _rope.LastRopePiece;
		_playerJoint.connectedBody.mass = 0.5f;

		IsTied = true;

		_playerMovement.StartSwinging(_currentRope.transform.position);
	}

	public void UntieCurrentJoint()
	{
		_playerMovement.StopSwinging();
		_playerJoint.connectedBody.mass = 2;

		_playerRigidbody.freezeRotation = true;
		_playerJoint.connectedBody = null;
		_playerJoint.enabled = false;
		_currentRope = null;
		IsTied = false;
	}
}