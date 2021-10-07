using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Trigger_Entree : MonoBehaviour
{
    public manObjectifs manObjectifsS1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Scr_Avatar>() != null)
        {
            manObjectifsS1.mesObjectifsPrincipaux[1].isDone = true;
        }

    }
}
