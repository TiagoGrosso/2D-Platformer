using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet {

				public override void Fire()
				{
								gameObject.GetComponent<Follow>().target = GameObject.FindGameObjectWithTag("Player").transform;
				}

}
