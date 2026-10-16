using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x02000004 RID: 4
	public class ServiceLocator : Singleton<ServiceLocator>, IService
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020BB File Offset: 0x000002BB
		public string Name
		{
			get
			{
				return "Service Locator";
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000007 RID: 7 RVA: 0x000020C4 File Offset: 0x000002C4
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x000020F8 File Offset: 0x000002F8
		public static event ServiceLocator.OnManagersInitialized ManagersInitializedEvent;

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212B File Offset: 0x0000032B
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002133 File Offset: 0x00000333
		public ServiceLocator.ServiceInitializationStatus InitializationStatus { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000213C File Offset: 0x0000033C
		public static ServiceLocator.ServiceInitializationStatus IsInitialized
		{
			get
			{
				return Singleton<ServiceLocator>.Instance.InitializationStatus;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002148 File Offset: 0x00000348
		private ServicesList Services
		{
			get
			{
				if (null == this.m_servicesInstance)
				{
					if (null != this.m_services)
					{
						this.m_servicesInstance = global::UnityEngine.Object.Instantiate<ServicesList>(this.m_services);
					}
					else
					{
						this.m_servicesInstance = ScriptableObject.CreateInstance<ServicesList>();
					}
				}
				return this.m_servicesInstance;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002195 File Offset: 0x00000395
		protected override void Awake()
		{
			base.Awake();
			if (Singleton<ServiceLocator>.HasInstance)
			{
				this != Singleton<ServiceLocator>.Instance;
				return;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021B4 File Offset: 0x000003B4
		public IEnumerator Initialize(bool sync = false)
		{
			List<IService> list = new List<IService>();
			GameObject[] servicesArray = this.Services.GetServicesArray();
			for (int i = 0; i < servicesArray.Length; i++)
			{
				IService[] array = this.InstantiateServicePrefab(servicesArray[i]);
				list.AddRange(array);
			}
			foreach (GameObject gameObject in this.Services.GetRuntimeCreatedServices())
			{
				this.SetupServiceParent(gameObject);
				list.AddRange(gameObject.GetComponentsInChildren<IService>());
			}
			return this.InitializeManagersCoroutine(list, sync);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002254 File Offset: 0x00000454
		public IEnumerator InitializeSync()
		{
			List<IService> list = new List<IService>();
			if (this.Services)
			{
				GameObject[] servicesArray = this.Services.GetServicesArray();
				for (int i = 0; i < servicesArray.Length; i++)
				{
					IService[] array = this.InstantiateServicePrefab(servicesArray[i]);
					list.AddRange(array);
				}
				foreach (GameObject gameObject in this.Services.GetRuntimeCreatedServices())
				{
					this.SetupServiceParent(gameObject);
					list.AddRange(gameObject.GetComponentsInChildren<IService>());
				}
			}
			foreach (GameObject gameObject2 in this.Services.GetRuntimeCreatedServices())
			{
				this.SetupServiceParent(gameObject2);
				list.AddRange(gameObject2.GetComponentsInChildren<IService>());
			}
			return this.InitializeManagersCoroutine(list, true);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002358 File Offset: 0x00000558
		private IEnumerator InitializeManagersCoroutine(List<IService> services, bool forceSync)
		{
			Coroutine[] coroutines = new Coroutine[services.Count];
			IEnumerator[] enumerators = new IEnumerator[services.Count];
			for (int i = 0; i < services.Count; i++)
			{
				enumerators[i] = services[i].Initialize(forceSync);
				if (!forceSync)
				{
					coroutines[i] = base.StartCoroutine(enumerators[i]);
				}
			}
			int num;
			for (int serviceIndex = 0; serviceIndex < services.Count; serviceIndex = num)
			{
				if (forceSync)
				{
					while (enumerators[serviceIndex].MoveNext())
					{
					}
				}
				else
				{
					yield return coroutines[serviceIndex];
				}
				Debug.Log(string.Concat(new string[]
				{
					"Service ",
					services[serviceIndex].Name,
					" completed initialization with status ",
					services[serviceIndex].InitializationStatus.ToString(),
					"."
				}));
				num = serviceIndex + 1;
			}
			for (int serviceIndex = 0; serviceIndex < services.Count; serviceIndex = num)
			{
				if (ServiceLocator.ServiceInitializationStatus.Initializing == services[serviceIndex].InitializationStatus)
				{
					yield return null;
				}
				num = serviceIndex + 1;
			}
			this.InitializationStatus = ServiceLocator.ServiceInitializationStatus.Ready;
			this.m_runtimeServices = services;
			this.OnInitializationComplete();
			ServiceLocator.OnManagersInitialized managersInitializedEvent = ServiceLocator.ManagersInitializedEvent;
			if (managersInitializedEvent != null)
			{
				managersInitializedEvent();
			}
			yield break;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002375 File Offset: 0x00000575
		protected virtual void OnInitializationComplete()
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002378 File Offset: 0x00000578
		private IService[] InstantiateServicePrefab(GameObject prefab)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(prefab);
			this.SetupServiceParent(gameObject);
			return gameObject.GetComponentsInChildren<IService>();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000239C File Offset: 0x0000059C
		private void SetupServiceParent(GameObject serviceObject)
		{
			if (serviceObject.name.EndsWith("(Clone)", StringComparison.InvariantCultureIgnoreCase))
			{
				serviceObject.name = serviceObject.name.Substring(0, serviceObject.name.Length - this.m_prefabCopyNameSuffixLength);
			}
			serviceObject.transform.SetParent(base.transform);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023F4 File Offset: 0x000005F4
		public T AddService<T>(out IEnumerator enumerator) where T : MonoBehaviour, IService
		{
			if (this.InitializationStatus != ServiceLocator.ServiceInitializationStatus.Uninitialized)
			{
				GameObject gameObject = new GameObject(typeof(T).Name);
				gameObject.transform.SetParent(base.transform, false);
				T t = gameObject.AddComponent<T>();
				this.m_runtimeServices.Add(t);
				enumerator = t.Initialize(false);
				return t;
			}
			T t2 = this.Services.AddService<T>();
			enumerator = null;
			return t2;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002464 File Offset: 0x00000664
		public bool AddServicesGameObject(GameObject prefab, out IEnumerator[] enumerators)
		{
			if (this.InitializationStatus != ServiceLocator.ServiceInitializationStatus.Uninitialized)
			{
				GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(prefab);
				gameObject.transform.SetParent(base.transform, false);
				IService[] componentsInChildren = gameObject.GetComponentsInChildren<IService>();
				enumerators = new IEnumerator[componentsInChildren.Length];
				this.m_runtimeServices.AddRange(componentsInChildren);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					enumerators[i] = componentsInChildren[i].Initialize(false);
				}
				return true;
			}
			this.Services.AddService(prefab);
			enumerators = null;
			return false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024DA File Offset: 0x000006DA
		[Obsolete("GetService has been deprecated. Use TryGetService instead which has no allocations.")]
		public static T GetService<T>()
		{
			return Singleton<ServiceLocator>.sm_instance.GetComponentInChildren<T>();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024E6 File Offset: 0x000006E6
		[Obsolete("GetServices has been deprecated. Use TryGetServices instead which has no allocations.")]
		public static T[] GetServices<T>()
		{
			return Singleton<ServiceLocator>.sm_instance.GetComponentsInChildren<T>();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024F4 File Offset: 0x000006F4
		public static T GetServiceOrThrow<T>()
		{
			T t;
			if (!ServiceLocator.TryGetService<T>(out t))
			{
				throw new Exception("Could not find service of type T");
			}
			return t;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002518 File Offset: 0x00000718
		public static bool TryGetService<T>(out T service)
		{
			for (int i = 0; i < Singleton<ServiceLocator>.sm_instance.m_runtimeServices.Count; i++)
			{
				if (Singleton<ServiceLocator>.sm_instance.m_runtimeServices[i] is T)
				{
					service = (T)((object)Singleton<ServiceLocator>.sm_instance.m_runtimeServices[i]);
					return true;
				}
			}
			service = default(T);
			return false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000257C File Offset: 0x0000077C
		public static int TryGetServices<T>(T[] services)
		{
			int num = 0;
			for (int i = 0; i < Singleton<ServiceLocator>.sm_instance.m_runtimeServices.Count; i++)
			{
				if (Singleton<ServiceLocator>.sm_instance.m_runtimeServices[i] is T)
				{
					services[num++] = (T)((object)Singleton<ServiceLocator>.sm_instance.m_runtimeServices[i]);
				}
				if (num == services.Length)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025E4 File Offset: 0x000007E4
		public static void ExecuteOnServices<T>(Action<T> executor)
		{
			foreach (T t in Singleton<ServiceLocator>.Instance.GetComponentsInChildren<T>())
			{
				executor(t);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000261C File Offset: 0x0000081C
		public static IEnumerator[] ReloadService<T>() where T : MonoBehaviour, IService
		{
			Singleton<ServiceLocator>.sm_instance.m_runtimeServices.RemoveAll(delegate(IService service)
			{
				if (service is T)
				{
					global::UnityEngine.Object.Destroy((service as T).gameObject);
					return true;
				}
				return false;
			});
			foreach (GameObject gameObject in Singleton<ServiceLocator>.sm_instance.Services.GetServicesArray())
			{
				T t;
				if (gameObject.TryGetComponent<T>(out t))
				{
					IEnumerator[] array;
					Singleton<ServiceLocator>.Instance.AddServicesGameObject(gameObject, out array);
					return array;
				}
			}
			return null;
		}

		// Token: 0x04000002 RID: 2
		private readonly int m_prefabCopyNameSuffixLength = 7;

		// Token: 0x04000004 RID: 4
		[SerializeField]
		private ServicesList m_services;

		// Token: 0x04000005 RID: 5
		private ServicesList m_servicesInstance;

		// Token: 0x04000006 RID: 6
		private List<IService> m_runtimeServices;

		// Token: 0x02000008 RID: 8
		public enum ServiceInitializationStatus
		{
			// Token: 0x04000011 RID: 17
			Uninitialized,
			// Token: 0x04000012 RID: 18
			Initializing,
			// Token: 0x04000013 RID: 19
			Ready,
			// Token: 0x04000014 RID: 20
			Failed
		}

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x06000024 RID: 36
		public delegate void OnManagersInitialized();
	}
}
