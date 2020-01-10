using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer, deadLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public bool onCeiling;
    public bool onDead;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, bottomOffset2, bottomOffset3, rightOffset, leftOffset,headOffset;
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset2, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset3, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onDead = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, deadLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset2, collisionRadius, deadLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset3, collisionRadius, deadLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onCeiling = Physics2D.OverlapCircle((Vector2)transform.position + headOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, bottomOffset2, bottomOffset3, rightOffset, leftOffset, headOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset2, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset3, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + headOffset, collisionRadius);
    }
}
