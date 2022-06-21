using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneDegistirici : MonoBehaviour
{
    public static void sahnedegis(int sahne_id)
    {
        SceneManager.LoadScene(sahne_id);

    }
}
