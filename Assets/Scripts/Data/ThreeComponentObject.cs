using Newtonsoft.Json;
using System;
using UnityEngine;

public class ThreeComponentObject<C1, D1, C2, D2, C3, D3> : BaseObject where C1 : MonoBehaviour where D1 : struct where C2 : MonoBehaviour where D2 : struct where C3 : MonoBehaviour where D3 : struct {

				public ObjectComponent<C1, D1> component1;
				public ObjectComponent<C2, D2> component2;
				public ObjectComponent<C3, D3> component3;

				public ThreeComponentObject(GameObject obj) : base(obj)
				{
								component1 = new ObjectComponent<C1, D1>(obj);
								component2 = new ObjectComponent<C2, D2>(obj);
								component3 = new ObjectComponent<C3, D3>(obj);
				}

				public override GameObject Deserialize(GameObject prefab)
				{
								GameObject newObj = base.Deserialize(prefab);
								component1.AttachTo(newObj);
								component2.AttachTo(newObj);
								component3.AttachTo(newObj);
								return newObj;
				}

				[JsonConstructor]
				public ThreeComponentObject()
				{

				}

				public class DifferingDataTypesException : Exception {

				}
}
