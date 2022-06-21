using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PuanSistemi3 : MonoBehaviour
{

    public TextMeshProUGUI skor_TMP;
    public int skor, enyuksekskor;
    float gskor;

    void Start()
    {
        StartCoroutine(skorumuGetir());
    }

    
    void Update()
    {
        
    }

    IEnumerator skorumuGetir()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "skorumugetir");
        form.AddField("nick", PlayerPrefs.GetString("nick"));



        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/adimadimtr/veritabani_islemler.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                skor =int.Parse( www.downloadHandler.text);
                skor_TMP.text = "" + skor;
                



            }
        }
    }
    IEnumerator skorumuGuncelle()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "skorumuGuncelle");
        form.AddField("nick", PlayerPrefs.GetString("nick"));
        form.AddField("skor", skor);



        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/adimadimtr/veritabani_islemler.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);


            }
        }
    }
    public void puanVer(float saniye)
    {
        gskor += saniye * 5;
        skor += (int)gskor;
        gskor = 0;
        skor_TMP.text = "" + skor;
       
        StartCoroutine(skorumuGuncelle());
    }
    public void yanlispuanVer(float saniye)
    {
        gskor += saniye * 2;
        skor -= (int)gskor;
        gskor = 0;
        skor_TMP.text = "" + skor;
        
        StartCoroutine(skorumuGuncelle());
    }

}
