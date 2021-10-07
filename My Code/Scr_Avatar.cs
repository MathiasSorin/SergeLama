using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Author: Mathias Sorin
Last revision: 27/03/2021
*/

public class Scr_Avatar : MonoBehaviour
{
    private float moveHorizontal;
    private float moveVertical;
    private bool jump;
    private bool grab = false;
    private bool spit = false;
    private bool kick = false;

    private bool isHolding = false;
    private bool canSpit = true;
    private bool canKick = true;
    private Collider objectHeld;

    public float playerSpeed = 5f;
    public float jumpStrenght = 5f;

    private Rigidbody rigidbodyComponent;
    public Transform groundCheckTransform;
    public Transform mouthTransform;
    public GameObject spitProjectile;

    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            grab = true;
        }
        if(Input.GetMouseButton(0))
        {
            spit = true;
        }
        if (Input.GetMouseButton(1))
        {
            kick = true;
        }
    }

    //Update is fixed just like time delta time
    void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(moveHorizontal * playerSpeed, rigidbodyComponent.velocity.y, rigidbodyComponent.velocity.z);
        rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, rigidbodyComponent.velocity.y, moveVertical * playerSpeed);

        if (spit && !isHolding && canSpit)
        {
            var tempspit = Instantiate(spitProjectile, mouthTransform.position, Quaternion.identity);
            tempspit.GetComponent<Rigidbody>().AddForce(groundCheckTransform.forward * 1000);
            spit = false;
            canSpit = false;
            Invoke("spitRate", 2);
        }
        else if(spit && isHolding && canSpit)
        {
            var temp = objectHeld.GetComponent<Mordable>();
            if(temp != null)
            {
                grab = false;
                isHolding = false;
                objectHeld.transform.parent = null;
                objectHeld.enabled = true;
                objectHeld.GetComponent<Rigidbody>().isKinematic = false;
                temp.Mordre();
                objectHeld = null;
            }
        }
        else
        {
            spit = false;
        }

        if (grab && !isHolding)
        {
            Collider[] colliderList = Physics.OverlapSphere(mouthTransform.position, 2f);
            foreach(var hitCollider in colliderList)
            {
                var temp = hitCollider.GetComponent<Agrippable>();
                if (temp != null)
                {
                    hitCollider.enabled = false;
                    hitCollider.GetComponent<Rigidbody>().isKinematic = true;
                    hitCollider.transform.parent = mouthTransform;
                    hitCollider.transform.position = mouthTransform.transform.position;
                    grab = false;
                    isHolding = true;
                    objectHeld = hitCollider;
                    return;
                }
                else
                {
                    grab = false;
                }
            }
        }
        else if(grab && isHolding)
        {
            grab = false;
            isHolding = false;
            objectHeld.transform.parent = null;
            objectHeld.enabled = true;
            objectHeld.GetComponent<Rigidbody>().isKinematic = false;
            objectHeld = null;
        }

        if (kick && canKick)
        {
            Collider[] colliderList = Physics.OverlapSphere(groundCheckTransform.position, 5f);
            foreach (var hitCollider in colliderList)
            {
                var temp = hitCollider.GetComponent<Rigidbody>();
                if(temp != null)
                {
                    if(hitCollider.gameObject.tag == "Breakable")
                    {
                        hitCollider.GetComponent<Scr_BreakableWall>().Break();
                    }
                    hitCollider.GetComponent<Rigidbody>().AddExplosionForce(15f, groundCheckTransform.position, 5f, 2f, ForceMode.Impulse);
                }
            }
            kick = false;
            canKick = false;
            Invoke("kickRate", 2);
        }
        else
        {
            kick = false;
        }

        if (jump && Physics.OverlapSphere(groundCheckTransform.position, 1.5f).Length != 1)
        {
            rigidbodyComponent.AddForce(Vector3.up * jumpStrenght, ForceMode.VelocityChange);
            jump = false;
        }
    }

    void spitRate()
    {
        canSpit = true;
    }

    void kickRate()
    {
        canKick = true;
    }
}
