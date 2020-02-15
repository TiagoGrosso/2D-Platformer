using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformObject : BaseObject
{
				public float speed;
				public Vector2[] targets;
				public Dictionary<string, int> testMap = new Dictionary<string, int>();

				public PlatformObject(GameObject platform) : base (platform)
				{
								testMap.Add("hello", 1);
								testMap.Add("yello", 2);

								try {
												ObjectMovement objectMovement = platform.GetComponent<ObjectMovement>();
												speed = objectMovement.speed;
												targets = objectMovement.targets;
								} catch {
												//Do nothing, just skip
								}
				}

				public override GameObject Deserialize(GameObject prefab)
				{
								GameObject platform = base.Deserialize(prefab);
								ObjectMovement objectMovement = platform.GetComponent<ObjectMovement>();

								if (targets.Length != 0) {
												objectMovement.speed = speed;
												objectMovement.targets = targets;
								} else
												GameObject.Destroy(objectMovement);

								return platform;
				}
}
