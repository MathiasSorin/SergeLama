using UnityEngine;

/*
Author: Mathias Sorin
Last revision: 27/03/2021
*/

public class Scr_MovingPlatform : MonoBehaviour
{
    public float platformSpeed;
    public float platformPause;
    public float platformSnap;

    public Transform[] points;
    private int point = 0;
    private Vector3 target;
    private GameObject centerParent;

    private void Start()
    {
        centerParent = new GameObject();
        centerParent.transform.parent = transform;
        if(points.Length > 0)
        {
            target = points[0].position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position != target)
        {
            MovePlatform();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = centerParent.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }

    void MovePlatform()
    {
        Vector3 heading = target - transform.position;
        transform.position += (heading / heading.magnitude) * platformSpeed * Time.deltaTime;
        if(heading.magnitude < platformSnap)
        {
            transform.position = target;
            Invoke("NextTarget", platformPause);
        }
    }

    void NextTarget()
    {
        ++point;
        if(point >= points.Length)
        {
            point = 0;
        }
        target = points[point].position;
    }
}
