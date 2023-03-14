using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModelVariants : MonoBehaviour
{
	[SerializeField] private GameObject[] _models;
	[SerializeField] private TMP_Dropdown _dropdown;

	private GameObject _currenSelected;

    private void Start()
    {
        _currenSelected = _models[0];

        List<TMP_Dropdown.OptionData> _optionDataList = new List<TMP_Dropdown.OptionData>();

        foreach (var item in _models)
        {
            TMP_Dropdown.OptionData data = new();
            data.text = item.name;
            _optionDataList.Add(data);
        }
        _dropdown.options = _optionDataList;

        _dropdown.onValueChanged.AddListener(Select);

    }

    public void Select(int index) 
    {
        _currenSelected.SetActive(false);
        _currenSelected = _models[index];
        _currenSelected.SetActive(true);
	}


}
