using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Boite_A_Patate : MonoBehaviour
{
    public manObjectifs manObjectifsS1;
    private void OnCollisionEnter(Collision collision)
    {
        Scr_Patate temp = collision.gameObject.GetComponent<Scr_Patate>();
        if (temp != null)
        {
            manObjectifsS1.mesObjectifsPrincipaux[8].isDone = true;
        }
    }
}
