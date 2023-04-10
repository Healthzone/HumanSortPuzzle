using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraInitializer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float margin = 1.0f;

    private GameObject[] _flasks;

    public float Margin { get => margin; set => margin = value; }
    public Camera Camera { get => _camera; }

    public void InitializeCameraPositionAndRotation(GameObject[] flasks)
    {
        try
        {
            if (_camera != null && flasks != null)
            {
                _flasks = flasks;
                var bounds = flasks[0].GetComponent<Renderer>().bounds;
                for (int i = 1; i < flasks.Length; i++)
                {
                    bounds.Encapsulate(flasks[i].GetComponent<Renderer>().bounds);
                }

                float orthoSize;
                if (_camera.aspect < 1)
                {
                    orthoSize = bounds.extents.magnitude / _camera.aspect;
                }
                else
                {
                    orthoSize = bounds.extents.magnitude;
                }
                var _cameraOffset = orthoSize / Mathf.Tan(Mathf.Deg2Rad * _camera.transform.rotation.eulerAngles.x);

                _camera.orthographicSize = orthoSize * margin;
                _camera.transform.position = bounds.center + Vector3.up * orthoSize;
                _camera.transform.position += Vector3.back * _cameraOffset;
                //_camera.nearClipPlane = minDistance - maxExtent * margin;
            }


        }
        catch
        {
            Debug.Log("Trying to get Camera component but no reference in scene");
        }
    }

    public void ReInitializedCameraPosition()
    {
        if (_camera != null && _flasks != null)
        {
            InitializeCameraPositionAndRotation(_flasks);
        }
    }
}
