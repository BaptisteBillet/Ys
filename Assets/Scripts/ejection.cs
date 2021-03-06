﻿using UnityEngine;
using System.Collections;

public class ejection : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Player player;
    public float puissanceCAC = 70;
    int degat = 1;
    public float RangeAttackCAC = 2.6f;
    GameObject otherPlayer;
    bool isMaybeDoubleTapping = false;
    bool doubleTap = false;
    public bool ejectionAction = false;


    [SerializeField]
    private GameObject EjectionEffect;

    // Use this for initialization
    void Start()
    {
        ejectionAction = false;
        puissanceCAC = 100;
        RangeAttackCAC = 2.6f;
    }

    void OnMouseEnter()
    {
        Debug.Log("mouse over");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mousclick on cac");
        }
    }

    IEnumerator doubleTouch()
    {

        yield return new WaitForSeconds(.5f);

        isMaybeDoubleTapping = false;
    }

    void attackCAC()
    {
        
        otherPlayer = GameManagerScript.instance.getOtherPlayer(player.ID);
        doubleTap = true;
        if (Vector3.Distance(otherPlayer.transform.position, this.transform.position) < RangeAttackCAC + (otherPlayer.transform.localScale.x / 2)&&!ejectionAction && !transform.parent.GetComponentInChildren<SlingShot>().action)
        {

			if(player.ID==1)
			{
				SoundManagerEvent.emit(SoundManagerType.DoubleTapBear);
			}
			else
			{
				SoundManagerEvent.emit(SoundManagerType.DoubleTapLion);
			}



            //attackCac = true;
            otherPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            Vector3 direction = otherPlayer.transform.position - this.transform.position;
            otherPlayer.GetComponent<Rigidbody>().AddForce(direction * puissanceCAC);
            otherPlayer.GetComponent<Player>().takeDamage(degat);
            ejectionAction = true;
            //StartCoroutine(waiting(0.1f));


            Object effect = Instantiate(EjectionEffect, this.transform.position, Quaternion.identity);
            Destroy(effect, 2f);

        }
    }

    /* IEnumerator waiting(float time)
     {
         Debug.Log("YIELDDD");
         float currentTime =0;
         while (currentTime < time)
         {
             yield return new WaitForSeconds(0.05f);
         }
        
        
         Debug.Log(this.GetComponentInParent<Player>().attackCac);
     }*/

    public bool isOtherPlayerMoving()
    {
        return otherPlayer.GetComponent<Player>().isMoving;
    }



    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0) && GameManagerScript.instance.isMouseOnCurrentPlayer(this.GetComponentInParent<Player>().ID, out hit) && !transform.parent.GetComponent<Player>().isMovingBump)
        {

            player = hit.collider.GetComponent<Player>();

            // attack
            if (isMaybeDoubleTapping)
            {
                attackCAC();
                StopAllCoroutines();
                isMaybeDoubleTapping = false;
            }
            else
            {

                isMaybeDoubleTapping = true;
                StartCoroutine(doubleTouch());

            }
        }
        if (GameManagerScript.instance.getOtherPlayer(this.GetComponentInParent<Player>().ID) !=null && GameManagerScript.instance.getOtherPlayer(this.GetComponentInParent<Player>().ID).GetComponent<Player>().playerSpeed > 0.5f)
        {
            this.GetComponentInParent<Player>().attackCac = true;
        }

    }
}

