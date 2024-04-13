using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Unlock")]
    [SerializeField] bool isUnlocked;
    [SerializeField] ResourceChunk unlockCost;

    [Header("Box Size")]
    readonly Vector2 areaSize = new Vector2(16f, 9f);
    readonly Vector2 areaStart = new Vector2(-7.5f, -4f);
    [SerializeField] int sizeX;
    [SerializeField] int sizeY;

    [Header("Graphics")]
    [SerializeField] Sprite floorTile;
    [SerializeField] Sprite wallTile;

    GameObject[,] machines;


    private void Start()
    {
        machines = new GameObject[sizeY, sizeX];

        for (int x = 0; x < (int) areaSize.x; x++)
        {
            for (int y = 0; y < (int) areaSize.y; y++)
            {
                GameObject temp = new GameObject("Tile");
                temp.transform.position = new Vector3(x + areaStart.x, y + areaStart.y, 0f);
                temp.transform.parent = transform;

                temp.AddComponent<SpriteRenderer>();
                SpriteRenderer sr = temp.GetComponent<SpriteRenderer>();


                if (x >= (int) areaSize.x - 1 - sizeX && x < (int)areaSize.x - 1 && y >= (int)areaSize.y - 1 - sizeY && y < (int)areaSize.y - 1)
                {
                    sr.sprite = floorTile;
                }
                else
                {
                    sr.sprite = wallTile;
                }
            }
        }
    }
}
