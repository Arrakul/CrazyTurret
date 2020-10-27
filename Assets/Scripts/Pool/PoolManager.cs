using UnityEngine;

public static class PoolManager
{
	private static PoolPart[] _pools;
	private static GameObject _objectsParent;

	[System.Serializable]
	public struct PoolPart 
	{
		public string name;
		public PoolObject prefab;
		public int count;
		
		[HideInInspector]
		public ObjectPooling ferula;
	}

	public static void Initialize(PoolPart[] newPools) 
	{
		_pools = newPools;
		_objectsParent = new GameObject {name = "Pool"};

		for (int i=0; i<_pools.Length; i++) 
		{
			if(_pools[i].prefab!=null)
			{
				var poolObjectsParent = new GameObject {name = _pools[i].name};
				poolObjectsParent.transform.SetParent(_objectsParent.transform);
				_pools[i].ferula = poolObjectsParent.AddComponent<ObjectPooling>();
				_pools[i].ferula.Initialize(_pools[i].count, _pools[i].prefab, poolObjectsParent.transform);
			}
		}
	}


	public static GameObject GetObject (string name, Vector3 position, Quaternion rotation) 
	{
		GameObject result = null;
		if (_pools != null) 
		{
			for (int i = 0; i < _pools.Length; i++) 
			{
				if (_pools[i].name == name) 
				{
					result = _pools[i].ferula.GetObject().gameObject;
					result.transform.position = position;
					result.transform.rotation = rotation;
					result.SetActive (true);
					return result;
				}
			}
		} 
		return result;
	}
	
	public static GameObject GetActiveObject (string name) 
	{
		GameObject result = null;
		if (_pools != null) 
		{
			for (int i = 0; i < _pools.Length; i++) {
				if (_pools[i].name == name) 
				{
					result = _pools[i].ferula.GetActiveObject()?.gameObject;
					return result;
				}
			}
		} 
		return result;
	}

}