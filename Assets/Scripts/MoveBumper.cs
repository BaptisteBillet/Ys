using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveBumper : MonoBehaviour {
    Ray ray;
    RaycastHit hit;
    bool onBumper = false;
    public int collisionCount;
    bool buttonIsOpened = false;
    Vector3 collisionPosition;

    public GameObject Canvas;
    public Animator m_Animator;
    public GameObject validateButton;

    void Awake()
    {
        validateButton.SetActive(false);
    }
	// Use this for initialization
	void Start () {
        onBumper = false;
        m_Animator.SetTrigger("Close");
        //temp
        validateButton.SetActive(false);
        buttonIsOpened = false;
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

        
        //Canvas.transform.LookAt(Vector3.zero, Canvas.transform.forward);
       // Debug.Log(Canvas.transform.name);
       // Debug.Log(Canvas.transform.up);
        //Debug.DrawRay(Vector3.zero, Canvas.transform.up);
	    if(Input.GetMouseButton(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<MoveBumper>())
                {
                    MoveValidateButton();
                    

                    if (buttonIsOpened)
                    {

						SoundManagerEvent.emit(SoundManagerType.TakeMush);
                        //m_Animator.SetTrigger("Close");
                        buttonIsOpened = false;
                        //temp
                        validateButton.SetActive(false);
                    }
                    
                    
                    onBumper = true;
                    Vector3 bumperPos = Input.mousePosition;
                    bumperPos = Camera.main.ScreenToWorldPoint(bumperPos);
                    bumperPos.z = 0.0f;

                    this.transform.position = bumperPos;
                    
                    
                }
            }
            
        }
        else if (Input.GetMouseButtonUp(0) && onBumper && collisionCount ==0)
        {

            if (!buttonIsOpened)
            {
				SoundManagerEvent.emit(SoundManagerType.ReleaseMush);
                //m_Animator.SetTrigger("Open");
                buttonIsOpened = true;
                //temp
                validateButton.SetActive(true);
            }
            
            
            onBumper = false;
        }
        
        
	}

    public void validMove()
    {
        GameObject player = GameManagerScript.instance.getPlayer(GameManagerScript.instance.getCurrentID());


        if (buttonIsOpened)
        {
            //m_Animator.SetTrigger("Close");
            buttonIsOpened = false;
            //temp
            validateButton.SetActive(false);
        }
        player.GetComponent<Player>().actionReady = false;
        transform.GetComponent<MoveBumper>().enabled = false;
    }

    void MoveValidateButton()
    {
        Vector3 direction = Vector3.zero - transform.position;
        direction.Normalize();
        Debug.DrawRay(transform.position, direction * 2.0f, Color.red);
        validateButton.transform.position = transform.position + direction*2.0f;
    }
}
