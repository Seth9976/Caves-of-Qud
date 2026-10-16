using System;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x02000012 RID: 18
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002DF3 File Offset: 0x00000FF3
		public static bool HasInstance
		{
			get
			{
				return Singleton<T>.sm_instanceSet;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002DFA File Offset: 0x00000FFA
		public static T Instance
		{
			get
			{
				return Singleton<T>.CurrentInstanceGetter();
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E08 File Offset: 0x00001008
		private static void SetInstance(T component)
		{
			Singleton<T>.sm_instance = component;
			if (null == Singleton<T>.sm_instance)
			{
				Singleton<T>.CurrentInstanceGetter = new Func<T>(Singleton<T>.GetMissingInstance);
				Singleton<T>.sm_instanceSet = false;
				return;
			}
			Singleton<T>.CurrentInstanceGetter = new Func<T>(Singleton<T>.GetLiveInstance);
			Singleton<T>.sm_instanceSet = true;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E5C File Offset: 0x0000105C
		protected virtual void Awake()
		{
			if (Singleton<T>.HasInstance && Singleton<T>.Instance != this)
			{
				Debug.Log("Preventing creation of a second Singleton of type " + typeof(T).ToString() + ". Destroying game object.");
				global::UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			Debug.Log("Singleton of type " + typeof(T).ToString() + " is being created");
			Singleton<T>.SetInstance(base.GetComponent<T>());
			if (this.m_isPersistent)
			{
				global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002EF2 File Offset: 0x000010F2
		protected virtual void OnEnable()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002EF4 File Offset: 0x000010F4
		protected virtual void OnDestroy()
		{
			Singleton<T>.SetInstance(default(T));
			Singleton<T>.sm_instanceSet = false;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002F15 File Offset: 0x00001115
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002F1C File Offset: 0x0000111C
		private static Func<T> CurrentInstanceGetter { get; set; }

		// Token: 0x0600006E RID: 110 RVA: 0x00002F24 File Offset: 0x00001124
		private static T GetLiveInstance()
		{
			return Singleton<T>.sm_instance;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F2C File Offset: 0x0000112C
		private static T GetMissingInstance()
		{
			Singleton<T>.SetInstance(global::UnityEngine.Object.FindObjectOfType<T>());
			if (Singleton<T>.sm_instance != null)
			{
				return Singleton<T>.sm_instance;
			}
			return default(T);
		}

		// Token: 0x04000024 RID: 36
		public static Func<T> SingletonInstanceMissingEvent;

		// Token: 0x04000025 RID: 37
		protected bool m_isPersistent = true;

		// Token: 0x04000026 RID: 38
		private static bool sm_instanceSet;

		// Token: 0x04000027 RID: 39
		protected static T sm_instance;
	}
}
