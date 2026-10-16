using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x02000005 RID: 5
	[CreateAssetMenu(menuName = "Laundry Bear \ud83e\udd9d/Core/Create Services Asset")]
	public class ServicesList : ScriptableObject
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000026A5 File Offset: 0x000008A5
		public GameObject[] GetServicesArray()
		{
			return this.m_services;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026AD File Offset: 0x000008AD
		public IEnumerable<GameObject> GetRuntimeCreatedServices()
		{
			return this.m_runtimeCreatedObjects;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026B8 File Offset: 0x000008B8
		public void AddService(GameObject objectInstance)
		{
			GameObject[] array = new GameObject[this.m_services.Length + 1];
			this.m_services.CopyTo(array, 0);
			array[this.m_services.Length] = objectInstance;
			this.m_services = array;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026F4 File Offset: 0x000008F4
		public T AddService<T>() where T : Component, IService
		{
			GameObject gameObject = new GameObject(typeof(T).Name);
			T t = gameObject.AddComponent<T>();
			this.m_runtimeCreatedObjects.Add(gameObject);
			return t;
		}

		// Token: 0x04000007 RID: 7
		[SerializeField]
		private GameObject[] m_services = new GameObject[0];

		// Token: 0x04000008 RID: 8
		private List<GameObject> m_runtimeCreatedObjects = new List<GameObject>();
	}
}
