using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
    public enum STATE
    {
        MENU,
        START,
        RUN,
        END

    }; 

	static GameManagerScript mInst;
	static public GameManagerScript instance { get { return mInst; } }
	void Awake()
	{
		if (mInst == null) mInst = this;
		DontDestroyOnLoad(this);
	}

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Bumper;
    public GameObject SpawnPlayer;
    Player scriptP1;
    Player scriptP2;

    public GameObject PlayerPrefab;
    public STATE state;
    public bool p1Turn;
	public int currentId;
    bool launch= false;

    /****UI***/

	// Use this for initialization
	void Start () {
        state = STATE.START;
        Player1 = null;
        Player2 = null;
        launch = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Player1 != null && Player2 != null && state == STATE.START)
        {
            if (!launch)
            {
                StartCoroutine(LaunchGame(0.2f));
            }
            
        } 
	    switch(state)
        {
            case STATE.MENU:
                break;
            case STATE.START:
				playerStartPosition();
                break;
            case STATE.RUN:
                gameRunning();
                break;
            case STATE.END:
                break;
            default: 
                break;
        }
	}

    /************ modif **************/
    IEnumerator LaunchGame(float time)
    {
        launch = true;
        float currentTime = 0;
        /*******choisi le joueur qui commence *****/
        int rand = Random.Range(1, 3);
        GameObject StartPlayer;
        GameObject OtherPlayer;

        if (rand == 1)
        {
            StartPlayer = Player1;
            OtherPlayer = Player2;
            p1Turn = false;
        }
        else
        {
            StartPlayer = Player2;
            OtherPlayer = Player1;
            p1Turn = true;
        }
        /*************/
        while(currentTime < time)
        {
            if (currentTime == 0.1f)
            {
                OtherPlayer.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            currentTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        
        Debug.Log(StartPlayer.name + "Start");
        //StartPlayer.GetComponentInChildren<SpriteRenderer>().enabled = true;
        state = STATE.RUN;
        
        
    }

    public void MovePlayerIfCollide(Vector3 direction, GameObject player)
    {
        player.transform.Translate(direction*3.0f* Time.deltaTime);
    }
    /**********************************/
    public int getCurrentID()
    {
        return currentId;
    }

    void gameRunning()
    {
        if (p1Turn && scriptP1.turnEnded())
        {
            changeTurn();
        }
        if (!p1Turn && scriptP2.turnEnded())
        {
            
            changeTurn();
        }

    }

    //ADD
    public bool isMouseOnPlayer(int ID)
    {
        RaycastHit hit;
        bool value = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<Player>())
            {
                if (hit.collider.GetComponent<Player>().ID == ID)
                {
                    value = true;
                }

            }
        }

        return value;
    }
    //END ADD

    public bool isMouseOnCurrentPlayer(int ID, out RaycastHit hit)
    {
        bool value = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && ID == currentId)
        {
            if (hit.collider.GetComponent<Player>())
            {
                if (hit.collider.GetComponent<Player>().ID == currentId)
                {
                    value = true;
                }

            }
        }

        return value;
    }

    public GameObject getPlayer(int ID)
    {
        if (ID == 1)
        {
            return Player1;
        }
        else
        {
            return Player2;
        }
    }

    public void changeTurn()
    {
        //Debug.Log("Tour du joueur 1 : " + p1Turn);
        p1Turn = !p1Turn;
		if(p1Turn)
		{
			currentId = 1;
		}else{
			currentId = 2;
		}
        scriptP1.setTurn(p1Turn);
        scriptP2.setTurn(!p1Turn);
    }

    public GameObject getOtherPlayer(int ID)
    {
        if (ID == 1)
        {
            return Player2;
        }
        else
        {
            return Player1;
        }
    }

    void playerStartPosition()
    {
        // Les joueurs choissisent leurs positions
        if (Input.GetMouseButtonDown(0) && !launch)
        {
            //Debug.Log("TOUCH");
            Vector3 pos = Input.mousePosition;    //screenPos to worldPos of mousePos
			pos.z = -Camera.main.transform.position.z;                 //Pos of player in 2D
			Vector3 playerPosInWorld = Camera.main.ScreenToWorldPoint(pos);

            //check vertical
            if (playerPosInWorld.y > 3.9f)
            {
                playerPosInWorld.y = 3.9f;
            }
            else if (playerPosInWorld.y < -3.9f)
            {
                playerPosInWorld.y = -3.9f;
            }

            if ((Input.mousePosition.x > (float)Screen.width / 2))
            {
                //check horizontal
                if (playerPosInWorld.x > 6.9f)
                {
                    playerPosInWorld.x = 6.9f;
                }
                else if (playerPosInWorld.x < 1.1f)
                {
                    playerPosInWorld.x = 1.1f;
                }

                /************************/
                if (Player1 == null)
                {
                    Player1 = (GameObject)Instantiate(PlayerPrefab, playerPosInWorld, new Quaternion());
                    Player1.name = "Player1";
                    scriptP1 = Player1.GetComponent<Player>();
                    scriptP1.setID(1);
                    HUDManager.Instance.setPlayer(1, Player1);
                    HUDManager.Instance.setLifeUI(1, scriptP1.getLife());
                }
                else
                {
                    Player1.transform.position = playerPosInWorld;
                }
            }
            else
            {
                
                //check horizontal
                if (playerPosInWorld.x < -6.9f)
                {
                    playerPosInWorld.x = -6.9f;
                }
                else if (playerPosInWorld.x > -1.1f)
                {
                    playerPosInWorld.x = -1.1f;
                }
                /****************/

                if (Player2 == null)
                {
                    Player2 = (GameObject)Instantiate(PlayerPrefab, playerPosInWorld, new Quaternion());
                    Player2.name = "Player2";
                    scriptP2 = Player2.GetComponent<Player>();
                    scriptP2.setID(2);
                    HUDManager.Instance.setPlayer(2, Player2);
                    HUDManager.Instance.setLifeUI(2, scriptP2.getLife());
                }
                else
                {

                    Player2.transform.position = playerPosInWorld;
                }
            }
        }
        
        
    }
}
