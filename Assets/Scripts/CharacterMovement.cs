using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	[SerializeField] float _speed;

	private Animator _animator;
	private Rigidbody2D _rigidbody;
	private InputHandler _inputHandler;

	public void Construct(InputHandler inputHandler)
	{
		_inputHandler = inputHandler;
	}

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}


	private void Update()
	{
		Vector2 dir = Vector2.zero;

		if (_inputHandler.ShouldMove)
		{
			dir.y = -1;
			_animator.SetInteger("Direction", 0);
		}
		else
		{
			dir.y = 0;
			_animator.SetInteger("Direction", 0);
		}

		_animator.SetBool("IsMoving", dir.magnitude > 0);

		_rigidbody.velocity = _speed * dir;
	}
}
