using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
				public float rotationSpeed;
				public bool rotateCounterclock;

    void Start()
    {
    }

    void Update()
    {
								int rotateDirection = rotateCounterclock ? -1 : 1;

								transform.Rotate(0, 0, rotateDirection * rotationSpeed * Time.deltaTime);
    }
}
