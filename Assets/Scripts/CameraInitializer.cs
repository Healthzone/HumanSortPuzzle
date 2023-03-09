using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _cameraOffsetZ = -3.7f;

    public void InitializeCameraPositionAndRotation(GameObject[] flasks)
    {
        var bound = new Bounds(flasks[0].transform.position, Vector3.zero);
        for (int i = 1; i < flasks.Length; i++)
        {
            bound.Encapsulate(flasks[i].transform.position);
        }
        var boundCenter = bound.center;

        _camera.transform.position = new Vector3(boundCenter.x, 18f, boundCenter.z + _cameraOffsetZ);
    }
}
