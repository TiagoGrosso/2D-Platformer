using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
				public Data data;

				private int curTarget;
				private Rigidbody2D body2D;

				private void Start()
				{
								curTarget = 0;
								body2D = gameObject.GetComponent<Rigidbody2D>();
				}

				private void Update()
				{
								if(OnTarget())
												ChangeTarget();
				}

				private void FixedUpdate()
				{
								MoveTowardsTarget();
				}

				private void MoveTowardsTarget()
				{
								body2D.MovePosition(Vector2.MoveTowards(body2D.position, data.targets[curTarget], data.speed * Time.fixedDeltaTime));
				}

				private void ChangeTarget()
				{
								curTarget = (curTarget + 1 >= data.targets.Length) ? 0 : (curTarget + 1);
				}

				private bool OnTarget()
				{
								return (Vector2.Distance(transform.position, data.targets[curTarget]) < 0.05f);
				}

				[Serializable]
				public struct Data {
								public Vector2[] targets;
								public float speed;
				}
}
