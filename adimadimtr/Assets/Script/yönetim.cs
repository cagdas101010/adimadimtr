using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yönetim : MonoBehaviour
{
    public GameObject kayit_P, anamenu_P;

    void Start()
    {

        if (!PlayerPrefs.HasKey("kayitDurumu"))
        {
            kayit_P.SetActive(true);
            anamenu_P.SetActive(false);
           

        }
        else

        {

            kayit_P.SetActive(false);
            anamenu_P.SetActive(true);

        }

    }

    public void devam_B()
    {
        PlayerPrefs.SetInt("kayitDurumu", 1);
        kayit_P.SetActive(false);
        anamenu_P.SetActive(true);


    }
    public void baslangic_B()
    {
        SahneDegistirici.sahnedegis(2);
    }
    public void skor_B()
    {
        SahneDegistirici.sahnedegis(3);
    }

    public void orta_B()
    {
        SahneDegistirici.sahnedegis(4);
    }
    public void zor_B()
    {
        SahneDegistirici.sahnedegis(5);
    }
    public void genel_B()
    {
        SahneDegistirici.sahnedegis(6);
    }
}
