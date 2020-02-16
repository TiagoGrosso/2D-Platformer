
using Newtonsoft.Json;
using System;
using UnityEngine;

public class ObjectComponent<C, D> where C : MonoBehaviour where D : struct {

				public D? data;

				public ObjectComponent(GameObject obj)
				{
								C component = obj.GetComponent<C>();
								if (!component)
												return;

								Type dataType = typeof(C).GetNestedType("Data");

								if (dataType == null || typeof(D) != dataType)
												throw new DifferingDataTypesException();

								data = (D)typeof(C).GetField("data").GetValue(component);
				}

				public GameObject AttachTo(GameObject obj)
				{
								if (this.data.HasValue) {
												C component;
												if (!(component = obj.GetComponent<C>()))
																component = obj.AddComponent<C>();
												typeof(C).GetField("data").SetValue(component, data.GetValueOrDefault());
								}
								return obj;
				}

				[JsonConstructor]
				public ObjectComponent()
				{

				}

				public class DifferingDataTypesException : Exception {

				}
}

