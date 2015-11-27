using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class HUDManager : Singleton<HUDManager> {
    protected HUDManager() { }
    GameObject player1GO=null, player2GO=null;
    List<GameObject> p1LifePoints = new List<GameObject>();
    List<GameObject> p2LifePoints = new List<GameObject>();
    GameObject p1Life, p2Life;
    public GameObject LifePoint;
    // Use this for initialization
	void Start () {
        p1Life = new GameObject();
        p1Life.name = "LifeBar1";
        p2Life = new GameObject();
        p2Life.name = "LifeBar2";
	}
	
	// Update is called once per frame
	void Update () {
        if(player1GO != null && player2GO !=null)
        {
            p1Life.transform.position = new Vector3(player1GO.transform.position.x,player1GO.transform.position.y - transform.localScale.y, 0.0f);
            p2Life.transform.position = new Vector3(player2GO.transform.position.x, player2GO.transform.position.y - transform.localScale.y, 0.0f);
        }
	}

    public void setPlayer(int ID, GameObject player)
    {
        if(ID == 1)
        {
            player1GO = player;
            //p1Life.transform.parent = player1GO.transform;
        }
        else
        {
            player2GO = player;
            //p2Life.transform.parent = player2GO.transform;
        }
    }

    public void updateLife(int ID, int Life)
    {
        int diffPoints;
        showLifeBar(ID, true);
        GameObject cache;
        if(ID == 1)
        {
            
            if(Life < p1LifePoints.Count)
            {
                // lost life
                diffPoints = p1LifePoints.Count - Life;
                for (int i = 0; i < diffPoints; i++)
                {
                    Destroy(p1LifePoints.Last());
                    p1LifePoints.RemoveAt(p1LifePoints.Count-1);
                }
            }
            else
            {
                // add life
                diffPoints = Life - p1LifePoints.Count;
                for (int i = 0; i < diffPoints; i++)
                {
                    cache = (GameObject)Instantiate(LifePoint, new Vector3(p1LifePoints.Last().transform.position.x+0.4f,0,0), Quaternion.identity);
                    cache.transform.parent = p1Life.transform;
                    p1LifePoints.Add(cache);
                }
            
            }
        }
        else
        {
            if (Life < p2LifePoints.Count)
            {
                //Debug.Log("lost life2 " + p2LifePoints.Count+ " - "+ Life);
                diffPoints = p2LifePoints.Count - Life;
                for (int i = 0; i < diffPoints; i++)
                {
                    Destroy(p2LifePoints.Last());
                    p2LifePoints.RemoveAt(p2LifePoints.Count-1);
                }
            }
            else
            {
                diffPoints = Life - p2LifePoints.Count;
                for (int i = 0; i < diffPoints; i++)
                {
                    cache = (GameObject)Instantiate(LifePoint, new Vector3(p2LifePoints.Last().transform.position.x + 0.4f, 0, 0), Quaternion.identity);
                    cache.transform.parent = p2Life.transform;
                    p1LifePoints.Add(cache);
                }
            }
        }
    }

    public void showLifeBar(int ID, bool show)
    {
        if(ID ==1)
        {
            p1Life.SetActive(show);
        }
        else
        {
            p2Life.SetActive(show);
        }
    }

    public void setLifeUI(int ID, int Life)
    {
        GameObject cache;
        if(ID == 1)
        {
            // vide la liste 
            foreach(GameObject go in p1LifePoints)
            {
                Destroy(go);
            }
            p1LifePoints.Clear();
            // remplis la liste de point de vie
            for (int i = 0; i < Life;i++ )
            {
                cache = (GameObject)Instantiate(LifePoint, new Vector3((i * 0.4f), 0, 0), Quaternion.identity);
                //cache.transform.parent = p1Life.transform;
                //p1LifePoints.Add(cache);
            }
        }
        else
        {
            foreach (GameObject go in p2LifePoints)
            {
                Destroy(go);
            }
            p2LifePoints.Clear();
            for (int i = 0; i < Life; i++)
            {
                cache = (GameObject)Instantiate(LifePoint, new Vector3((i * 0.4f), 0, 0), Quaternion.identity);
                //cache.transform.parent = p2Life.transform;
                //p2LifePoints.Add(cache);
                
            }
        }
        
    }
}
