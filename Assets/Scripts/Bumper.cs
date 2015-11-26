using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {
    Vector3 bumperPosition;
    Vector3 playerPosition;
    Vector3 dir;

    public Animator m_Animator;

    public float puissance = 100;
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
        else
        {
            puissance = 50;
        }
        
        bumperPosition = new Vector3(transform.position.x, transform.position.y, 0.0f);
	}
	
    void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.tag == "Player")
        {

            m_Animator.SetTrigger("Bump");
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
    }

    IEnumerator WaitBump(float time, GameObject player)
    {
        yield return new WaitForSeconds(time);
        player.GetComponent<Player>().isBumping = false;
    }
}
