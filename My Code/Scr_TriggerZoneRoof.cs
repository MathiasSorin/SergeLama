using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerZoneRoof : MonoBehaviour
{
    public manRandomObjectives manRandomObjectivesS1;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Scr_Avatar>() != null)
        {
            manRandomObjectivesS1.mesObjectifs[2].isDone = true;
            Debug.Log("allo");
        }
        
    }
}
