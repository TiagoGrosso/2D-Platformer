using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFrame : MonoBehaviour
{
				public PortalFrame otherFrame;
				public bool canTeleport = true;
				public float teleportCooldown;
				public Gradient portalColorGradient;
				public SpriteRenderer sprite;
				private float cooldownCount = 0;

				private void Teleport(Collider2D collider)
				{
								otherFrame.canTeleport = false;
								canTeleport = false;
								collider.GetComponent<Teleport>().BeginTeleport(otherFrame.transform.position);
				}

				private void CountCooldown()
				{
								cooldownCount += Time.deltaTime;
								sprite.color = portalColorGradient.Evaluate(cooldownCount / teleportCooldown);
								if (cooldownCount >= teleportCooldown) {
												otherFrame.ResetPortal();
												ResetPortal();
								}
				}

				private void OnTriggerStay2D(Collider2D collider)
				{
								if (canTeleport && collider.CompareTag("Player"))
												Teleport(collider);
				}

				public void ResetPortal()
				{
								canTeleport = true;
								cooldownCount = 0;
				}

				private void Update()
				{
								if (!canTeleport)
												CountCooldown();
				}

}
