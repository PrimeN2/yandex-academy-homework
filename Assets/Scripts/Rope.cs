using UnityEngine;

public class Rope : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _lastRopePiece;
	public Rigidbody2D LastRopePiece { get => _lastRopePiece; }
}
