using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public static UnityEvent OnFlasksInitialized = new UnityEvent();
    public static UnityEvent<Bot[], bool> OnBotsInitialized = new UnityEvent<Bot[], bool>();
    public static UnityEvent OnFlaskControllerInitialized = new UnityEvent();
    public static UnityEvent OnNewFlaskAdded = new UnityEvent();
    public static UnityEvent OnFlaskFilledByOneColor = new UnityEvent();
    public static UnityEvent OnFlaskSelected = new UnityEvent();
    public static UnityEvent<int> OnLevelEnd = new UnityEvent<int>();

    public static void SendFlaskInitialized()
    {
        OnFlasksInitialized.Invoke();
    }

    public static void SendBotsInitialized(Bot[] bots, bool restart = false)
    {
        OnBotsInitialized.Invoke(bots, restart);
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
    public static void SendLevelEnd(int animIndex)
    {
        OnLevelEnd.Invoke(animIndex);
    }
    public static void SendFlaskSelected()
    {
        OnFlaskSelected.Invoke();
    }

}
