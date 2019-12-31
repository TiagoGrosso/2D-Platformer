using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
				public Player player;
				public float shrunkSize;
				public float teleportTime;

				private Vector3 shrunkScale;
				private Vector3 normalScale;
				private float maxSizeUpdatePerFrame;
				private Vector3 destination;
				private bool shrinking = false;
				private bool normalizing = false;

				public void BeginTeleport(Vector3 destination)
				{
								player.Freeze(true);
								this.destination = destination;
								shrinking = true;
				}

				private void FinishTeleport()
				{
								shrinking = false;
								transform.position = destination;
								normalizing = true;
								player.Unfreeze();
				}

				private void Start()
				{
								normalScale = transform.localScale;
								shrunkScale = new Vector3(shrunkSize, shrunkSize);
								maxSizeUpdatePerFrame = Vector3.Distance(shrunkScale, normalScale) / teleportTime;
				}

				private void Normalize()
				{
								transform.localScale = Vector3.MoveTowards(transform.localScale, normalScale, maxSizeUpdatePerFrame * Time.deltaTime);
								if (Vector2.Distance(transform.localScale, normalScale) < 0.1f) {
												normalizing = false;
								}
				}

				private void Shrink()
				{
								transform.localScale = Vector3.MoveTowards(transform.localScale, shrunkScale, maxSizeUpdatePerFrame * Time.deltaTime);
								if (Vector2.Distance(transform.localScale, shrunkScale) < 0.1f) {
												FinishTeleport();
								}
				}

				private void Update()
				{
								if (shrinking)
												Shrink();
								else if (normalizing)
												Normalize();
				}
}
