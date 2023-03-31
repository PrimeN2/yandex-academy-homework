using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public Action<int> OnScoreChanged;

	[SerializeField] private Player _playerPrefab;
	[SerializeField] private LevelSection _levelSectionPrefab;

	[SerializeField] private Vector3 _playerDefaultPosition = new Vector3(0, 0.31f, 0);
	[SerializeField] private Vector3 _eagleOffset = new Vector3(3.6f, -1.5f, 0);
	[SerializeField] private Vector3 _playerOffset = new Vector3(0, 0.55f, 0);

	[SerializeField] private float _sectionOffsetY = 9f;

	private List<LevelSection> _levelSections;

	private int _score = 0;

	public Player Player { get; set; }
	public LevelSection FirstSection { get => _levelSections[0]; }

	public bool HasPassedFirstSection { get; private set; }

	public void Construct(int score)
	{
		_levelSections = new List<LevelSection>();
		HasPassedFirstSection = false;

		_score = score;

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
		section.Construct(
			Player.transform, _eagleOffset + section.transform.position, _playerOffset, _sectionOffsetY);
		section.OnSectionPassed += GenerateNewSection;

		_levelSections.Add(section);
	}

	private void GenerateNewSection(LevelSection previousSection) 
	{
		HasPassedFirstSection = true;

		_levelSections.Remove(previousSection);
		previousSection.OnSectionPassed -= GenerateNewSection;
		Destroy(previousSection.gameObject);

		CreateLevelSection(
			(_levelSections[_levelSections.Count - 1].transform.position.y - _sectionOffsetY) * Vector3.up);

		OnScoreChanged?.Invoke(++_score);
	}

	private void CreatePlayer()
	{
		Player = Instantiate(_playerPrefab, _playerDefaultPosition, Quaternion.identity, transform);
	}
}