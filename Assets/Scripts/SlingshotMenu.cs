using UnityEngine;
using System.Collections;

public class SlingshotMenu : MonoBehaviour {

    Vector3 mouseDownPos, mouseUpPos;
    public float dist = 0;
    public bool shoot = false;
    public bool action = false;
    public float force = 10;
    public float distanceMax = 4;
    bool isSlingShotting = false;
    LineRenderer lineRenderer;
    Vector3 velocity;
    float playerSpeed;
    
	// Use this for initialization
	void Start () {
        force = 50;
        action = false;
        lineRenderer = transform.parent.gameObject.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        velocity = transform.GetComponentInParent<Rigidbody>().velocity;
        playerSpeed = velocity.magnitude;
        if (playerSpeed <= 0.5f && !shoot)
        {
            action = false;
            transform.GetComponentInParent<Rigidbody>().velocity *=0;
        }
	}

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && shoot && isSlingShotting) // si le joueur est en train de tirer
        {
            Debug.Log("DRAW");
            // on scale la jauge de puissance en fonction de la distance
            ScaleJauge();
        }
    }

    void ScaleJauge()
    {
        lineRenderer.enabled = true;
        Vector3 mousePosInWorld;// = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cursorPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePosInWorld = Input.mousePosition;
        mousePosInWorld.z = 0;

        dist = Vector3.Distance(new Vector3(cursorPos.x, cursorPos.y, 0.0f), new Vector3(mousePosInWorld.x, mousePosInWorld.y, 0.0f));
        lineRenderer.SetPosition(0, new Vector3(cursorPos.x, cursorPos.y, 0.0f));
        Vector3 direction = mousePosInWorld - cursorPos;
        direction.Normalize();

        direction = Vector3.ClampMagnitude(direction, 1.0f);
        direction.z = 0;
        if (dist > distanceMax)
        {
            dist = distanceMax;
        }
        lineRenderer.SetPosition(1, cursorPos + (direction * dist));


        lineRenderer.SetColors(Color.yellow, new Color(255.0f, 255.0f - (100 + dist), 0.0f));
    }

    void OnMouseDown()
    {
        /*StartCoroutine(CountDown(0.5f));
        if (!action && myID == GameManagerScript.instance.currentId && !transform.parent.GetComponent<Player>().doubleTap && !isOnPlayer && !transform.parent.GetComponent<Player>().isMovingBump && !transform.parent.GetComponentInChildren<ejection>().ejectionAction)
        {
            shoot = true;
            playing = true;
            transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            mouseDownPos = transform.parent.position;

        }*/
        if (!action)
        {
            Debug.Log("mouseDown");
            shoot = true;
            isSlingShotting = true;
            transform.parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            mouseDownPos = transform.parent.position;
            Debug.Log("mouseDown:"+mouseDownPos);
        }

    }

    void OnMouseUp()
    {
        isSlingShotting = false;
        if (!action)
        {
            mouseDownPos.z = 0;
            mouseUpPos = Input.mousePosition; //on stock la position de d'arrivée
            Debug.Log("mouseUp:"+mouseUpPos);
            //mouseUpPos = Camera.main.ScreenToWorldPoint(mouseUpPos);
            mouseUpPos.z = 0;
            mouseDownPos = Camera.main.WorldToScreenPoint(mouseDownPos);
            //mouseUpPos.y = 0;
            shoot = false;
            dist = Vector3.Distance(mouseDownPos, mouseUpPos);
            Debug.Log("mDownPos:"+mouseDownPos+" mUpPos"+mouseUpPos);
            if (dist > distanceMax)
            {
                dist = distanceMax;
            }
            if (dist > 3)
            {
                action = true;
                var direction = mouseDownPos - mouseUpPos;
                Debug.Log("distance:" + dist + " direction:" + direction);
                transform.parent.GetComponentInParent<Rigidbody>().AddForce(direction * dist * force*Time.deltaTime);

                //transform.parent.GetComponent<Player>().startTypeZone = transform.parent.GetComponent<Player>().currentTypeZone;
                //StartCoroutine(Wait(0.1f));

            }
            lineRenderer.enabled = false;
        }
    }

    /*IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        playing = false;
    }*/
}
