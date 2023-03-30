using UnityEngine;

public class InputHandler : MonoBehaviour
{
	public bool ShouldMove { get => _isActive && Input.GetMouseButton(0); }

	private bool _isActive = false;

	public void Construct(bool isActive)
	{
		_isActive = isActive;
	}

	public void EnableHandling()
	{
		_isActive = true;
	}

	public void DisableHandling()
	{
		_isActive = false;
	}
}
