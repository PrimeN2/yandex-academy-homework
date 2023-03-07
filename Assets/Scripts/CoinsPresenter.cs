using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CoinsPresenter : Presenter
{
	private const string OnPickupCoinTrigger = "OnPickupCoin";

	private Wallet _model;

	[Inject]
	private void Construct(Wallet model)
	{
		_model = model;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Contains("Coin"))
		{
			_model.AddCoin();
		}
	}

	private void OnEnable()
	{
		_model.OnBalanceChanged += ShowBalance;
	}

	private void OnDisable()
	{
		_model.OnBalanceChanged -= ShowBalance;
	}

	private void ShowBalance(int currentBalance)
	{
		_render.text = $"Coins: {currentBalance}";
		_animator.SetTrigger(OnPickupCoinTrigger);
	}
}