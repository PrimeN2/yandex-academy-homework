using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField] private Player _playerPrefab;
	[SerializeField] private LevelSection _levelSectionPrefab;

	[SerializeField] private Vector3 _playerDefaultPosition = new Vector3(0, 0.31f, 0);
	[SerializeField] private Vector3 _eagleOffset = new Vector3(3.6f, -1.5f, 0);
	[SerializeField] private Vector3 _playerOffset = new Vector3(0, 0.55f, 0);

	[SerializeField] private float _sectionOffsetY = 9f;

	private List<LevelSection> _levelSections;

	public Player Player { get; set; }

	public void Construct()
	{
		_levelSections = new List<LevelSection>();

		BuildLevel();
	}

	public void BuildLevel()
	{
		CreatePlayer();

		for (int i = 0; i < 4; i++)
			CreateLevelSection(Vector3.down * (i * _sectionOffsetY));
	}

	private void CreateLevelSection(Vector3 position)
	{
		var section = Instantiate(_levelSectionPrefab, transform);

		section.transform.position = position;
		section.Construct(Player.transform, _eagleOffset + section.transform.position, _playerOffset);
		section.OnSectionPassed += GenerateNewSection;

		_levelSections.Add(section);
	}

	private void GenerateNewSection(LevelSection section) 
	{
		_levelSections.Remove(section);
		section.OnSectionPassed -= GenerateNewSection;
		Destroy(section.gameObject);

		CreateLevelSection(
			(_levelSections[_levelSections.Count - 1].transform.position.y - _sectionOffsetY) * Vector3.up);
	}

	private void CreatePlayer()
	{
		Player = Instantiate(_playerPrefab, _playerDefaultPosition, Quaternion.identity, transform);
	}
}