using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Data data;

    void Update()
    {
								int rotateDirection = data.rotateCounterclock ? -1 : 1;

								transform.Rotate(0, 0, rotateDirection * data.rotationSpeed * Time.deltaTime);
    }

    [Serializable]
    public struct Data {
        public float rotationSpeed;
        public bool rotateCounterclock;
    }
}
