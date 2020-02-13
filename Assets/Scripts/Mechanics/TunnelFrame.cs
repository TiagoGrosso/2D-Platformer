using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelFrame : MonoBehaviour
{
				private void OnTriggerEnter2D(Collider2D collider)
				{
								if (collider.CompareTag("Player"))
												Physics2D.IgnoreLayerCollision(8, 9, true);
				}

				private void OnTriggerExit2D(Collider2D collider)
				{
								if (collider.CompareTag("Player"))
												Physics2D.IgnoreLayerCollision(8, 9, false);
				}
}
