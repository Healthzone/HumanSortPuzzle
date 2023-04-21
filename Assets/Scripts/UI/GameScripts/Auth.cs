using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Auth : MonoBehaviour
{
    [SerializeField] private GameObject authOffGameObject;
    [SerializeField] private GameObject authOnGameObject;
    [SerializeField] private GameObject authBtnGameObject;

    //private void OnEnable() => YandexGame.GetDataEvent += CheckAuthStatus;

    //private void OnDisable() => YandexGame.GetDataEvent -= CheckAuthStatus;

    //private void Start()
    //{
    //    if(YandexGame.SDKEnabled)
    //        //CheckAuthStatus();
    //}

    public void AuthPlayer()
    {
        YandexGame.AuthDialog();
        //CheckAuthStatus();
    }
    private void Update()
    {
        if (YandexGame.SDKEnabled && YandexGame.auth)
        {
            authOffGameObject.SetActive(false);
            authOnGameObject.SetActive(true);
            authBtnGameObject.SetActive(false);
        }
        else
        {
            authOffGameObject.SetActive(true);
            authOnGameObject.SetActive(false);
            authBtnGameObject.SetActive(true);
        }
    }
    //public void CheckAuthStatus()
    //{
    //    if (YandexGame.auth && YandexGame.playerName != "")
    //    {
    //        authOffGameObject.SetActive(false);
    //        authOnGameObject.SetActive(true);
    //        authBtnGameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        authOffGameObject.SetActive(true);
    //        authOnGameObject.SetActive(false);
    //        authBtnGameObject.SetActive(true);
    //    }
    //}
}
