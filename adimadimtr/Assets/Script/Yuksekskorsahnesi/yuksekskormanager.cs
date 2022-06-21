using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class yuksekskormanager : MonoBehaviour
{
    public RectTransform content;
    public int kullanicisayisi;
    public YuksekSkorList yuksekskorlist;
    public GameObject kullaniciModel;

    void Start()
    {
        StartCoroutine(skorlariGetir());
    }

    
    void Update()
    {
        //ContentAyarla(120, kullanicisayisi);
    }

    private void ContentAyarla(float carpan, int kullanicisayisi)
    {
        content.sizeDelta = new Vector2(0, carpan * kullanicisayisi);
    }
    IEnumerator skorlariGetir()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "SkorlariGetir");



        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/adimadimtr/veritabani_islemler.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                
                yuksekskorlist = JsonUtility.FromJson<YuksekSkorList>(www.downloadHandler.text);
                kullanicilariDiz();
                
            }
        }
    }

    void kullanicilariDiz()
    {
        ContentAyarla(120, yuksekskorlist.butunSkorlar.Count);
        for (int i = 0; i < yuksekskorlist.butunSkorlar.Count; i++)
        {
            GameObject gecici = Instantiate(kullaniciModel);
            gecici.transform.SetParent(content.transform);
            gecici.transform.localScale = new Vector3(1,1,1);
            Image background_I = gecici.transform.GetChild(0).GetComponent<Image>();
            Image kullanici_I = background_I.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI kullaniciNick_TMP = background_I.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI kullaniciSkor_TMP = background_I.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI siralama_TMP = background_I.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            kullanici_I.sprite = Resources.Load<Sprite>("bayrak");
            YuksekSkorModel yuksekSkorModel = yuksekskorlist.butunSkorlar[i];
            kullaniciNick_TMP.text = yuksekSkorModel.KullaniciNick;
            kullaniciSkor_TMP.text = yuksekSkorModel.KullaniciSkor.ToString();
            siralama_TMP.text = "" + (i + 1);

        }
    }

    public void Anamenu_B()
    {
        SahneDegistirici.sahnedegis(1);
    }
   
}
