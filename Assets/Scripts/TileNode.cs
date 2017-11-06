using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileNode : MonoBehaviour
{
    public List<Sprite> allSprites;
    public Sprite defaultSprite;

    [SerializeField] private Vector3 tilePosition;
    private SpriteRenderer spriteRenderer;

    public Vector3 TilePosition
    {
        get { return tilePosition; }
        set { tilePosition = value; }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //transform.localPosition = tilePosition;

        //if (allSprites.Count == 0)
        //{
        //    spriteRenderer.sprite = defaultSprite;
        //}
    }

    private void OnDrawGizmos()
    {
        if (allSprites.Count == 0)
        {
            Gizmos.DrawCube(transform.position, new Vector3(0.95f, 0.95f));
        }
    }
}
