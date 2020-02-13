using UnityEngine;

public class Player : MonoBehaviour
{
				public Rigidbody2D playerBody;
				private Vector2 velocity;
				private GameController gameController;

				public bool CanBeControlled { get; private set; } = true;

				private void MoveToStartPosition()
				{
								this.transform.position = gameController.PlayerStartPosition;
				}

				public void TryKill(bool freeze = true)
				{
								Kill(freeze);
				}

				private void Kill(bool freeze)
				{
								gameController.GameOver = true;
								if(freeze)
												Freeze();
								Time.timeScale = 0;
								Shatter();
				}

				private void Shatter()
				{
								print("TODO: Shatter");
				}

				public void Respawn()
				{
								MoveToStartPosition();
								velocity = Vector2.zero;
								Unfreeze();
								Time.timeScale = 1;
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

				private void OnTriggerEnter2D(Collider2D collider)
				{
								if (collider.CompareTag("End")) {
												gameController.GameWon = true;
								} else if (collider.CompareTag("Dark")) {
												gameController.EnableDarkMode();
								}
				}

				private void Start()
				{
								gameController = FindObjectOfType<GameController>();

								MoveToStartPosition();
				}

}
