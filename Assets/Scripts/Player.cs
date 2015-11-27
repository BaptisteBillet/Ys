using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public partial class Player : MonoBehaviour {
    public int ID;
    public int health =100;
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
    public GameObject m_ejectionZone;
    bool uiTurnIsOn = false;

    public GameObject lifeBar;

    public Animator m_CercleAnimator;
    public Animator m_LifeAnimator;

    private int m_Damage;

    //Particle effects
    [SerializeField]
    private GameObject wallCollisionEffect;
    [SerializeField]
    private GameObject bumperCollisionEffect;
    [SerializeField]
    private GameObject playerCollisionEffect;
    [SerializeField]
    private GameObject destroyedEffect;

    public GameObject powerUpEffect;
    public GameObject currentTerrain;
    public GameObject startTerrain;

	// Use this for initialization

    public Image m_Animal;
    public Image m_Background;
    public Sprite m_Lion;
    public Sprite m_Bear;
    public Color m_LionColor;
    public Color m_BearColor;

    public void ChangeAnimal(bool isLion)
    {
        if(isLion)
        {
            m_Animal.sprite = m_Lion;
            m_Background.color = m_LionColor;
        }
        else
        {
            m_Animal.sprite = m_Bear;
            m_Background.color = m_BearColor;
        }
    }


	void Start () {
        health = 100;
        Damage = 10;
        uiTurnIsOn = false;
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
        powerUpEffect.SetActive(false);
        startTerrain = currentTerrain;
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
        if (startTypeZone != currentTypeZone && playerSpeed > 0)
        {
            if (startTerrain != null&& !GameManagerScript.instance.arePlayerOnSameTerrain())
                startTerrain.GetComponent<TerrainEffectManager>().KillEffect();
        }

        //ADD
        if (needValidMove && GameManagerScript.instance.isMouseOnPlayer(ID) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("END Sling VALID");
            needValidMove = false;
            validatingEnd();
			SoundManagerEvent.emit(SoundManagerType.PlacePlayer);
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

                isMovingBump = true;
                GameManagerScript.instance.Bumper.GetComponent<MoveBumper>().enabled = true;
            }
            else
            {
                actionReady = false;
                GameManagerScript.instance.Bumper.GetComponent<MoveBumper>().enabled = false;
                
            }
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (startTypeZone == TypeZone.TerrainType.FOREST && !collision.collider.GetComponent<Player>())
        {
            powerUpEffect.SetActive(true);
        }
        if (startTypeZone == TypeZone.TerrainType.PLAIN && !collision.collider.GetComponent<Player>())
        {
            powerUpEffect.SetActive(false);
        }
        if (collision.gameObject.tag == "Wall")
        {

			SoundManagerEvent.emit(SoundManagerType.BumpWall);

            foreach (ContactPoint contact in collision.contacts)
            {
                Object effect = Instantiate(wallCollisionEffect, contact.point, Quaternion.LookRotation(contact.normal));
                Destroy(effect, 1f);
            }
        }

        if (collision.gameObject.tag == "bumper")
        {
			SoundManagerEvent.emit(SoundManagerType.BumpBumper);

            foreach (ContactPoint contact in collision.contacts)
            {
                Object effect = Instantiate(bumperCollisionEffect, contact.point, Quaternion.LookRotation(contact.normal));
                Destroy(effect, 1f);
            }
        }
        if((GameManagerScript.instance.getCurrentID()== this.ID)&&(collision.collider.GetComponent<Player>())&&attackReady)
        {
			SoundManagerEvent.emit(SoundManagerType.BumpPlayer);

            foreach (ContactPoint contact in collision.contacts)
            {
                Object effect = Instantiate(playerCollisionEffect, contact.point, Quaternion.LookRotation(contact.normal));
                Destroy(effect, 1f);
            }
            powerUpEffect.SetActive(false);
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
        startTerrain = currentTerrain;
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        foreach (Transform child in transform)
        {
            //child.gameObject.SetActive(turn);
            transform.GetChild(0).gameObject.SetActive(turn);
            transform.GetChild(1).gameObject.SetActive(turn);
        }
        if (turn)
        {
            if (!uiTurnIsOn)
            {
                m_CercleAnimator.SetTrigger("Open");
                uiTurnIsOn = true;
            }
            
            m_LifeAnimator.SetTrigger("Close");

            m_ejectionZone.GetComponent<Image>().enabled = true;
            //transform.GetComponentInChildren<SpriteRenderer>().enabled = true;

            
            startTypeZone = currentTypeZone;
            countCollision = 0;
            if (startTypeZone == TypeZone.TerrainType.PLAIN)
            {
                powerUpEffect.SetActive(true);
            }
        }
        else
        {
            m_ejectionZone.GetComponent<Image>().enabled = false;
            if (uiTurnIsOn)
            {
                m_CercleAnimator.SetTrigger("Close");
                uiTurnIsOn = false;
            }
            
            m_LifeAnimator.SetTrigger("Open");
            isMovingBump = turn;

            if (currentTerrain != null)
            {
                currentTerrain.GetComponent<TerrainEffectManager>().ActivateEffect();

				switch(startTypeZone)
				{
				case TypeZone.TerrainType.BUMPER:
					CanvasMagicText.instance.ChangeText("Move the Bumper");
					CanvasMagicText.instance.AppearText();
					CanvasMagicText.instance.HideText(3);
					break;

				case TypeZone.TerrainType.FOREST:
					CanvasMagicText.instance.ChangeText("Bounce for Double damage");
					CanvasMagicText.instance.AppearText();
					CanvasMagicText.instance.HideText(3);
					break;

				case TypeZone.TerrainType.PLAIN:
					CanvasMagicText.instance.ChangeText("Direct Hit for Double damage");
					CanvasMagicText.instance.AppearText();
					CanvasMagicText.instance.HideText(3);
					break;

				}


            }

            if (currentTypeZone == TypeZone.TerrainType.MOUNTAIN)
            {
				CanvasMagicText.instance.ChangeText("Your have a Shield!");
				CanvasMagicText.instance.AppearText();
				CanvasMagicText.instance.HideText(3);

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
			CanvasMagicText.instance.ChangeText("Shield Broke!");
			CanvasMagicText.instance.AppearText();
			CanvasMagicText.instance.HideText(3);

			SoundManagerEvent.emit(SoundManagerType.CrashGlass);

        }
        else
        {

			if(ID==1)
			{
				SoundManagerEvent.emit(SoundManagerType.DamageBear);
			}
			else
			{
				SoundManagerEvent.emit(SoundManagerType.DamageLion);
			}


            // get dmg
            if((health > damage)&&(damage > 0))
            {
                
                m_Damage = damage;
                m_LifeAnimator.SetTrigger("Open");
                Invoke("AssumeDamage", 0.26f);
                
            }
            else
            {

                GameManagerScript.instance.endGame(ID);
                Object effect = Instantiate(destroyedEffect, this.transform.position, Quaternion.identity);
                Destroy(effect, 2f);
                
                // mort
            }
        }
    }

    private void AssumeDamage()
    {
        health -= m_Damage;
        lifeBar.GetComponent<Image>().fillAmount = health / 100.0f;

                
    }

}
