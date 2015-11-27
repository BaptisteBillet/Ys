using UnityEngine;
using System.Collections;

public class lineDraw : MonoBehaviour {
    bool aim;
    LineRenderer lineAim;
    LineRenderer lineAimRebond;
	// Use this for initialization
	void Start () {
        aim = false;
        lineAim = transform.gameObject.GetComponent<LineRenderer>();
        lineAim.enabled = aim;
	}
	
	// Update is called once per frame
	void Update () {
        if (aim)
        {
            drawAim();
        }
	}


    public void changeIsAim(bool value)
    {
        aim = value;
        //lineAim.enabled = value;
    }
    void drawAim()
    {
        Vector3 mousePosInWorld = Input.mousePosition;
        
        mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosInWorld);
        mousePosInWorld.z = 0;
        RaycastHit hit;

        Vector3 direction = transform.parent.position - mousePosInWorld;
        lineAim.SetPosition(0, this.gameObject.transform.position);
        int layerWall = 1<<11;
        if (Physics.Raycast(transform.parent.position, direction, out hit, Mathf.Infinity, layerWall))
        {
            //print(mousePosInWorld);
            lineAim.SetPosition(1, hit.point);
            Vector3 incomingVec = hit.point - this.transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
            if (Physics.Raycast(hit.point, reflectVec, out hit, Mathf.Infinity, layerWall))
            {
                lineAim.SetPosition(2, hit.point);
            }

        }
        lineAim.enabled = true;
        lineAim.SetColors(Color.red, Color.green);
    }
}
