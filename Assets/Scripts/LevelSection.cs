using UnityEngine;

public class LevelSection : MonoBehaviour
{
	public event System.Action<LevelSection> OnSectionPassed;

	[SerializeField] private Eagle _eagle;

	private Transform _target;

	private float _sectionYOffset;

	public Eagle Eagle { get => _eagle; }

	public void Construct(Transform target, Vector2 eagleOffset, Vector2 playerOffset, float sectionYOffset)
	{
		float eagleSpeed = Random.Range(2, 4);

		_target = target;
		_sectionYOffset = sectionYOffset;

		_eagle.Construct(eagleOffset + Vector2.right * eagleSpeed * 0.5f, playerOffset, eagleSpeed);
	}

	private void Update()
	{
		if (_target != null && transform.position.y > _target.position.y + _sectionYOffset)
			OnSectionPassed?.Invoke(this);
	}
}