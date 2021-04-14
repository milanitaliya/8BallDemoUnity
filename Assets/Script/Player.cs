using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    public GameObject indicationObject;
    public GameObject playerObject;
    public  bool move;
    public int forceSpeed;
    void Start()
    {
        move = true;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (move)
            {
                indicationObject.SetActive(true);
                playerRigidBody.isKinematic = false;
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    playerRigidBody.gameObject.transform.forward = ( hit.point- playerRigidBody.gameObject.transform.position);
                }
                float dist = Vector3.Distance(hit.point, playerRigidBody.gameObject.transform.position);
             
                if (dist <4&&dist>0)
                {
                    forceSpeed = 150;
                }
                else if (dist < 9 && dist >4 )
                {
                    forceSpeed = 250;
                }
                else
                {
                    forceSpeed = 350;
                }              
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            indicationObject.SetActive(false);
            if (move)
            {
                playerRigidBody.isKinematic = false;
                move = false;
                playerRigidBody.AddForce(playerRigidBody.gameObject.transform.forward * forceSpeed);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            indicationObject.SetActive(true);
        }
    }
    void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.name != "Player")
        {
            StopAllCoroutines();
            StartCoroutine(StopPlayer());
        }   
    }
    IEnumerator StopPlayer()
    { 
        yield return new WaitForSeconds(2f);
        move = true;
    }
}
