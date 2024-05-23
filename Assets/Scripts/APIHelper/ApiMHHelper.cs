using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiMHHelper : MonoBehaviour
{
    private static ApiMHHelper _instance;
    public static ApiMHHelper Instance { 
        get {
            return _instance;
    }}

    private ApiMHHelper()
    {
        _instance = this;
    }

    [Header("Api Call Setup")]
    public string BaseUrl = "";

    public void MakeArmoursApiCall(Action<List<Armour>> onSuccess)
    {
        string url = BaseUrl + "armor";
        IEnumerator apiCall = ApiHelper.Get<List<Armour>>(url, onSuccess, OnFailure);
        StartCoroutine(apiCall);
    }

    public void MakeItemsApiCall(Action<List<Item>> onSuccess)
    {
        string url = BaseUrl + "items";
        IEnumerator apiCall = ApiHelper.Get<List<Item>>(url, onSuccess, OnFailure);
        StartCoroutine(apiCall);
    }

    public void MakeMonstersApiCall(Action<List<Monster>> onSuccess)
    {
        string url = BaseUrl + "monsters";
        IEnumerator apiCall = ApiHelper.Get<List<Monster>>(url, onSuccess, OnFailure);
        StartCoroutine(apiCall);
    }

    public void MakeArmourTextureApiCall(string url, Action<Texture> onSuccess)
    {
        IEnumerator apiCall = ApiHelper.GetTexture(url, onSuccess, OnFailure);
        StartCoroutine(apiCall);
    }

    private void OnFailure(Exception exception)
    {
        Debug.LogError("[!] Call Error: " + exception);
    }
}
