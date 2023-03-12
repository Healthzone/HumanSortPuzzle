using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
            return;
        }
        //Выбираем первый раз колбу
        if (selectedFlaskController == null)
        {
            selectedFlaskController = hit.transform.GetComponent<FlaskController>();
            HighlightFlaskPlane();
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
            material.color = Color.black;
            material.DOColor(result, colorShowDuration);
        }
    }

    private void TryTranslateBots(GameObject secondFlask)
    {
        Debug.Log("Я перемещаюсь");

        var poppedBot = selectedFlaskController.Bots.Pop();
        var poppedColor = selectedFlaskController.Colors.Pop();

        var secondFlaskController = secondFlask.GetComponent<FlaskController>();
        secondFlaskController.Bots.Push(poppedBot);
        secondFlaskController.Colors.Push(poppedColor);
        var toPos = secondFlaskController.FlaskPositions.Dequeue();
        Debug.Log(toPos.position);

        selectedFlaskController.FlaskPositions.Enqueue(poppedBot.transform.parent);
        if (selectedFlaskController.FlaskPositions.Count > 1)
        {
            
        }

        poppedBot.transform.SetParent(toPos);
        poppedBot.GetComponent<NavMeshAgent>().SetDestination(toPos.position);


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
