﻿using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {
    Vector3 bumperPosition;
    Vector3 playerPosition;
    Vector3 dir;

    public Animator m_Animator;

    [SerializeField]
    private GameObject bumpEffect;

    public float puissance = 10;
	// Use this for initialization
	void Start () {
        if (transform.gameObject.name == "Bumper")
        {
            puissance = 100;
        }
        else if (transform.gameObject.name == "MovableBumper")
        {
            puissance = 100;
        }
        
        bumperPosition = new Vector3(transform.position.x, transform.position.y, 0.0f);
	}
	
    void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.tag == "Player")
        {

            m_Animator.SetTrigger("Bumper");
            
            playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0.0f);
            Bump(player.gameObject, playerPosition);
        }
    }

    void Bump(GameObject player, Vector3 playerPos)
    {
        player.GetComponent<Player>().isBumping = true;
        dir = playerPos -bumperPosition;
        player.gameObject.GetComponent<Rigidbody>().AddForce(dir * puissance);
        StartCoroutine(WaitBump(0.2f, player));


        Object effect = Instantiate(bumpEffect, this.transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    IEnumerator WaitBump(float time, GameObject player)
    {
        yield return new WaitForSeconds(time);
        player.GetComponent<Player>().isBumping = false;
    }
}
