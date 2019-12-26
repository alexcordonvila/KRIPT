using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector2 startPosition;
    public float speed;
    public bool direction;

    private int dir;
    // Start is called before the first frame update
    void Start()
    {
        
        startPosition = transform.position;
        
        int random = Random.Range(0, 10);
        if (random < 2 || random >8)
        {
            dir = 1;
        }else
        {
            dir = 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (direction) { 
        transform.position =
      new Vector2(startPosition.x + (Mathf.Sin(Time.time * speed * dir) * 0.6f), transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startPosition.x , transform.position.y + (Mathf.Sin(Time.time * speed * dir) * 0.6f));

        }
    }
}
