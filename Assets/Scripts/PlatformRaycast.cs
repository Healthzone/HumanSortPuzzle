using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class PlatformRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float colorShowDuration = 0.5f;
    [SerializeField] private GameObject flaskHighlighter;

    [SerializeField] private FlaskController selectedFlaskController;
    private RaycastHit hit;

    private MaterialPropertyBlock _propBlock;
    private Renderer _renderer;


    private Color nullColor = new Color(0, 0, 0, 0);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray castPoint = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, raycastLayer))
            {
                SelectFlask(hit);

                //_renderer = hit.transform.gameObject.GetComponent<Renderer>();
                //_renderer.material.color = GetRandomColor();

            }

        }
    }


    private void SelectFlask(RaycastHit hit)
    {
        var hitFlask = hit.transform.GetComponent<FlaskController>();

        if (hitFlask.Colors.Count == 0 && selectedFlaskController == null)
        {
            return;
        }
        if (selectedFlaskController == null)
        {
            selectedFlaskController = hit.transform.GetComponent<FlaskController>();
            HighlightFlaskPlane();
        }
        if (selectedFlaskController != null && hitFlask != selectedFlaskController)
        {
            TryTranslateBots();
        }

    }

    private void HighlightFlaskPlane()
    {
        selectedFlaskController.Colors.TryPeek(out Color result);
        if (result != nullColor)
        {
            flaskHighlighter.SetActive(true);
            flaskHighlighter.transform.position = selectedFlaskController.GetComponent<Renderer>().bounds.center;
            var material = flaskHighlighter.GetComponent<MeshRenderer>().sharedMaterial;
            material.color = Color.black;
            material.DOColor(result, colorShowDuration);
        }
    }

    private void TryTranslateBots()
    {
        Debug.Log("Я перемещаюсь");
        selectedFlaskController = null;
        flaskHighlighter.SetActive(false);

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
