using UnityEngine;
using System.Collections;

[RequireComponent(typeof (TypeZone))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class TerrainEffectManager : MonoBehaviour {

    
    //private TypeZone.TerrainType currentType;
    [SerializeField]
    private GameObject currentEffect;

    private SpriteRenderer ZoneSprite;

    [SerializeField]
    private Color BaseColor = new Color(0.5f, 0.5f, 0.5f);

    [SerializeField]
    private Color HighlightColor = new Color(1f, 1f, 1f);

    [SerializeField]
    private float m_ColorChangeSpeed = 2f;

    private Animator TerrainAnimator;


    // Use this for initializations
    void Start()
    {
        ZoneSprite = this.GetComponent<SpriteRenderer>();
        TerrainAnimator = this.GetComponent<Animator>();

        ZoneSprite.color = BaseColor;
    }
	

    public void ActivateEffect()
    {
        StartCoroutine("LerpEffects");

        ZoneSprite.sortingOrder = 1;

        TerrainAnimator.SetTrigger("ScaleUpnDown");


        if (currentEffect != null)
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

        ZoneSprite.color = BaseColor;

        ZoneSprite.sortingOrder = 0;


        //StartCoroutine()
        if (currentEffect != null)
        {
            currentEffect.SetActive(false);
        }
        else
        {
            Debug.Log("No Terrain Effect Attached");
        }
    }
    
    IEnumerator LerpEffects()
    {
        while (ZoneSprite.color != HighlightColor)
        {
            ZoneSprite.color = new Color(ZoneSprite.color.r + 0.1f, ZoneSprite.color.g + 0.1f, ZoneSprite.color.b + 0.1f);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
