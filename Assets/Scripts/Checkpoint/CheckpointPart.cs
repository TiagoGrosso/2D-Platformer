using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointPart : MonoBehaviour
{
				public Checkpoint checkpoint;
				public Color claimedColor;

				private void OnTriggerEnter2D(Collider2D collider)
				{
								if (collider.CompareTag("Player"))
												checkpoint.Claim();
				}

				private void CheckpointClaimed()
				{
								gameObject.GetComponent<SpriteRenderer>().color = claimedColor;
				}
}
