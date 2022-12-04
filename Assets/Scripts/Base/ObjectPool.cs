using System.Collections.Generic;
using UnityEngine;

namespace Phoebe.Neo.Base {
	public class ObjectPool {
		private static ObjectPool instance;

		private Dictionary<string, GameObject> GameObjectDic;

		private Dictionary<string, List<IObject>> GameObjectPool;

		public static ObjectPool Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ObjectPool();
				}
				return instance;
			}
		}

		public ObjectPool()
		{
			Init();
		}

		public void Init()
		{
			GameObjectDic = new Dictionary<string, GameObject>();
			GameObjectPool = new Dictionary<string, List<IObject>>();
		}

		public GameObject AddObject(ResourceType type, string name)
		{
			if (GameObjectDic.ContainsKey(name))
			{
				return GameObjectDic[name];
			}
			GameObject gameObject = ResourceLoader.Instance.Load(type, name) as GameObject;
			if (gameObject == null)
			{
				Debug.LogWarning("ObjectPool:Can't add object with name: '" + name + "'. Please make sure your ResourceType and object name true.");
				return null;
			}
			gameObject.SetActive(value: false);
			GameObjectDic.Add(name, gameObject);
			GameObjectPool.Add(name, new List<IObject>());
			return gameObject;
		}

		public bool AddObject(string name, GameObject obj)
		{
			if (GameObjectDic.ContainsKey(name))
			{
				return false;
			}
			obj.SetActive(value: false);
			GameObjectDic.Add(name, obj);
			GameObjectPool.Add(name, new List<IObject>());
			return true;
		}

		public void RemoveObject(string name)
		{
			if (!GameObjectDic.ContainsKey(name))
			{
				return;
			}
			GameObjectDic.Remove(name);
			foreach (IObject item in GameObjectPool[name])
			{
				try
				{
					item.Delete();
				}
				catch
				{
				}
			}
			GameObjectPool.Remove(name);
		}

		public IObject GetObject(string name, Dictionary<string, object> valueDic)
		{
			if (!GameObjectDic.ContainsKey(name))
			{
				Debug.LogError("ObjectPool:Can't get object with name '" + name + "', plaese check the object name and make sure your gameobject is added.");
				return null;
			}
			List<IObject> list = new List<IObject>();
			foreach (IObject item in GameObjectPool[name])
			{
				try
				{
					if (!item.IsActive)
					{
						item.Create(valueDic);
						return item;
					}
				}
				catch
				{
					list.Add(item);
				}
			}
			Remove(name, list);
			return CreateNew(name, valueDic);
		}

		private IObject CreateNew(string name, Dictionary<string, object> valueDic)
		{
			IObject component = Object.Instantiate(GameObjectDic[name]).GetComponent<IObject>();
			component.Create(valueDic);
			GameObjectPool[name].Add(component);
			return component;
		}

		private void Remove(string name, List<IObject> iobjects)
		{
			foreach (IObject iobject in iobjects)
			{
				GameObjectPool[name].Remove(iobject);
			}
		}
	}
}