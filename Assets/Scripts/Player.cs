using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public int ID;
    public int health;
    public bool isShieldUp;
    public TypeZone.TerrainType currentTypeZone;
    public TypeZone.TerrainType startTypeZone;
    public int Damage;
    public bool actionReady;
    public bool attackReady;
    public bool doubleTap;
    public bool isMaybeDoubleTapping = false;
    public bool enddoubleTap = true;
    public GameObject otherPlayer;
    public bool attackCac = false;
    public bool isBumping = false;
    public bool isMoving = false;
    public bool isMovingBump = false;
    public float playerSpeed;
    Vector3 velocity;
    public int countCollision = 0;
    public bool collideAtStart = false;
    bool needValidMove = false;
	// Use this for initialization


	void Start () {
        Damage = 1;
        needValidMove = false;
        countCollision = 0;
        isMoving = false;
        doubleTap = false;
        isMaybeDoubleTapping = false;
        isShieldUp = false;
        actionReady = false;
        isBumping = false;
        isMovingBump = false;
        startTypeZone = currentTypeZone;
	}
	
	// Update is called once per frame
	void Update () {
        velocity = transform.GetComponent<Rigidbody>().velocity;
        playerSpeed = velocity.magnitude;
        if (playerSpeed != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //ADD
        if (needValidMove && GameManagerScript.instance.isMouseOnPlayer(ID) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("END Sling VALID");
            needValidMove = false;
            validatingEnd();
        }
        //END
        if(ID == GameManagerScript.instance.currentId )
        {
            if (playerSpeed <= 0.5f && !transform.GetComponentInChildren<SlingShot>().playing && transform.GetComponentInChildren<SlingShot>().action && !isBumping && !attackCac)
            {
                transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                needValidMove = true;
                Debug.Log("END Sling");
                transform.GetComponentInChildren<ejection>().ejectionAction = true;
                validatingEnd();
            }
            if (attackCac && GameManagerScript.instance.getOtherPlayer(ID).GetComponent<Player>().playerSpeed <= 0.5f)
            {
                // Debug.Log("fin cac");
                GameManagerScript.instance.getOtherPlayer(ID).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                doubleTap = false;
                if (GameManagerScript.instance.isMouseOnPlayer(GameManagerScript.instance.getOtherPlayer(ID).GetComponent<Player>().ID) && Input.GetMouseButtonDown(0))
                {
                    transform.GetComponentInChildren<SlingShot>().action = true;
                    Debug.Log("END CAC");
                    attackCac = false;
                    validatingEnd();
                    

                }
            }
            
            
        }
	}

    //added
    void validatingEnd()
    {
        if (!needValidMove)
        {
            transform.GetComponentInChildren<ejection>().ejectionAction = false;
            transform.GetComponentInChildren<SlingShot>().action = false;
            if (currentTypeZone == TypeZone.TerrainType.BUMPER)
            {
                transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                Debug.Log("bump2");
                isMovingBump = true;
                GameManagerScript.instance.Bumper.GetComponent<MoveBumper>().enabled = true;
            }
            else
            {
                Debug.Log("end2");
                actionReady = false;
                GameManagerScript.instance.Bumper.GetComponent<MoveBumper>().enabled = false;
                
            }
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if((GameManagerScript.instance.getCurrentID()== this.ID)&&(collision.collider.GetComponent<Player>())&&attackReady)
        {
            if (startTypeZone == TypeZone.TerrainType.PLAIN && countCollision == 0)
            {
                collision.collider.GetComponent<Player>().takeDamage(this.Damage*2);
            }
            else if (startTypeZone == TypeZone.TerrainType.FOREST && countCollision != 0)
            {
                collision.collider.GetComponent<Player>().takeDamage(this.Damage*2);
            }
            else
            {
                collision.collider.GetComponent<Player>().takeDamage(this.Damage);
            }
            attackReady = false;
        }
        countCollision++;
    }

    /**********modif collision start****************/
    void OnCollisionStay(Collision collision)
    {
        if (GameManagerScript.instance.state == GameManagerScript.STATE.START)
        {
            Vector3 direction = transform.position - collision.transform.position;
            GameManagerScript.instance.MovePlayerIfCollide(direction, transform.gameObject);
        }
        
    }
    /***********************************************/
    public bool turnEnded()
    {
        return !actionReady;
    }
    
    public void setTurn(bool turn)
    {
        needValidMove = false;
        actionReady = turn;
        attackReady = turn;
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(turn);
        }
        if (turn)
        {
            transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
            startTypeZone = currentTypeZone;
            countCollision = 0;
        }
        else
        {
            isMovingBump = turn;
            if (currentTypeZone == TypeZone.TerrainType.MOUNTAIN)
            {
                isShieldUp = true;
            }
        }
        

    }


    public void setID(int id)
    {
        ID = id;
		SlingShot ss= gameObject.GetComponentInChildren<SlingShot>();
		ss.myID = id;
    }

    public int getLife()
    {
        return health;
    }

    public void takeDamage(int damage)
    {
        if(isShieldUp)
        {
            // no dmg -> animation de blockage ?
            isShieldUp = false;
        }
        else
        {
            // get dmg
            if((health > damage)&&(damage > 0))
            {
                health -= damage;
                
            }
            else
            {
                Debug.Log("Player "+ID+" lost");
                // mort
            }
            HUDManager.Instance.updateLife(this.ID, health);
        }
    }



}
