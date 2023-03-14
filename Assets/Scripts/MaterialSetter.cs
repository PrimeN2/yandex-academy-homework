using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    [SerializeField] private Renderer[] _models;

    public void SetMaterial(Material material) 
    {
        foreach (var model in _models)
        {
            model.material = material;
		}
    }
}
