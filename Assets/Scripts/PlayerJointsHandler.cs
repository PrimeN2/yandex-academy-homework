using System;
using UnityEngine;

[RequireComponent(typeof(Joint2D))]
public class PlayerJointsHandler : MonoBehaviour
{
	private Joint2D _playerJoint;

	private void Awake()
	{
		_playerJoint = GetComponent<Joint2D>();
	}

	public void UntieCurrentJoint()
	{
		_playerJoint.connectedBody = null;
		_playerJoint.enabled = false;
	}
}