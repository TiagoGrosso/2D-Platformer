using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
				public Data data;

				private AreaEffector2D effector;

				private void Start()
				{
								effector = GetComponent<AreaEffector2D>();
								effector.forceMagnitude = data.forceMagnitude;
				}

				private void Update()
				{
								effector.forceAngle = transform.rotation.z;
				}

				[Serializable]
				public struct Data {
								public float forceMagnitude;
				}
}
