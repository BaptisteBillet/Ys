using UnityEngine;
using System.Collections;

public class MoveBumper : MonoBehaviour {
    Ray ray;
    RaycastHit hit;
    bool onBumper = false;
    public int collisionCount;
    Vector3 collisionPosition;
	// Use this for initialization
	void Start () {
        onBumper = false;
	}

    void OnCollisionEnter(Collision col)
    {
        
        if (onBumper)
        {
            collisionCount++;
            transform.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        
        
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("triggerEnter");
        if(col.tag =="bumper")
        {
            collisionCount++;
            transform.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "bumper")
        {
            collisionCount--;
            if (collisionCount == 0)
            {
                transform.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }

    void OnCollisionExit()
    {
        if (onBumper)
        {
            collisionCount--;
            if (collisionCount == 0)
            {
                transform.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        
	    if(Input.GetMouseButton(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<MoveBumper>())
                {
                    onBumper = true;
                    GameManagerScript.instance.panelbumper.SetActive(false);
                    Vector3 bumperPos = Input.mousePosition;
                    bumperPos = Camera.main.ScreenToWorldPoint(bumperPos);
                    bumperPos.z = 0.0f;

                    this.transform.position = bumperPos;
                    
                    
                }
            }
            
        }
        else if (Input.GetMouseButtonUp(0) && onBumper && collisionCount ==0)
        {
            onBumper = false;
            GameManagerScript.instance.panelbumper.SetActive(true);
        }
        
        
	}

    public void validMove()
    {
        GameObject player = GameManagerScript.instance.getPlayer(GameManagerScript.instance.getCurrentID());
        GameManagerScript.instance.panelbumper.SetActive(false);
        player.GetComponent<Player>().actionReady = false;
        transform.GetComponent<MoveBumper>().enabled = false;
    }
}
