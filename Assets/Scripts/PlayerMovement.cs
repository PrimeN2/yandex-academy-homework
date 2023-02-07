using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _playerBody;

    [SerializeField] private float _thrust;

	private void Awake()
    {
        _playerBody = GetComponent<Rigidbody2D>();
	}

    private void Start()
    {
        _playerBody.AddForce(transform.right * -1 * _thrust);
    }
}
