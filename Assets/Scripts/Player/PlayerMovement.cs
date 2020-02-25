using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
				private PlayerControls playerControls;
				private Rigidbody2D playerBody;
				private Collider2D playerCollider;

				//Moving
				private bool canMoveInAir = false;
				private Coroutine waitBeforeMovingInAir;
				public float waitTimeBeforeMovingInAir;
				public float Movement {
								get; private set;
				}
				public float moveForce;
				public float torque;
				public float maxAngularVelocity;


				//JumpingMovement
				private bool isGrounded = false;
				private bool canDoubleJump = false;
				private bool canJumpOnWall = false;
				private Coroutine waitBeforeJumpingOnWall;
				public float waitTimeBeforeJumpingOnWall;
				public float rayLength;
				public float normalJumpForce;
				public float doubleJumpForce;
				private float jumpForce;
				public LayerMask jumpBaseMask;

				//Sticking to Wall
				public bool StuckToWall {
								get; private set;
				}
				private ContactPoint2D pointOfContact;
				private bool canSlide;
				private bool fastSlide;
				private Coroutine waitBeforeSliding;
				public float forceAgainstWall;
				public float slideDownForce;
				public float fastSlideDownForce;
				public float waitTimeBeforeSliding;

				private void Awake()
				{
								Initialize();
								SetControls();
				}

				private void Initialize()
				{
								playerControls = new PlayerControls();
								playerBody = gameObject.GetComponent<Rigidbody2D>();
								playerCollider = gameObject.GetComponent<Collider2D>();
				}

				private void SetControls()
				{
								playerControls.Movement.Jump.performed += context => TryJump();

								playerControls.Movement.Move.performed += context => Movement = context.ReadValue<float>();
								playerControls.Movement.Move.canceled += context => Movement = 0;

								playerControls.Movement.Slide.performed += context => fastSlide = true;
								playerControls.Movement.Slide.canceled += context => fastSlide = false;
				}

				private Vector2 JumpDirection()
				{
								return !StuckToWall ?
												new Vector2(Movement / 4, 1 - Mathf.Abs(Movement / 4)) :
												(Movement == 0 || Mathf.Sign(Movement) != Mathf.Sign(pointOfContact.normal.x)) ?
																new Vector2(0.3f * Mathf.Sign(pointOfContact.normal.x), 0.8f) :
																new Vector2(0.6f * Mathf.Sign(pointOfContact.normal.x), 0.7f);
				}

				private void TryJump()
				{
								if (CanJump())
												Jump();
				}

				private bool CanJump()
				{
								if (!isGrounded) {
												if (!canDoubleJump) {
																if(!canJumpOnWall) {
																				return false;
																}
												} else {
																canDoubleJump = false;
																jumpForce = doubleJumpForce;
																return true;
												}
								}
								canDoubleJump = true;
								jumpForce = normalJumpForce;
								return true;
				}

				private void Jump()
				{
								Vector2 jumpDirection = JumpDirection();

								ReleaseFromWall();

								playerBody.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
								playerBody.AddTorque(torque * -Movement);

								canMoveInAir = false;
								if (waitBeforeMovingInAir != null)
												StopCoroutine(waitBeforeMovingInAir);
								waitBeforeMovingInAir = StartCoroutine(WaitBeforeMovingInAir());
				}

				private void TryMove()
				{
								if (CanMove())
												Move();
				}

				private bool CanMove()
				{
								return !StuckToWall && (isGrounded || canMoveInAir);
				}

				private void Move()
				{
								playerBody.AddForce(Vector2.right * Movement * moveForce, ForceMode2D.Force);
								playerBody.AddTorque(-Movement * torque, ForceMode2D.Force);
								//Angular velocity can be positive or negative depending on direction, so we limit both
								if(playerBody.bodyType != RigidbodyType2D.Static)
												playerBody.angularVelocity = Math.Max(Mathf.Min(playerBody.angularVelocity, maxAngularVelocity), -maxAngularVelocity);
				}

				private bool IsGrounded()
				{
								//TODO Check if it is better than we put this on a OnCollisionStay2D()
								return Physics2D.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y + rayLength, jumpBaseMask);
				}

				private void OnEnable()
				{
								playerControls.Movement.Enable();
				}

				private void OnDisable()
				{
								playerControls.Movement.Disable();
				}

				private void ReleaseFromWall()
				{
								if (!StuckToWall)
												return;

								playerBody.velocity = Vector2.zero;
								StuckToWall = false;
								playerBody.gravityScale = 10;
								canSlide = false;
								canJumpOnWall = false;
				}

				private void StickToWall()
				{
								StuckToWall = true;
								canDoubleJump = false;
								playerBody.gravityScale = 0;
								playerBody.velocity = new Vector2(0, 0);

								if (waitBeforeSliding != null)
												StopCoroutine(waitBeforeSliding);
								waitBeforeSliding = StartCoroutine(WaitBeforeSliding());

								if (waitBeforeJumpingOnWall != null)
												StopCoroutine(waitBeforeJumpingOnWall);
								waitBeforeJumpingOnWall = StartCoroutine(WaitBeforeJumpingOnWall());
				}

				private void OnCollisionEnter2D(Collision2D collision)
				{
								if (collision.collider.CompareTag("Wall")) {
												if (!isGrounded && Mathf.Abs(collision.contacts[0].normal.y) < 0.2f) {
																pointOfContact = collision.contacts[0];
																StickToWall();
												} else {
																StuckToWall = false;
												}
								}
				}

				private void OnCollisionExit2D(Collision2D collision)
				{
								if (collision.collider.CompareTag("Wall")) {
												ReleaseFromWall();
								}
				}

				private void TryPushToWall()
				{
								if (CanPushToWall())
												PushToWall();
				}

				private bool CanPushToWall()
				{
								int wallSide = -(int)Mathf.Sign(pointOfContact.normal.x);

								//If velocity == 0, then Sign() return 1 but we want that to mean going down so 1 -> down and -1 -> up
								int movementDirection = -(int)Mathf.Sign(playerBody.velocity.y);

								return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (playerCollider.bounds.extents.y * movementDirection)),
												Vector3.right * wallSide,
												playerCollider.bounds.extents.x + rayLength,
												jumpBaseMask);
				}

				private void PushToWall()
				{
								playerBody.AddForce(-pointOfContact.normal * forceAgainstWall, ForceMode2D.Impulse);
				}

				private void SlideOnWall()
				{
								if (fastSlide)
												playerBody.AddForce(Vector2.down * fastSlideDownForce, ForceMode2D.Force);
								else if (canSlide)
												playerBody.AddForce(Vector2.down * slideDownForce, ForceMode2D.Force);
				}

				private IEnumerator WaitBeforeSliding()
				{
								yield return new WaitForSeconds(waitTimeBeforeSliding);
								canSlide = true;
				}

				private IEnumerator WaitBeforeJumpingOnWall()
				{
								yield return new WaitForSeconds(waitTimeBeforeJumpingOnWall);
								canJumpOnWall = true;
				}

				private IEnumerator WaitBeforeMovingInAir()
				{
								yield return new WaitForSeconds(waitTimeBeforeMovingInAir);
								canMoveInAir = true;
				}

				private void FixedUpdate()
				{
								TryMove();

								if (isGrounded = IsGrounded()) {
												ReleaseFromWall();
								}
								if (StuckToWall) {
												TryPushToWall();
												SlideOnWall();
								}
				}
}