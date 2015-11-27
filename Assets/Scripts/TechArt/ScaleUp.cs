using UnityEngine;
using System.Collections;

public class ScaleUp : MonoBehaviour {

    [SerializeField]
    private float m_ScaleSpeed = 2f;

    [SerializeField]
    private float m_ScaleDestination = 2f;

	
	// Update is called once per frame
	void Update () {

        Vector3 scale = new Vector3(m_ScaleDestination, m_ScaleDestination, 0);
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, scale, Time.deltaTime * m_ScaleSpeed);

        if (this.transform.localScale.x >= (scale.x-0.4f))
        {
            Destroy(this.gameObject);
        }

    }

}
