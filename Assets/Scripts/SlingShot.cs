using UnityEngine;
using System.Collections;


public class SlingShot : MonoBehaviour {

    Vector3 mouseUpPos;
    Vector3 mouseDownPos;
    

    //public lineDraw scriptAim;

    float dist;
    public float distanceMax = 6;
    //public float puissanceMax = 30;
    public bool shoot = false;
	public bool playing = false;
	public bool action = false;
    bool isSlingShotting = false;
    bool powerUp = false;
    public float force = 50;
    public GameObject cancelImage;
	LineRenderer lineRenderer;
    RaycastHit hit;
    Player player;
	public int myID;
    bool isOnPlayer = false;

    bool isCircleAlreadyAppear=false;

    /*******UI*******/

	// Use this for initialization
	void Start () {
        distanceMax = 6;
        force = 50;
        shoot = false;
        playing = false;
        action = false;
        isSlingShotting = false;
		lineRenderer = transform.parent.gameObject.GetComponent<LineRenderer>();
		lineRenderer.enabled = false;
        player = transform.parent.gameObject.GetComponent<Player>();
        cancelImage.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (GameManagerScript.instance.isMouseOnCurrentPlayer(myID, out hit))
        {
            isOnPlayer = true;
        }
        else
        {
            isOnPlayer = false;
        }

        transform.position = transform.parent.position;

        if (powerUp && Input.GetMouseButtonDown(0))
        {
            powerUp = false;
            Debug.Log("POWER USED");
            var direction = mouseDownPos - mouseUpPos;
            if (myID == 1)
            {
                Debug.Log("P1 BOOST");
                transform.parent.GetComponent<Rigidbody>().velocity *= 2;
            }
            else
            {
                Debug.Log("P2 STOP");
                transform.parent.GetComponent<Rigidbody>().velocity /= 2;
                StartCoroutine(powerStop());
            }
        }

	}

    IEnumerator powerStop()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("P2 STOPED !");
        transform.parent.GetComponent<Rigidbody>().velocity *=0;
    }

    void FixedUpdate()
    {
        if (myID == GameManagerScript.instance.currentId && Input.GetMouseButton(0) && shoot && !transform.parent.GetComponent<Player>().isMaybeDoubleTapping && isSlingShotting && !isOnPlayer) // si le joueur est en train de tirer
        {
            //scriptAim.changeIsAim(true);
            // on scale la jauge de puissance en fonction de la distance
            ScaleJauge();
        }
        else
        {
            cancelImage.SetActive(false);
            //scriptAim.changeIsAim(false);
        }
    }
    void ScaleJauge()
    {
        lineRenderer.enabled = true;
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosInWorld.z = 0;

        dist = Vector3.Distance(new Vector3(transform.parent.position.x, transform.parent.position.y, 0.0f), new Vector3(mousePosInWorld.x, mousePosInWorld.y, 0.0f));
        lineRenderer.SetPosition(0, new Vector3(transform.parent.position.x, transform.parent.position.y, 0.0f));
        Vector3 direction = mousePosInWorld - transform.position;
        direction.Normalize();

        direction = Vector3.ClampMagnitude(direction, 1.0f);
        direction.z = 0;
        if (dist > distanceMax)
        {
            dist = distanceMax;
        }

        if (dist > 3)
        {
            cancelImage.SetActive(false);
            lineRenderer.SetColors(Color.yellow, Color.yellow);
        }
        else
        {
            cancelImage.SetActive(true);
            lineRenderer.SetColors(Color.red, Color.red);
        }
        lineRenderer.SetPosition(1, transform.position + (direction * dist));
        

        
    }

    void OnMouseDown()
    {
        if(isCircleAlreadyAppear==false)
        {
            isCircleAlreadyAppear = true;
            GameManagerScript.instance.Player1.GetComponent<Player>().m_CercleAnimator.SetTrigger("Close");
            GameManagerScript.instance.Player1.GetComponent<Player>().m_LifeAnimator.SetTrigger("Close");

            GameManagerScript.instance.Player2.GetComponent<Player>().m_CercleAnimator.SetTrigger("Close");
            GameManagerScript.instance.Player2.GetComponent<Player>().m_LifeAnimator.SetTrigger("Close");
        }



        StartCoroutine(CountDown(0.5f));
        if (!action && myID == GameManagerScript.instance.currentId && !transform.parent.GetComponent<Player>().doubleTap && !isOnPlayer && !transform.parent.GetComponent<Player>().isMovingBump && !transform.parent.GetComponentInChildren<ejection>().ejectionAction)
		{
			shoot = true;
			playing = true;
            transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            mouseDownPos = transform.parent.position;
            
		}
        
        
    }

    IEnumerator CountDown(float time)
    {
        float currentTime = 0;
        while(currentTime < time)
        {
            currentTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        isSlingShotting = true;

    }

    void OnMouseUp()
    {
        isSlingShotting = false;
        if (!action && playing && myID == GameManagerScript.instance.currentId && !transform.parent.GetComponent<Player>().doubleTap)
		{
            mouseUpPos = Input.mousePosition; //on stock la position de d'arrivée
            mouseUpPos = Camera.main.ScreenToWorldPoint(mouseUpPos);
            mouseUpPos.z = 0;
            //mouseUpPos.y = 0;
            dist = Vector3.Distance(mouseDownPos, mouseUpPos);
            if (dist > distanceMax)
            {
                dist = distanceMax;
            }
            if (dist > 3)
            {
                shoot = false;
                action = true;
                powerUp = true;
                var direction = mouseDownPos - mouseUpPos;
                transform.parent.GetComponent<Rigidbody>().AddForce(direction * dist * force);

                transform.parent.GetComponent<Player>().startTypeZone = transform.parent.GetComponent<Player>().currentTypeZone;
                StartCoroutine(Wait(0.1f));

            }
            else
            {
                shoot = false;
                playing = false;
            }
            lineRenderer.enabled = false;
		}
    }

	IEnumerator Wait(float time)
	{
		yield return new WaitForSeconds(time);
		playing = false;
	}
}
