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
        speed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
      new Vector2(startPosition.x , transform.position.y + (Mathf.Sin(Time.time * speed) * 0.01f));
    }
}
