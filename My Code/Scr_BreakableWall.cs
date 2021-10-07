using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Author: Mathias Sorin
Last revision: 27/03/2021
*/

public class Scr_BreakableWall : MonoBehaviour
{
    public GameObject breakableWalltrue;

    public void Break()
    {
        Instantiate(breakableWalltrue, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
