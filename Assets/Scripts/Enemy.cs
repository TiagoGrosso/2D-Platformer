using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Data data;

    public LayerMask groundLayers;

    private Rigidbody2D body;
    private Collider2D enemyCollider;
    private int direction = 0;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();

        direction = Probability.RandomInCollection(new int[] { -1, 1 });
    }

    private void Update()
    {
        if (HoleInGround())
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        direction = -direction;
    }

    //Does not work on tilted ground. Should take into account rotation to support that
    private bool HoleInGround()
    {
        Vector2 rayCenter = transform.position + (enemyCollider.bounds.extents.x * direction * Vector3.right);
        
        return !Physics2D.Raycast(rayCenter, Vector2.down, enemyCollider.bounds.extents.y + 0.05f, groundLayers);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall")) {
            ChangeDirection();
        }
    }

    private void FixedUpdate()
    {
        body.MovePosition(transform.position + (Vector3.right * data.speed * Time.fixedDeltaTime * direction));
    }

    [Serializable]
    public struct Data {
        public float speed;
    }
}
