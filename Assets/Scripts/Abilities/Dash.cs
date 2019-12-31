
using UnityEngine;

public class Dash : Ability<float> {

				public float dashForce;
				private Rigidbody2D playerBody;


				protected override void Cast()
				{
								playerBody.AddForce(Vector2.right * value * dashForce, ForceMode2D.Impulse);
				}

				protected override bool CanCast()
				{
								return base.CanCast() && value != 0;
				}

				protected override void Start()
				{
								base.Start();
								playerBody = playerObject.GetComponent<Rigidbody2D>();
				}
}

