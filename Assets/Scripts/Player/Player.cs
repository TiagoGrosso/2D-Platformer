using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
				public Rigidbody2D playerBody;
				private Vector2 velocity;
				private TimeGameController gameController;
				public bool CanBeControlled { get; private set; }

				private void MoveToStartPosition()
				{
								this.transform.position = gameController.PlayerStartPosition;
				}

				public void Kill()
				{
								Freeze();
								Shatter();
				}

				private void Shatter()
				{
								print("TODO: Shatter");
				}

				public void Respawn()
				{
								MoveToStartPosition();
								playerBody.bodyType = RigidbodyType2D.Dynamic;
								CanBeControlled = true;
				}

				public void Won()
				{
								playerBody.bodyType = RigidbodyType2D.Static;
								CanBeControlled = false;
				}

				public void Freeze(bool conserveVelocity = false)
				{
								if (conserveVelocity)
												velocity = playerBody.velocity;
								else
												velocity = Vector2.zero;

								playerBody.bodyType = RigidbodyType2D.Static;
								CanBeControlled = false;
				}

				public void Unfreeze()
				{
								playerBody.bodyType = RigidbodyType2D.Dynamic;
								CanBeControlled = true;
								playerBody.velocity = velocity;
				}
				private void OnCollisionEnter2D(Collision2D collision)
				{
								if (collision.collider.CompareTag("Hazard") || collision.collider.CompareTag("Enemy")) {
												gameController.GameOver = true;
								}
				}

				private void OnTriggerEnter2D(Collider2D collider)
				{
								if (collider.CompareTag("End")) {
												gameController.GameWon = true;
								}
				}

				private void Start()
				{
								gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeGameController>();

								MoveToStartPosition();
				}
}
