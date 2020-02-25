using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UDP.Common.MiniJSON;

//WIP
public class SaveAndLoad : MonoBehaviour {

				public const string PREFABS_BASE_PATH = "Prefabs";
				public const string LEVELS_BASE_PATH = "Levels";

				private string levelsPath;

				private JsonSerializerSettings serializationSettings = new JsonSerializerSettings {
								TypeNameHandling = TypeNameHandling.Auto
				};

				private void Start()
				{
								levelsPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + LEVELS_BASE_PATH + Path.AltDirectorySeparatorChar;								
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
								GameObject parentObject = GameObject.Find(parentName);

								if (!parentObject) {
												Debug.LogWarning($"Parent object '{parentName}' not found");
												return new T[] { }; 
								}

								List<T> objectsToSerialize = new List<T>();

								foreach (Transform child in parentObject.transform) {
												objectsToSerialize.Add(constructor(child.gameObject));
								}

								return objectsToSerialize.ToArray();
				}

				private void InstantiateLevel(Level level)
				{
								foreach (ObjectMapping objectMapping in ObjectMapping.MAPPINGS) {
												GameObject prefab = Resources.Load<GameObject>(PREFABS_BASE_PATH + Path.AltDirectorySeparatorChar + objectMapping.Path + Path.AltDirectorySeparatorChar + objectMapping.Name);

												if (!prefab) {
																Debug.LogError($"Could not find prefab {objectMapping.Name} in {objectMapping.Path}.");
																continue;
												}

												GameObject parentObject = GameObject.Find(objectMapping.ParentName);

												if (!parentObject) {
																parentObject = Instantiate(new GameObject(), GameObject.Find("World").transform);
																parentObject.name = objectMapping.ParentName;
												}

												InstantiateObjects(prefab, parentObject.transform, level.objects[objectMapping.ParentName]);
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
												new ObjectMapping("Objects", "Floor", "Floors", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Objects", "Wall", "Walls", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Objects", "Crate", "Crates", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Objects", "Checkpoint", "Checkpoints", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Objects", "Platform", "Platforms", OneComponentObject<ObjectMovement, ObjectMovement.Data>.CONSTRUCTOR),
												new ObjectMapping("Objects", "Pendulum", "Pendulums", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Mechanics", "Bounce Pad", "BouncePads", OneComponentObject<BouncePad, BouncePad.Data>.CONSTRUCTOR),
												new ObjectMapping("Mechanics", "Dark", "Darks", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Mechanics", "End", "Ends", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Mechanics", "Speed Boost", "SpeedBoosts", OneComponentObject<SpeedBoost, SpeedBoost.Data>.CONSTRUCTOR),
												new ObjectMapping("Mechanics", "Tunnel", "Tunnels", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Mechanics", "Slow Zone", "SlowZones", OneComponentObject<SlowZone, SlowZone.Data>.CONSTRUCTOR),
												new ObjectMapping("Mechanics", "Portal", "Portals", PortalObject.CONSTRUCTOR),
												new ObjectMapping("Enemies", "Enemy", "Basic Enemies", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Enemies", "Firewall", "Firewall", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Enemies", "Hazard", "BasicHazards", BaseObject.CONSTRUCTOR),
												new ObjectMapping("Enemies", "Laser", "Lasers", LaserObject.CONSTRUCTOR),
												new ObjectMapping("Enemies", "Turret", "Turrets", OneComponentObject<Turret, Turret.Data>.CONSTRUCTOR),
												new ObjectMapping("Enemies", "Puffer Fish", "PufferFishes", TwoComponentObject<Enemy, Enemy.Data, PufferFish, PufferFish.Data>.CONSTRUCTOR),
												new ObjectMapping("Enemies", "Pendulum Hazard", "PendulumHazards", BaseObject.CONSTRUCTOR)
				};

				}

				[Serializable]
				public struct Level {
								public Dictionary<string, BaseObject[]> objects;
				}
}
