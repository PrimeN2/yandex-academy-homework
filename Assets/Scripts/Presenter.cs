using UnityEngine;
using UnityEngine.UI;

public abstract class Presenter : MonoBehaviour
{
    [SerializeField] protected Text _render;
    [SerializeField] protected Animator _animator;
}
