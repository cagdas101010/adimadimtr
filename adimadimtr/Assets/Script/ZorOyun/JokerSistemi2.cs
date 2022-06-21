using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JokerSistemi2 : MonoBehaviour
{
    public Color orjinalRenk, dogruRenk2, yanlisRenk2;
    public List<string> ihtimaller;
    public GameObject[] secenekler;
    public Button[] jokerlerim2;

    static int oncekiTutulan = -1;


    public void JokerleriYenile()
    {
        for (int i = 0; i < jokerlerim2.Length; i++)
        {
            jokerlerim2[i].interactable = true;
        }
    }




    public void sifirla()
    {
        for (int i = 0; i < secenekler.Length; i++)
        {
            secenekler[i].SetActive(true);
            secenekler[i].GetComponent<Image>().color = orjinalRenk;
        }
        oncekiTutulan = -1;
    }

    string secilen;

    public void yuzdeliJokerKullan(int dogruCevap, string JokerTuru)
    {

        if (JokerTuru.Equals("25"))
        {
            ihtimaller = new List<string>()
            {
            "1","2","3",
            "0","2","3",
            "0","1","3",
            "0","1","2"
            };
        }
        else
        {
            ihtimaller = new List<string>()
            {
            "12","13","23",
            "02","03","23",
            "01","03","13",
            "01","02","12"
            };
        }

        if (oncekiTutulan == -1)
        {

            int random = 0;

            switch (dogruCevap)
            {
                case 1:
                    {//CEVAP A İSE
                        random = Random.Range(0, 3); //0-1-2
                        break;
                    }
                case 2:
                    {//CEVAP B İSE
                        random = Random.Range(3, 6); //3-4-5
                        break;
                    }
                case 3:
                    {//CEVAP C İSE
                        random = Random.Range(6, 9); //6-7-8
                        break;
                    }
                case 4:
                    {//CEVAP D İSE
                        random = Random.Range(9, 12); //9-10-11
                        break;
                    }
            }
            secilen = ihtimaller[random]; //2
            oncekiTutulan = random;
        }
        else
        {
            if (oncekiTutulan % 3 == 0)
            {
                secilen = ihtimaller[oncekiTutulan + 2]; //0->2
            }
            else if (oncekiTutulan % 3 == 1)
            {
                secilen = ihtimaller[oncekiTutulan]; //1->1 4->4
            }
            else if (oncekiTutulan % 3 == 2)
            {
                secilen = ihtimaller[oncekiTutulan - 2]; //5->3 2->0
            }
        }
        for (int i = 0; i < secilen.Length; i++)
        {
            string gecici = secilen.Substring(i, 1); //1 2
            secenekler[int.Parse(gecici)].SetActive(false);

        }


    }
}
