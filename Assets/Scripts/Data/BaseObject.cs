using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public Dictionary<string, MonoBehaviour[]> components = new Dictionary<string, MonoBehaviour[]>();

    public MonoBehaviour objectMovement;

    protected void PutTransform(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }

    public virtual GameObject Deserialize(GameObject prefab)
    {
        GameObject newObject = GameObject.Instantiate(prefab);
        newObject.name = prefab.name;
        newObject.transform.position = position;
        newObject.transform.rotation = rotation;
        newObject.transform.localScale = scale;

        return newObject;
    }

    public BaseObject(GameObject gameObject)
    {
        PutTransform(gameObject.transform);
    }

    public static implicit operator BaseObject(GameObject gameObject)
    {
        return new BaseObject(gameObject);
    }
}
