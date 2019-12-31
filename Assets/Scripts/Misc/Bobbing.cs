using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
				public float max;
				public float speed;
				private float current = 0;
				private int direction = 1;

				void Update()
    {
								current += (direction * speed * Time.deltaTime);

								transform.Translate(0, direction * speed * Time.deltaTime, 0, Space.World);

								if (Mathf.Abs(current) > max)
												direction *= -1;
    }
}
