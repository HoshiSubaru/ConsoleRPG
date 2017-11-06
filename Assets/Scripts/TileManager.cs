using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileManager : MonoBehaviour
{
    public static TileManager instance;

    public TileNode[,] allTiles;
    public Vector2 noOfTiles;
    public float tileSize;
    public GameObject defaultTile;

    private void Awake()
    {
        instance = this;
    }

    public TileNode GetNearestNode(Vector3 pos)
    {
        return null;
    }

    public void GenerateGrid()
    {
        TileNode[] allChild = GetComponentsInChildren<TileNode>();
        foreach(TileNode tile in allChild)
        {
            DestroyImmediate(tile.gameObject);
        }

        allTiles = new TileNode[(int)noOfTiles.x, (int)noOfTiles.y];

        for (int r = 0; r < noOfTiles.x; r++)
        {
            for (int c = 0; c < noOfTiles.x; c++)
            {
                if(allTiles[r, c] != null)
                {
                    allTiles[r, c].TilePosition = transform.right * (c * tileSize)
                    - transform.up * (r * tileSize);
                    break;
                }

                allTiles[r, c] = Instantiate(defaultTile, transform).GetComponent<TileNode>();

                allTiles[r, c].TilePosition = transform.right * (c * tileSize)
                    - transform.up * (r * tileSize);
            }
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
