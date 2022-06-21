using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class netkontrol : MonoBehaviour
{
    

    public static bool internet;
    void Start()
    {
        StartCoroutine(internetdurum());
    }

    IEnumerator internetdurum()
    {
        WWWForm form = new WWWForm();


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/adimadimtr/netkontrol.txt", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                internet = false;
            }
            else
            {
                internet = true;
                Debug.Log("İnternete Bağlısınız");
            }

            yield return new WaitForSeconds(2);
            StartCoroutine(internetdurum());
        }
    }
}
