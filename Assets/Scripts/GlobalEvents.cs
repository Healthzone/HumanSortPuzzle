using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public static UnityEvent OnFlasksInitialized = new UnityEvent();
    public static UnityEvent OnBotsInitialized = new UnityEvent();
    public static UnityEvent OnFlaskControllerInitialized = new UnityEvent();

    public static void SendFlaskInitialized()
    {
        OnFlasksInitialized.Invoke();
    }

    public static void SendBotsInitialized()
    {
        OnBotsInitialized.Invoke();
    }
    public static void SendFlaskControllerInitialized()
    {
        OnFlaskControllerInitialized.Invoke();
    }


}
