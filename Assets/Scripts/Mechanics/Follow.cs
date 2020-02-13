using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
				public Transform target;
				public float acceleration;
				public LayerMask obsctacleLayer;

				public Rigidbody2D body;

				private void FixedUpdate()
				{
								Vector2 direction = (target.position - transform.position).normalized;
								body.AddForce(direction * acceleration);
								transform.up = body.velocity;
				}
}
