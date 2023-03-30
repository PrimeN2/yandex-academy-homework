using System;
using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    private const string LabelText = "Your score:";

	[SerializeField] private TMP_Text _label;

    public void Construct(int score)
    {
        SetScore(score);
    }

    public void SetScore(int score)
    {
        _label.text = LabelText + score;
    }

}
