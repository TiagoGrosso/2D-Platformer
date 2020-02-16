using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("Replaced by [OneComponentObject)")]
public class PlatformObject : BaseObject
{
				public ObjectMovement.Data? objectMovementData;

				public PlatformObject(GameObject platform) : base (platform)
				{
								try {
												objectMovementData = platform.GetComponent<ObjectMovement>().data;
								} catch {
												//Do nothing, just skip
								}
				}

				[JsonConstructor]
				public PlatformObject()
				{
								
				}

				public override GameObject Deserialize(GameObject prefab)
				{
								GameObject platform = base.Deserialize(prefab);

								if (this.objectMovementData.HasValue) {
												ObjectMovement objectMovement = platform.AddComponent<ObjectMovement>();
												objectMovement.data = objectMovementData.GetValueOrDefault();
								}

								return platform;
				}
}
