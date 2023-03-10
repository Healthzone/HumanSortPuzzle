using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float colorShowDuration = 0.5f;

    private RaycastHit hit;

    private MaterialPropertyBlock _propBlock;
    private Renderer _renderer;

    private FlaskController selectedFlaskController;

    private Color nullColor = new Color(0, 0, 0, 0);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray castPoint = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, raycastLayer))
            {
                HighlightPlane(hit);
                //_renderer = hit.transform.gameObject.GetComponent<Renderer>();
                //_renderer.material.color = GetRandomColor();

            }

        }
    }


    private void HighlightPlane(RaycastHit hit)
    {
        if (selectedFlaskController != null)
        {
            selectedFlaskController.FlaskPlane.SetActive(false);
        }
        selectedFlaskController = hit.transform.GetComponent<FlaskController>();

        selectedFlaskController.Colors.TryPeek(out Color result);
        if (result != nullColor)
        {
            selectedFlaskController.FlaskPlane.SetActive(true);
            var material = selectedFlaskController.FlaskPlane.GetComponent<MeshRenderer>().sharedMaterial;
            material.color = Color.black;
            material.DOColor(result, colorShowDuration);
                


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
