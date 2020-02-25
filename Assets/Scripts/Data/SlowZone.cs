using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public Data data;

    void Start()
    {
        AreaEffector2D areaEffector2D = GetComponent<AreaEffector2D>();
        areaEffector2D.drag = data.drag;
        areaEffector2D.angularDrag = data.angularDrag;
    }

    [Serializable]
    public struct Data {
        public float drag;
        public float angularDrag;
    }
}
