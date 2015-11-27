using UnityEngine;
using System.Collections;

public class CheckZone : MonoBehaviour {

    RaycastHit hit;
    public TypeZone.TerrainType currentType;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.GetComponent<Player>().ID == GameManagerScript.instance.currentId)
        {
            int layer = 1<<13;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up,Mathf.Infinity,layer);
            if (hit.collider != null)
            {
                currentType = hit.collider.GetComponent<TypeZone>().terrainType;
                transform.GetComponent<Player>().currentTypeZone= currentType;

                transform.GetComponent<Player>().currentTerrain = hit.collider.gameObject;
            }
        }
	}
}
