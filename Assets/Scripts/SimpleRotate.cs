using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {

    public float m_Speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(transform.forward, Time.deltaTime * m_Speed);
	}
}
