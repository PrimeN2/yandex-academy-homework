using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Interaction : MonoBehaviour
{

    [SerializeField] private Camera _camera;

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				if (hit.collider.TryGetComponent(out IClickable hitable))
					hitable.Click();
			}
		}


    }
}
