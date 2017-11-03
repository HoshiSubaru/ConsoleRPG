using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
	}
	


    public void Move(string[] args)
    {
        Vector3 direction = transform.position;
        float value = float.Parse(args[2]);

        switch(args[1])
        {
            case "up":
                direction.y += value;
                break;

            case "down":
                direction.y -= value;
                break;
            case "left":
                direction.x -= value;
                break;
            case "right":
                direction.x += value;
                break;
        }

        StopAllCoroutines();
        StartCoroutine(MoveCoroutine(direction));
    }

    private IEnumerator MoveCoroutine(Vector3 position)
    {
        while(transform.position != position)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * 10);
            yield return new WaitForFixedUpdate();
        }
    }
}
