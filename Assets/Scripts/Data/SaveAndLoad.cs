using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using UnityEngine;

//WIP
public class SaveAndLoad : MonoBehaviour {

				public const string PREFABS_BASE_PATH = "Prefabs/";
				public const string LEVELS_BASE_PATH = "Levels/";

				public string serializedLevel;

				private string levelsPath;

				private JsonSerializerSettings serializationSettings = new JsonSerializerSettings {
								TypeNameHandling = TypeNameHandling.Auto
				};

				private void Start()
				{
								levelsPath = Application.persistentDataPath + "/" + LEVELS_BASE_PATH;
								SerializeLevel("Level1");
								DeserializeLevel("Level1");
				}

				public void SerializeLevel(string levelName)
				{
								string serializedLevel = JsonConvert.SerializeObject(GetLevel(), Formatting.Indented, serializationSettings);
								Directory.CreateDirectory(levelsPath);
								File.WriteAllText(levelsPath + levelName + ".json", serializedLevel);
				}

				public void DeserializeLevel(string levelName)
				{
								string serializedLevel = File.ReadAllText(levelsPath + levelName + ".json");
								Level level = JsonConvert.DeserializeObject<Level>(serializedLevel, serializationSettings);

								InstantiateLevel(level);
				}

				private Level GetLevel()
				{
								Level level = new Level();
								level.objects = new Dictionary<string, BaseObject[]>();

								foreach (ObjectMapping objectMapping in ObjectMapping.MAPPINGS) {
												level.objects.Add(objectMapping.ParentName, ConvertObjectsToSerializables(objectMapping.ParentName, objectMapping.Constructor));
								}

								return level;
				}

				private T[] ConvertObjectsToSerializables<T>(string parentName, Func<GameObject, T> constructor) where T : BaseObject
				{
								Transform parent = GameObject.Find(parentName).transform;

								List<T> objectsToSerialize = new List<T>();

								foreach (Transform child in parent) {
												objectsToSerialize.Add(constructor(child.gameObject));
								}

								return objectsToSerialize.ToArray();
				}

				private void InstantiateLevel(Level level)
				{
								foreach (ObjectMapping objectMapping in ObjectMapping.MAPPINGS) {
												GameObject prefab = Resources.Load<GameObject>(PREFABS_BASE_PATH + objectMapping.Path + objectMapping.Name);
												InstantiateObjects(prefab, GameObject.Find(objectMapping.ParentName).transform, level.objects[objectMapping.ParentName]);
								}

								//Create firewall
				}

				private void InstantiateObjects(GameObject prefab, Transform parent, BaseObject[] objectsToDeserialize)
				{
								if (objectsToDeserialize == null || objectsToDeserialize.Length == 0)
												return;

								foreach (BaseObject obj in objectsToDeserialize)
												obj.Deserialize(prefab).transform.parent = parent;
				}

				public class ObjectMapping {

								public string Path { get; private set; }
								public string Name { get; private set; }
								public string ParentName { get; private set; }
								public Func<GameObject, BaseObject> Constructor { get; private set; }

								private ObjectMapping(string path, string name, string parentName, Func<GameObject, BaseObject> constructor)
								{
												this.Path = path;
												this.Name = name;
												this.ParentName = parentName;
												this.Constructor = constructor;
								}

								public static readonly ObjectMapping[] MAPPINGS = new ObjectMapping[] {
												new ObjectMapping("Objects/", "Floor", "Floors", gameObject => new BaseObject(gameObject)),
												new ObjectMapping("Objects/", "Wall", "Walls", gameObject => new BaseObject(gameObject)),
												new ObjectMapping("Objects/", "Crate", "Crates", gameObject => new BaseObject(gameObject)),
												new ObjectMapping("Objects/", "Checkpoint", "Checkpoints", gameObject => new BaseObject(gameObject)),
												new ObjectMapping("Objects/", "Platform", "Platforms", gameObject => new OneComponentObject<ObjectMovement, ObjectMovement.Data>(gameObject)),
												new ObjectMapping("Mechanics/", "Bounce Pad", "BouncePads", gameObject => new OneComponentObject<ObjectMovement, ObjectMovement.Data>(gameObject))
				};

				}

				[Serializable]
				public struct Level {
								public Dictionary<string, BaseObject[]> objects;
				}
}
