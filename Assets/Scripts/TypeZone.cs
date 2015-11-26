using UnityEngine;
using System.Collections;

public class TypeZone : MonoBehaviour {

    public enum TerrainType
    {
        FOREST,
        PLAIN,
        MOUNTAIN,
        BUMPER
    };

    public TerrainType terrainType;
}
