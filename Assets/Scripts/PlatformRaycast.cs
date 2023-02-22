using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private Camera mainCamera;

    private RaycastHit hit;

    private MaterialPropertyBlock _propBlock;
    private Renderer _renderer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray castPoint = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, raycastLayer))
            {
                _renderer = hit.transform.gameObject.GetComponent<Renderer>();
                _renderer.material.color = GetRandomColor();

            }

        }
    }



    private Color GetRandomColor()
    {
        return new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );

    }
}
