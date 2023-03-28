using UnityEngine;

public class LevelSection : MonoBehaviour
{
	public event System.Action<LevelSection> OnSectionPassed;

	[SerializeField] private Eagle _eagle;

	private Transform _target;

	public void Construct(Transform target, Vector2 eagleOffset, Vector2 playerOffset)
	{
		float eagleSpeed = Random.Range(2, 4);

		_target = target;
		_eagle.Construct(eagleOffset + Vector2.right * eagleSpeed * 0.5f, playerOffset, eagleSpeed);
	}

	private void Update()
	{
		if (_target != null && transform.position.y > _target.position.y + 18)
			OnSectionPassed?.Invoke(this);
	}
}