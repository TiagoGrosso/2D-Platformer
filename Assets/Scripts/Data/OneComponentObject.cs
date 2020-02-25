using Newtonsoft.Json;
using System;
using UnityEngine;

public class OneComponentObject<C, D> : BaseObject where C : MonoBehaviour where D : struct{

				public ObjectComponent<C, D> component;

				public OneComponentObject(GameObject obj) : base(obj)
				{
								component = new ObjectComponent<C, D>(obj);
				}

				public override GameObject Deserialize(GameObject prefab)
				{
								GameObject newObj = base.Deserialize(prefab);
								component.AttachTo(newObj);
								return newObj;
				}

				[JsonConstructor]
				public OneComponentObject()
				{

				}

				public new static Func<GameObject, OneComponentObject<C, D>> CONSTRUCTOR = gameObject => new OneComponentObject<C, D>(gameObject);

}

