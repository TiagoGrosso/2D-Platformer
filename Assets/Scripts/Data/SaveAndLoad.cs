using Pathfinding.Ionic.Zip;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UDP.Common.MiniJSON;

public class SaveAndLoad : MonoBehaviour {
				public string serializedLevel;
				public GameObject floor;
				public GameObject wall;
				public GameObject crate;
				public GameObject platform;

				private void Start()
				{
								Level level = new Level {
												floors = ConvertObjectsToSerializables("Floors", gameObject => new BaseObject(gameObject)),
												walls = ConvertObjectsToSerializables("Walls", gameObject => new BaseObject(gameObject)),
												crates = ConvertObjectsToSerializables("Crates", gameObject => new BaseObject(gameObject)),
												platforms = ConvertObjectsToSerializables("Platforms", gameObject => new PlatformObject(gameObject)),
								};

								serializedLevel = JsonUtility.ToJson(level, true);

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
								Level level = JsonUtility.FromJson<Level>(serializedLevel);

								InstantiateObjects(floor, GameObject.Find("Floors").transform, level.floors);
								InstantiateObjects(wall, GameObject.Find("Walls").transform, level.walls);
								InstantiateObjects(crate, GameObject.Find("Crates").transform, level.crates);
								InstantiateObjects(platform, GameObject.Find("Platforms").transform, level.platforms);
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
								public PlatformObject[] platforms;
				}

				public static class JsonHelper {
								public static T[] FromJson<T>(string json)
								{
												Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
												return wrapper.Items;
								}

								public static string ToJson<T>(T[] array, bool prettyPrint = false)
								{
												Wrapper<T> wrapper = new Wrapper<T>();
												wrapper.Items = array;
												return JsonUtility.ToJson(wrapper, prettyPrint);
								}

								[Serializable]
								private class Wrapper<T> {
												public T[] Items;
								}
				}
}
