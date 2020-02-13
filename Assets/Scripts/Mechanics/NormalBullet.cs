using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet {

				public float bulletForce;

				public override void Fire()
				{
								gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
				}
}
