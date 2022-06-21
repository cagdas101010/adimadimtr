using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class kayitol : MonoBehaviour
{
    public TMP_InputField nick_IF;
    public TextMeshProUGUI hata_T;
    public GameObject devam_B;
    

    void Start()
    {
        devam_B.SetActive(false);
        hata_T.gameObject.SetActive(false);
    }


    void Update()
    {
        
    }


    public void kontrol()
    {
        if (netkontrol.internet)
        {
            if (!nick_IF.text.Equals(""))
            {
                StartCoroutine(KayitOl());


            }
            else
            {


                textYazdir("bos birakmayin!");

            }
        }
        else
        {


            textYazdir("internet baglantisi yok!");

        }
    }


        IEnumerator KayitOl()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "kayitol");
        form.AddField("nick", nick_IF.text);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/adimadimtr/veritabani_islemler.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                textYazdir(www.downloadHandler.text);
                if (www.downloadHandler.text.Equals("Kayıt Başarılı!"))
                {
                    PlayerPrefs.SetString("nick", nick_IF.text);
                    nick_IF.interactable = false;
                    devam_B.SetActive(true);

                }
               
            }
        }
    }

    void textYazdir(string mesaj)
    {
        hata_T.gameObject.SetActive(true);
        hata_T.text = mesaj;
        Invoke("sifirla", 1.2f);

    }
    void sifirla()
    {
        hata_T.text = "";

    }

}
