using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static Unity.Burst.Intrinsics.X86;

public class PlatformRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float colorShowDuration = 0.5f;

    [SerializeField] private FlaskController selectedFlaskController;

    [SerializeField] private bool isEnabledReycastTargetting = true;
    private RaycastHit hit;

    private Color nullColor = new Color(0, 0, 0, 0);

    public bool IsEnabledReycastTargetting { get => isEnabledReycastTargetting; set => isEnabledReycastTargetting = value; }

    private void Update()
    {
        if (isEnabledReycastTargetting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray castPoint = mainCamera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, raycastLayer))
                {
                    SelectFlask(hit);
                }
                else if (selectedFlaskController != null)
                {
                    ChangeFlaskPlaneAlphaColor(selectedFlaskController);
                    selectedFlaskController = null;
                }

            }
        }
    }
    private void SelectFlask(RaycastHit hit)
    {
        var hitFlask = hit.transform.GetComponent<FlaskController>();
        Debug.Log(hitFlask);

        //Выбираем повторно ту же колбу
        if (hitFlask.Equals(selectedFlaskController))
        {
            ChangeFlaskPlaneAlphaColor(selectedFlaskController);
            selectedFlaskController = null;
            return;
        }

        //Пытаемся выбрать пустую колбу
        if (hitFlask.Colors.Count == 0 && selectedFlaskController == null)
        {
            return;
        }
        //Выбираем первый раз колбу
        if (selectedFlaskController == null)
        {
            var flaskController = hit.transform.GetComponent<FlaskController>();
            if (!flaskController.IsFilledByOneColor)
            {
                selectedFlaskController = flaskController;
                HighlightFlaskPlane(selectedFlaskController);
            }
        }
        //Выбираем вторую колбу и пытаемся переместить ботов
        if (selectedFlaskController != null && hitFlask != selectedFlaskController)
        {
            HighlightSecondFlaskPlane(hitFlask.gameObject.GetComponent<FlaskController>());
            TryTranslateBots(hitFlask.gameObject);

        }

    }

    private void HighlightFlaskPlane(FlaskController sourceController)
    {
        sourceController.Colors.TryPeek(out Color result);
        if (result != nullColor)
        {
            sourceController.FlaskPlane.SetActive(true);
            //highlighter.transform.position = sourceController.GetComponent<Renderer>().bounds.center;
            var material = sourceController.FlaskPlane.GetComponent<MeshRenderer>().material;
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

        if (poppedBotsList.Count > 0)
        {
            ReverseElement reverseElement = new ReverseElement(poppedBotsList, selectedFlaskController);
            GetComponent<ReverseActionSystem>().SaveAction(reverseElement);
        }
        ChangeFlaskPlaneAlphaColor(selectedFlaskController);
        selectedFlaskController = null;
    }

    private void ChangeFlaskPlaneAlphaColor(FlaskController flask)
    {
        var material = flask.FlaskPlane.GetComponent<MeshRenderer>().material;
        var color = material.color;
        color.a = 0f;

        DOTween.Sequence()
           .Append(material.DOColor(color, colorShowDuration))
           .OnComplete(() =>
               {
                   flask.FlaskPlane.SetActive(false);
               });
    }
    private void HighlightSecondFlaskPlane(FlaskController flask)
    {
        flask.Colors.TryPeek(out Color result);
        if (result != nullColor)
        {
            flask.FlaskPlane.SetActive(true);
            var material = flask.FlaskPlane.GetComponent<MeshRenderer>().material;
            var color = Color.clear;
            material.color = color;
            DOTween.Sequence()
                .Append(material.DOColor(result, colorShowDuration / 2))
                .OnComplete(() =>
                    {
                        color = material.color;
                        color.a = 0f;
                    })
                .Append(material.DOColor(color, colorShowDuration / 2))
                .OnComplete(() =>
                     {
                         flask.FlaskPlane.SetActive(false);
                     });
        }

    }
}
