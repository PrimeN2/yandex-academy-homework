using UnityEngine;

public class DangerZoneTriggerHandler : MonoBehaviour
{
	[SerializeField] private Eagle _eagle;

	private void OnTriggerStay2D(Collider2D collision)
	{
		_eagle.Attack(collision.transform);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		_eagle.Dismiss(collision.transform);
	}
}