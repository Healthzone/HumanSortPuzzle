using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CameraInitializer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float margin = 0.9f;

    public void InitializeCameraPositionAndRotation(GameObject[] flasks)
    {
        var bounds = flasks[0].GetComponent<Renderer>().bounds;
        for (int i = 1; i < flasks.Length; i++)
        {
            bounds.Encapsulate(flasks[i].GetComponent<Renderer>().bounds);
        }

        float horizontalFov = Camera.VerticalToHorizontalFieldOfView(_camera.fieldOfView, _camera.aspect);
        float maxExtent = bounds.extents.magnitude;
        var minDistance = (maxExtent * margin) / Mathf.Tan(horizontalFov * 0.5f * Mathf.Deg2Rad);

        _camera.transform.position = new Vector3(bounds.center.x, minDistance, bounds.center.z);
        _camera.transform.LookAt(bounds.center);
        _camera.nearClipPlane = minDistance - maxExtent * margin;

    }
}
