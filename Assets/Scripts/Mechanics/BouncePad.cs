using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.Experimental.XR;

[JsonObject(MemberSerialization.OptIn)]
public class BouncePad : MonoBehaviour
{
				public Data data;

				private float resetTimeCount = 0;
				private bool canBounce = true;
				private bool bounce = false;
				private Vector2 startPosition;
				private Vector2 targetPosition;

				private void Bounce(Rigidbody2D body)
				{
								body.AddForce(transform.up * data.bounceForce, ForceMode2D.Impulse);
								bounce = true;
				}

				private void OnCollisionEnter2D(Collision2D collision)
				{
								if(collision.collider.CompareTag("Player") || collision.collider.CompareTag("Enemy")) {
												Bounce(collision.collider.GetComponent<Rigidbody2D>());
								}
				}

				private void Start()
				{
								startPosition = transform.position;
								targetPosition = startPosition + (Vector2) transform.up * data.bouncePadHeight;
				}

				private void Update()
				{
								if (!canBounce) {
												resetTimeCount += Time.deltaTime;
												transform.position = Vector2.Lerp(targetPosition, startPosition, resetTimeCount / data.resetTime);

												if (resetTimeCount >= data.resetTime) {
																canBounce = true;
																resetTimeCount = 0;
												}
								} else if (bounce) {
												transform.position = Vector2.MoveTowards(transform.position, targetPosition, data.bouncePadSpeed * Time.deltaTime);
												
												if(Vector2.Distance(transform.position, targetPosition) < 0.1f) {
																bounce = false;
																canBounce = false;
												}
								}
				}

				[Serializable]
				public struct Data {
								public float bounceForce;
								public float bouncePadHeight;
								public float bouncePadSpeed;
								public float resetTime;
				}
}
