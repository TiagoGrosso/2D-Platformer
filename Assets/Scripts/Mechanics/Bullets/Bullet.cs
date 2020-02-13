using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
				public abstract void Fire();

				private void DestroyBullet()
				{
								Destroy(gameObject);
				}

				private void OnCollisionEnter2D(Collision2D collision)
				{
								DestroyBullet();
				}
}
 