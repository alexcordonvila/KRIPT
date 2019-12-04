using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Vector2 startPosition;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
      new Vector2(startPosition.x + (Mathf.Sin(Time.time * speed) * 0.6f), transform.position.y);
    }
}
