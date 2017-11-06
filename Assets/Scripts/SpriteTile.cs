using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTile : MonoBehaviour
{
    private TileNode nearestNode;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!Input.GetMouseButton(0))
        {
            transform.localPosition = nearestNode.TilePosition;
        }
        else
        {
            nearestNode = TileManager.instance.GetNearestNode(transform.position);
        }
	}
}
