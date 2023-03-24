using System.Collections;
using UnityEngine;

public class Clickable : MonoBehaviour, IClickable
{
	[SerializeField] private Resources _resources;
    [SerializeField] private CoinsContainer _containerPrefab;

	[SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private float _scaleTime = 0.25f;
	[SerializeField] private float _departureSpeed = 2;

    private Transform _containersKeeper;

	public void Click()
    {
        CreateCoinsContainer();
        StartCoroutine(HitAnimation());
    }

    private void CreateCoinsContainer()
    {
        if (!_containersKeeper)
            _containersKeeper = new GameObject("Containers Keeper").GetComponent<Transform>();

        var container =
            Instantiate(_containerPrefab, transform.position + Vector3.up, Quaternion.identity, _containersKeeper);
        container.Construct(_resources, 1, GetComponentInChildren<MeshRenderer>().material);
        container.GetComponent<Rigidbody>().velocity += new Vector3(Random.Range(-1f, 1f), 1, -1) * _departureSpeed;
    }

    private IEnumerator HitAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / _scaleTime)
        {
            float scale = _scaleCurve.Evaluate(t);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
}
