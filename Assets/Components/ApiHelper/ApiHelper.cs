using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Unity.Serialization.Json;

public class ApiHelper : MonoBehaviour
{
    public static IEnumerator Get(
        string url,
        Action<string> onSuccess,
        Action<Exception> onFailure
        )
    {
        // Preparamos la petición
        UnityWebRequest request = UnityWebRequest.Get(url);

        // Enviamos la consulta mediente una corrutina
        yield return request.SendWebRequest();

        // Gestionamos la respuesta
        //      FALLO --> Lanzamos la función de OnFailure y paramos la ejecución de la función
        //      OK --> Pasamos la respuesta a texto y se la pasamos a la función OnSuccess
        if (request.result != UnityWebRequest.Result.Success)
        {
            onFailure(new Exception(request.error));
            yield break;
        }

        string text = request.downloadHandler.text;

        onSuccess(text);
    }

    public static IEnumerator Get<T>(
        string url,
        Action<T> onSuccess,
        Action<Exception> onFailure
        )
    {
        return Get(url, jsonText =>
        {
            try
            {
                T result = JsonSerialization.FromJson<T>(jsonText);
                onSuccess(result);
            }
            catch (Exception e)
            {
                onFailure(e);
            }
        }, onFailure);
    }

    public static IEnumerator GetTexture(
        string url,
        Action<Texture> onSuccess,
        Action<Exception> onFailure)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onFailure(new Exception(request.error));
            yield break;
        }

        DownloadHandlerTexture handler = (DownloadHandlerTexture)request.downloadHandler;
        onSuccess(handler.texture);
    }
}
