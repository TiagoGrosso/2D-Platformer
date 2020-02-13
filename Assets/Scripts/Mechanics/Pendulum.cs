using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour {

				public Rigidbody2D body;
				public float pushForce;

				void FixedUpdate()
				{
								body.AddForce(Vector2.right * pushForce * Math.Sign(body.angularVelocity));
				}

}
