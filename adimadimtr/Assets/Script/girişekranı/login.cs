using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class login : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine("Coutdown");
    }

    private IEnumerator Coutdown()
    {
        yield return new WaitForSeconds(5);
        Application.LoadLevel(1);

    }
}
