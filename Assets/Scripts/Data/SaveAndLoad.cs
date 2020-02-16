using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;

//WIP
public class SaveAndLoad : MonoBehaviour {
				public string serializedLevel;
				public GameObject floor;
				public GameObject wall;
				public GameObject crate;
				public GameObject platform;
				public GameObject checkpoint;
				public GameObject bouncePad;

				private void Start()
				{
								Level level = new Level {
												floors = ConvertObjectsToSerializables("Floors", gameObject => new BaseObject(gameObject)),
												walls = ConvertObjectsToSerializables("Walls", gameObject => new BaseObject(gameObject)),
												crates = ConvertObjectsToSerializables("Crates", gameObject => new BaseObject(gameObject)),
												platforms = ConvertObjectsToSerializables("Platforms", gameObject => new OneComponentObject<ObjectMovement, ObjectMovement.Data>(gameObject)),
												checkpoints = ConvertObjectsToSerializables("Checkpoints", gameObject => new BaseObject(gameObject)),
												bouncePads = ConvertObjectsToSerializables("BouncePads", gameObject => new OneComponentObject<BouncePad, BouncePad.Data>(gameObject))
								};

								serializedLevel = JsonConvert.SerializeObject(level, Formatting.Indented);
								print(serializedLevel);
								InstantiateLevel();
				}

				public T[] ConvertObjectsToSerializables<T>(string parentName, Func<GameObject, T> constructor) where T : BaseObject
				{
								Transform parent = GameObject.Find(parentName).transform;

								List<T> objectsToSerialize = new List<T>();

								foreach (Transform child in parent) {
												objectsToSerialize.Add(constructor(child.gameObject));
								}

								return objectsToSerialize.ToArray();
				}

				public void InstantiateLevel()
				{
								Level level = JsonConvert.DeserializeObject<Level>(serializedLevel);

								InstantiateObjects(floor, GameObject.Find("Floors").transform, level.floors);
								InstantiateObjects(wall, GameObject.Find("Walls").transform, level.walls);
								InstantiateObjects(crate, GameObject.Find("Crates").transform, level.crates);
								InstantiateObjects(platform, GameObject.Find("Platforms").transform, level.platforms);
								InstantiateObjects(checkpoint, GameObject.Find("Checkpoints").transform, level.checkpoints);
								InstantiateObjects(bouncePad, GameObject.Find("BouncePads").transform, level.bouncePads);

								//Create Firewall
				}

				public void InstantiateObjects(GameObject prefab, Transform parent , BaseObject[] objectsToDeserialize)
				{
								foreach (BaseObject obj in objectsToDeserialize)
												obj.Deserialize(prefab).transform.parent = parent;
				}

				[Serializable]
				public struct Level {

								public BaseObject[] floors;
								public BaseObject[] walls;
								public BaseObject[] crates;
								public OneComponentObject<ObjectMovement, ObjectMovement.Data>[] platforms;
								public BaseObject[] checkpoints;
								public OneComponentObject<BouncePad, BouncePad.Data>[] bouncePads;
				}
}
