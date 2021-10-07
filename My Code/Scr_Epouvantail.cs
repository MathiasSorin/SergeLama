using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Epouvantail : MonoBehaviour
{
    public Scr_Epouvantail_Seau epouvantailSeau;
    Transform tr;

    public manRandomObjectives manRandomObjectivesS1;

    private void Start()
    {
        tr = this.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Scr_Seau temp = collision.gameObject.GetComponent<Scr_Seau>();
        if(temp != null)
        {
            Destroy(collision.gameObject);
            Transformation();
        }
    }

    public void Transformation()
    {

        Instantiate(epouvantailSeau, tr.position, Quaternion.identity);
        manRandomObjectivesS1.mesObjectifs[1].isDone = true;
        Destroy(this.gameObject);
    }

}
