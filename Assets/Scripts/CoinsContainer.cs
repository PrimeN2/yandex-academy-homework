using System.Threading.Tasks;
using UnityEngine;

public class CoinsContainer : MonoBehaviour, IClickable
{
	[SerializeField] private HitEffect _hitEffectPrefab;

	private Resources _resources;
	private int _coinsPerClick = 1;

	public void Construct(Resources resources, int coinsPerClick, Material material)
	{
		_resources = resources;
		_coinsPerClick = coinsPerClick;

		GetComponent<MeshRenderer>().material = material;
	}

	public void Click()
	{
		_resources.CollectCoins(_coinsPerClick, transform.position);
		HitEffect hitEffect = Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
		hitEffect.Init(_coinsPerClick);

		DelaydDestroy();
	}

	public async void DelaydDestroy()
	{
		gameObject.SetActive(false);
		await Task.Delay(2000);
		Destroy(gameObject);
	}
}
