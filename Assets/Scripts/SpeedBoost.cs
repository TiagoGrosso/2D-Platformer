using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
				public AreaEffector2D effector;

				private void Update()
				{
								effector.forceAngle = transform.rotation.z;
				}
}
