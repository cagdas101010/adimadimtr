using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System;

public static class RastgeleSırala
{

    public static void Shuffle<T>(this IList<T> list)
    {

        int n = list.Count;
        while (n > 1)
        {


            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public class oyundongusu : MonoBehaviour
{
    
    public float zaman = 0;
    public Slider zamanslider;
    public Image fill;
    public Gradient gradient;
    [ Header("SORULAR")]
    public SorularList sorular;
    public List<int> SorularınSırasi;
    [Header("Soru Değişkenleri")]
    public TextMeshProUGUI soru_TMP;
    public Text A_T, B_T, C_T, D_T;
    public int soruno=0;
    BirimSoruModel birimsorumodel;
    public Color dogruRenk, yanlisRenk;
    public Image[] butonImages;

    AudioSource seskaynagi;
    public AudioClip dogrusesi, yanlisesi;
    public GameObject gameover_p;
    public bool oyunbasladimi;


    JokerSistemi JokerSistemi;
    PuanSistemi PuanSistemi;

    
    void Start()
    {

        Time.timeScale = 1;
        PuanSistemi = GetComponent<PuanSistemi>();
        seskaynagi = GetComponent<AudioSource>();
        JokerSistemi = GetComponent<JokerSistemi>();
        zaman = 15;
        

        StartCoroutine(sorulariGetir());
        soruno = 0;

    }
    void sorularıSormaSırası()
    {
        for (int i = 0; i < sorular.butunSorular.Count; i++)
        {
            SorularınSırasi.Add(i);
        }
        SorularınSırasi.Shuffle();
    }
    
    void Update()
    {
        if (oyunbasladimi)
        {
            if (zaman > 0)
            {
                zaman -= Time.deltaTime;
                zamanslider.value = zaman;
                fill.color = gradient.Evaluate(zaman / birimsorumodel.saniye);

            }
            else
            {
                gameOver();
            }

        }
          
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Anamenu_B();
        }
            

    }

    private void gameOver()
    {
        gameover_p.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator sorulariGetir()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "sorular");



        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/adimadimtr/veritabani_islemler.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                Debug.Log("Başarılı!");
                sorular = JsonUtility.FromJson<SorularList>(www.downloadHandler.text);
                oyunbasladimi = true;
                sorularıSormaSırası();
                soruSor(soruno);
            }
        }
    }
    public void soruSor(int soruno)
    {



        birimsorumodel = sorular.butunSorular[SorularınSırasi[soruno]];

        soru_TMP.text = birimsorumodel.soru;
        A_T.text = birimsorumodel.a_cevap;
        B_T.text = birimsorumodel.b_cevap;
        C_T.text = birimsorumodel.c_cevap;
        D_T.text = birimsorumodel.d_cevap;

        zaman = birimsorumodel.saniye;
        zamanslider.maxValue = birimsorumodel.saniye;
        ciftcevap = false;

        JokerSistemi.sifirla();


    }
    public void kontrolEt(int basilancevap)
    {

        birimsorumodel = sorular.butunSorular[SorularınSırasi[soruno]];
        if (birimsorumodel.dogrucevap == basilancevap)
        {
            Debug.Log("Doğru!");
            seskaynagi.PlayOneShot(dogrusesi);
           
            PuanSistemi.puanVer(zaman);
            soruno++;
            soruSor(soruno);
        }
        else
        {
            Debug.Log("Yanlış!");
            seskaynagi.PlayOneShot(yanlisesi);
            PuanSistemi.yanlispuanVer(zaman);

            JokerSistemi.secenekler[basilancevap - 1].GetComponent<Image>().color = JokerSistemi.yanlisRenk;


            if (!ciftcevap)
            {
               JokerSistemi.secenekler[birimsorumodel.dogrucevap - 1].GetComponent<Image>().color = JokerSistemi.dogruRenk;
               gameOver();


            }

            else
            {
                ciftcevap = false;
            }
        }


    }
    public void yuzde50Kullan()
    {
        JokerSistemi.yuzdeliJokerKullan(birimsorumodel.dogrucevap, "50");
        JokerSistemi.jokerlerim[0].interactable = false;

    }
    
    public void paskullan()
    {
        soruno++;
        soruSor(soruno);
        JokerSistemi.jokerlerim[1].interactable = false;

    }
    
    public void DogruCevapKullan()
    {
        JokerSistemi.secenekler[birimsorumodel.dogrucevap - 1].GetComponent<Image>().color = JokerSistemi.dogruRenk;
        JokerSistemi.jokerlerim[2].interactable = false;
    }
    
    public bool ciftcevap;

    public void ciftcevapKullan()
    {
        ciftcevap = true;
        JokerSistemi.jokerlerim[3].interactable = false;

    }
    
    public void YenidenOyna_B()
    {
        Time.timeScale = 1;
        SorularınSırasi.Shuffle();
        soruno = 0;
        soruSor(soruno);
        JokerSistemi.JokerleriYenile();
        gameover_p.SetActive(false);
    }

    public void Anamenu_B()
    {
        SahneDegistirici.sahnedegis(1);
    }
}
