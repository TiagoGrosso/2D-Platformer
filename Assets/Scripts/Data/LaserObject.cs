using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : BaseObject {

				public LaserBeam.Data beamData;

				public override GameObject Deserialize(GameObject prefab)
				{
								GameObject newObj = base.Deserialize(prefab);
								newObj.GetComponentInChildren<LaserBeam>().data = beamData;
								return newObj;
				}
				public LaserObject(GameObject gameObject) : base(gameObject)
				{
								beamData = gameObject.GetComponentInChildren<LaserBeam>().data;
				}

				[JsonConstructor]
				public LaserObject()
				{

				}

				public new static Func<GameObject, LaserObject> CONSTRUCTOR = gameObject => new LaserObject(gameObject);

}
