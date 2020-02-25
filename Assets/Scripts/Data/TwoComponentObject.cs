using Newtonsoft.Json;
using System;
using UnityEngine;

public class TwoComponentObject<C1, D1, C2, D2> : BaseObject where C1 : MonoBehaviour where D1 : struct where C2 : MonoBehaviour where D2 : struct {

				public ObjectComponent<C1, D1> component1;
				public ObjectComponent<C2, D2> component2;

				public TwoComponentObject(GameObject obj) : base(obj)
				{
								component1 = new ObjectComponent<C1, D1>(obj);
								component2 = new ObjectComponent<C2, D2>(obj);
				}

				public override GameObject Deserialize(GameObject prefab)
				{
								GameObject newObj = base.Deserialize(prefab);
								component1.AttachTo(newObj);
								component2.AttachTo(newObj);
								return newObj;
				}

				[JsonConstructor]
				public TwoComponentObject()
				{

				}

				public new static Func<GameObject, TwoComponentObject<C1, D1, C2, D2>> CONSTRUCTOR = gameObject => new TwoComponentObject<C1, D1, C2, D2>(gameObject);

}


