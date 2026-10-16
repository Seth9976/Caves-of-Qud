using System;
using UnityEngine;

namespace PlayFab.Internal
{
	// Token: 0x020001AC RID: 428
	public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000155D6 File Offset: 0x000137D6
		public static T instance
		{
			get
			{
				SingletonMonoBehaviour<T>.CreateInstance();
				return SingletonMonoBehaviour<T>._instance;
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000155E4 File Offset: 0x000137E4
		public static void CreateInstance()
		{
			if (SingletonMonoBehaviour<T>._instance == null)
			{
				SingletonMonoBehaviour<T>._instance = global::UnityEngine.Object.FindFirstObjectByType<T>();
				if (SingletonMonoBehaviour<T>._instance == null)
				{
					SingletonMonoBehaviour<T>._instance = new GameObject(typeof(T).Name).AddComponent<T>();
				}
				if (!SingletonMonoBehaviour<T>._instance.initialized)
				{
					SingletonMonoBehaviour<T>._instance.Initialize();
					SingletonMonoBehaviour<T>._instance.initialized = true;
				}
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001566D File Offset: 0x0001386D
		public virtual void Awake()
		{
			if (Application.isPlaying)
			{
				global::UnityEngine.Object.DontDestroyOnLoad(this);
			}
			if (SingletonMonoBehaviour<T>._instance != null)
			{
				global::UnityEngine.Object.DestroyImmediate(base.gameObject);
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00015699 File Offset: 0x00013899
		protected virtual void Initialize()
		{
		}

		// Token: 0x0400094C RID: 2380
		private static T _instance;

		// Token: 0x0400094D RID: 2381
		protected bool initialized;
	}
}
