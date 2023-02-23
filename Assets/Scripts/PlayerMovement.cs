using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerGravitationSystem), typeof(PlayerJointsSystem))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private Rope StartingRope;

	private Rigidbody2D _playerRigidbody;
	private CapsuleCollider2D _playerCollider;
	private PlayerJointsSystem _jointsSystem;
	private PlayerGravitationSystem _gravitationSystem;

	public bool IsTied { get => _jointsSystem.IsTied; }

	public void TieTo(Rope rope) 
	{
		if (IsTied) return;

		TurnManualGravitationSystemOff();

		_jointsSystem.TieTo(rope);
		_playerRigidbody.AddForce(_jointsSystem.GetOnTiedThrust());
	}

	public void UntieFromRope() 
	{
		if (!IsTied) return;

		_jointsSystem.UntieFromCurrentRope();

		TurnManualGravitationSystemOn();
	}

	public void Jump()
	{
		_gravitationSystem.Jump();
	}

	private void Awake()
	{
		_playerRigidbody = GetComponent<Rigidbody2D>();
		_playerCollider = GetComponent<CapsuleCollider2D>();
		_gravitationSystem = GetComponent<PlayerGravitationSystem>();
		_jointsSystem = GetComponent<PlayerJointsSystem>();
	}

	private void Start()
	{
		if (StartingRope != null)
			TieTo(StartingRope);
		else 
			TurnManualGravitationSystemOn();
	}

	private void FixedUpdate()
	{
		if (IsTied)
			_playerRigidbody.AddForce(_jointsSystem.GetSwingForce());
		else
		{
			_gravitationSystem.ApplyGravity();
		}
	}

	private void TurnManualGravitationSystemOn()
	{
		_playerRigidbody.bodyType = RigidbodyType2D.Kinematic;
		_playerCollider.enabled = true;
	}

	private void TurnManualGravitationSystemOff()
	{
		_playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
		_playerCollider.enabled = false;
	}
}
