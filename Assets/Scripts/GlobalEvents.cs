using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public static UnityEvent OnFlasksInitialized = new UnityEvent();
    public static UnityEvent<Bot[]> OnBotsInitialized = new UnityEvent<Bot[]>();
    public static UnityEvent OnFlaskControllerInitialized = new UnityEvent();
    public static UnityEvent OnNewFlaskAdded = new UnityEvent();
    public static UnityEvent OnFlaskFilledByOneColor = new UnityEvent();

    public static void SendFlaskInitialized()
    {
        OnFlasksInitialized.Invoke();
    }

    public static void SendBotsInitialized(Bot[] bots)
    {
        OnBotsInitialized.Invoke(bots);
    }
    public static void SendFlaskControllerInitialized()
    {
        OnFlaskControllerInitialized.Invoke();
    }
    public static void SendFlaskFilledByOneColor()
    {
        OnFlaskFilledByOneColor.Invoke();
    }
    public static void SendNewFlaskAdded()
    {
        OnNewFlaskAdded.Invoke();
    }

}
