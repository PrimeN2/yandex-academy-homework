using UnityEngine;

public class Rope : MonoBehaviour
{
	public Rigidbody2D LastRopePiece { get => _lastRopePiece; }
	[SerializeField] private Rigidbody2D _lastRopePiece;
}
