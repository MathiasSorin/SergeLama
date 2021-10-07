using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerNappe : MonoBehaviour
{
    public manObjectifs manObjectifsS3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Scr_Nappe>() != null)
        {
            manObjectifsS3.mesObjectifsPrincipaux[0].isDone = true;
        }

    }
}
