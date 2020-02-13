using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
				public bool freeze;
				public bool kill = true;

				private void OnTriggerEnter2D(Collider2D collider)
				{
								if (kill)
												collider.SendMessage("TryKill", freeze, SendMessageOptions.DontRequireReceiver);
				}

				private void OnCollisionEnter2D(Collision2D collision)
				{
								if (kill)
												collision.collider.SendMessage("TryKill", freeze, SendMessageOptions.DontRequireReceiver);
				}
}
