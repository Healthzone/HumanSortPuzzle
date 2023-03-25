using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static Unity.Burst.Intrinsics.X86;

public class PlatformRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float colorShowDuration = 0.5f;
    [SerializeField] private GameObject flaskHighlighter;

    [SerializeField] private FlaskController selectedFlaskController;
    private RaycastHit hit;

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

        //Пытаемся выбрать пустую колбу
        if (hitFlask.Colors.Count == 0 && selectedFlaskController == null)
        {
            Debug.Log("Колба пустая");
            return;
        }
        //Выбираем первый раз колбу
        if (selectedFlaskController == null)
        {
            var flaskController = hit.transform.GetComponent<FlaskController>();
            if (!flaskController.IsFilledByOneColor)
            {
                selectedFlaskController = flaskController;
                HighlightFlaskPlane();
            }
        }
        //Выбираем вторую колбу и пытаемся переместить ботов
        if (selectedFlaskController != null && hitFlask != selectedFlaskController)
        {
            TryTranslateBots(hitFlask.gameObject);
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
            var color = Color.clear;
            material.color = color;
            material.DOColor(result, colorShowDuration);
        }
    }

    private void TryTranslateBots(GameObject secondFlask)
    {
        bool isNextBotHasSameColor = false;
        bool isNextPositionEmpty = false;

        var secondFlaskController = secondFlask.GetComponent<FlaskController>();

        var poppedBotsList = new List<GameObject>();
        do
        {
            if (secondFlaskController.Bots.Count == 4)
                continue;

            secondFlaskController.Colors.TryPeek(out Color goalColor);
            selectedFlaskController.Colors.TryPeek(out Color sourceColor);

            if (sourceColor != goalColor && secondFlaskController.Colors.Count != 0)
                continue;

            var poppedBot = selectedFlaskController.Bots.Pop();
            var poppedColor = selectedFlaskController.Colors.Pop();

            poppedBotsList.Add(poppedBot);

            selectedFlaskController.ShiftNextPositionIndex(0);
            isNextPositionEmpty = secondFlaskController.ProcessBotPosition(poppedBot);

            selectedFlaskController.Colors.TryPeek(out Color nextColor);
            if (poppedColor == nextColor)
                isNextBotHasSameColor = true;
            else
                isNextBotHasSameColor = false;
        } while (isNextBotHasSameColor && isNextPositionEmpty);

        ReverseElement reverseElement = new ReverseElement(poppedBotsList, selectedFlaskController);
        GetComponent<ReverseActionSystem>().SaveAction(reverseElement);
        selectedFlaskController = null;
        UnhightlightFlaskPlane();
    }

    private void UnhightlightFlaskPlane()
    {
        var material = flaskHighlighter.GetComponent<MeshRenderer>().sharedMaterial;
        var color = material.color;
        color.a = 0f;
        material.DOColor(color, colorShowDuration / 2).OnComplete(EnableFlaskPlane);

    }
    private void EnableFlaskPlane()
    {
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
