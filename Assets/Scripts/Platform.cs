using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
				public PlatformEffector2D effector;
				public Collider2D platformCollider;
				public float waitBeforeNotLettingPass;

				private bool tryingToPass = false;
				private PlayerControls playerControls;
				private Collider2D playerCollider;
				private Coroutine dontLetPassCoroutine;

				private void LetPlayerPassThrough()
				{
								Physics2D.IgnoreCollision(playerCollider, platformCollider, true);

								if (dontLetPassCoroutine != null)
												StopCoroutine(dontLetPassCoroutine);
				}

				private IEnumerator DontLetPlayerPassThrough()
				{
								tryingToPass = false;
								yield return new WaitForSeconds(waitBeforeNotLettingPass);
								Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
				}

				private void Start()
				{
								playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
				}

				private void Awake()
				{
								playerControls = new PlayerControls();

								playerControls.Movement.Slide.performed += context => LetPlayerPassThrough();
								playerControls.Movement.Slide.canceled += context => dontLetPassCoroutine = StartCoroutine(DontLetPlayerPassThrough());
				}

				private void OnEnable()
				{
								playerControls.Movement.Slide.Enable();
				}

				private void OnDisable()
				{
								playerControls.Movement.Slide.Disable();
				}
}
