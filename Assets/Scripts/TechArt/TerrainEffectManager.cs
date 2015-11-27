using UnityEngine;
using System.Collections;

[RequireComponent(typeof (TypeZone))]
public class TerrainEffectManager : MonoBehaviour {

    //private TypeZone.TerrainType currentType;
    [SerializeField]
    private GameObject currentEffect;


    // Use this for initializations
    void Start()
    {
        /*
        currentType = this.GetComponent<TypeZone>().terrainType;

        switch (currentType)
        {
            case TypeZone.TerrainType.FOREST:
                currentEffect = this.GetComponentInParent<TerrainEffectHolder>().forestTerrainEffect;
                break;
            case TypeZone.TerrainType.MOUNTAIN:
                currentEffect = this.GetComponentInParent<TerrainEffectHolder>().mountainTerrainEffect;
                break;

            case TypeZone.TerrainType.BUMPER:
                currentEffect = this.GetComponentInParent<TerrainEffectHolder>().bumperTerrainEffect;
                break;
            case TypeZone.TerrainType.PLAIN:
                currentEffect = this.GetComponentInParent<TerrainEffectHolder>().plainTerrainEffect;
                break;

        }
        */
    }
	

    public void ActivateEffect()
    {
        if(currentEffect != null)
        {
            currentEffect.SetActive(true);

        }
        else
        {
            Debug.Log("No Terrain Effect Attached");
        }
    }


    public void KillEffect()
    {

        Debug.Log("quitting");

        if (currentEffect != null)
        {
            currentEffect.SetActive(false);
        }
        else
        {
            Debug.Log("No Terrain Effect Attached");
        }
    }
}
