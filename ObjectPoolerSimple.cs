/// <summary>
/// Object pooler - used to pooling the object.
/// </summary>
/// <author>Keyur Soneji</author>
///<lastModifiedDate>16 January, 2015</lastModifiedDate>

using UnityEngine;
using System.Collections.Generic;

namespace Keyur.Components.ObjectPooling
{
	public class ObjectPoolerSimple : MonoBehaviour
	{
		#region PublicVariables
		
		public static ObjectPoolerSimple instance;//singleton
		
		public GameObject pooledObject;//gameobject or prefab that is going to pool
		public int pooledAmount; //number of clone at start
		public bool isPoolGrow; //can pool grow at runtime
	     public int maxGrowAmount; //maximum number of clone
	     public List<GameObject> pooledObjectsList; //list of clones
		#endregion 
	
	
		#region PrivateVariables
	     private GameObject tempGameObject;//temporary game object to store instantiated gameobject
		#endregion
		
	
		#region UnityCallbacks
		
		void Awake()
		{
			instance = this; //initialize singleton
		}
		
		void Start()
		{
			pooledObjectsList = new List<GameObject>();
               //instantiate pooledAmount of clones
			for (int i=0; i<pooledAmount; i++)
			{
				tempGameObject = (GameObject)Instantiate(pooledObject);
				tempGameObject.SetActive(false);
				pooledObjectsList.Add(tempGameObject);//assign in list of clones
			}
		}
		#endregion 
	
	
		#region PublicMethods
          /// <summary>
          /// get pooled object from pool
          /// </summary>
          /// <returns></returns>
		public GameObject GetPooledObject()
		{
               //return first non used clone from list
			for (int i=0; i<pooledObjectsList.Count; i++)
			{
				if (!pooledObjectsList [i].activeInHierarchy)
					return pooledObjectsList [i];
			}
				
               //if all clones are in use & pool can be grow,
               //it is instantiated new clone at run time 
			if (isPoolGrow && pooledObjectsList.Count < maxGrowAmount)
			{
				tempGameObject = (GameObject)Instantiate(pooledObject);
				pooledObjectsList.Add(tempGameObject);
				return tempGameObject;
			}
			return null;//return null if not a single clone not in use
		}
		
          /// <summary>
          /// deActive the gameobject
          /// </summary>
		public void DestroyPooledObject(GameObject go)
		{
			go.SetActive(false);
		}
		#endregion 
	}
}