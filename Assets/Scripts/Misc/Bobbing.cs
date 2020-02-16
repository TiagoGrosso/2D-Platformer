using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour {

				public Data data;

				private float current = 0;
				private int direction = 1;

				void Update()
				{
								current += (direction * data.speed * Time.deltaTime);

								transform.Translate(0, direction * data.speed * Time.deltaTime, 0, Space.World);

								if (Mathf.Abs(current) > data.max)
												direction *= -1;
				}

				[Serializable]
				public struct Data {
								public float max;
								public float speed;
				}
}
