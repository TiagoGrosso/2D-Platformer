using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalObject : BaseObject
{
				public Vector3 firstPortalPosition;
				public Vector3 secondPortalPosition;

				public override GameObject Deserialize(GameObject prefab)
				{
								GameObject newObj = base.Deserialize(prefab);
								newObj.transform.GetChild(0).transform.localPosition = firstPortalPosition;
								newObj.transform.GetChild(1).transform.localPosition = secondPortalPosition;
								return newObj;
				}

				public PortalObject(GameObject gameObject) : base(gameObject)
				{
								firstPortalPosition = gameObject.transform.GetChild(0).transform.localPosition;
								secondPortalPosition = gameObject.transform.GetChild(1).transform.localPosition;
				}

				[JsonConstructor]
				public PortalObject()
				{

				}

				public new static Func<GameObject, PortalObject> CONSTRUCTOR = gameObject => new PortalObject(gameObject);


}
