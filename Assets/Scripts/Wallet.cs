using System;

public class Wallet
{
	public Action<int> OnBalanceChanged;

	private int _balance;

	public Wallet(int balance)
	{
		_balance = balance;
	}

	public void AddCoin()
	{
		_balance += 1;
	}

	public bool TryDiscard(int price)
	{
		if (_balance < price)
			return false;

		_balance -= price;

		OnBalanceChanged?.Invoke(_balance);

		return true;
	}
}