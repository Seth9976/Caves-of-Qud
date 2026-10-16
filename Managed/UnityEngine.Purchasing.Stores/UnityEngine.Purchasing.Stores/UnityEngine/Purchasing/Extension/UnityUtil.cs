using System;
using System.Collections;
using System.Collections.Generic;
using Uniject;

namespace UnityEngine.Purchasing.Extension
{
	// Token: 0x02000094 RID: 148
	[HideInInspector]
	[AddComponentMenu("")]
	internal class UnityUtil : MonoBehaviour, IUtil
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0000B010 File Offset: 0x00009210
		public T[] GetAnyComponentsOfType<T>() where T : class
		{
			GameObject[] array = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
			List<T> list = new List<T>();
			GameObject[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				foreach (MonoBehaviour monoBehaviour in array2[i].GetComponents<MonoBehaviour>())
				{
					if (monoBehaviour is T)
					{
						list.Add(monoBehaviour as T);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000B088 File Offset: 0x00009288
		public DateTime currentTime
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000B08F File Offset: 0x0000928F
		public string persistentDataPath
		{
			get
			{
				return Application.persistentDataPath;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000B096 File Offset: 0x00009296
		public string deviceUniqueIdentifier
		{
			get
			{
				return SystemInfo.deviceUniqueIdentifier;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000B09D File Offset: 0x0000929D
		public string unityVersion
		{
			get
			{
				return Application.unityVersion;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000B0A4 File Offset: 0x000092A4
		public string cloudProjectId
		{
			get
			{
				return Application.cloudProjectId;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000B0AB File Offset: 0x000092AB
		public string userId
		{
			get
			{
				return PlayerPrefs.GetString("unity.cloud_userid", string.Empty);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000B0BC File Offset: 0x000092BC
		public string gameVersion
		{
			get
			{
				return Application.version;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000B0C3 File Offset: 0x000092C3
		public ulong sessionId
		{
			get
			{
				return ulong.Parse(PlayerPrefs.GetString("unity.player_sessionid", "0"));
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000B0D9 File Offset: 0x000092D9
		public RuntimePlatform platform
		{
			get
			{
				return Application.platform;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000B0E0 File Offset: 0x000092E0
		public bool isEditor
		{
			get
			{
				return Application.isEditor;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000B0E7 File Offset: 0x000092E7
		public string deviceModel
		{
			get
			{
				return SystemInfo.deviceModel;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000B0EE File Offset: 0x000092EE
		public string deviceName
		{
			get
			{
				return SystemInfo.deviceName;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000B0F5 File Offset: 0x000092F5
		public DeviceType deviceType
		{
			get
			{
				return SystemInfo.deviceType;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000B0FC File Offset: 0x000092FC
		public string operatingSystem
		{
			get
			{
				return SystemInfo.operatingSystem;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000B103 File Offset: 0x00009303
		public int screenWidth
		{
			get
			{
				return Screen.width;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000B10A File Offset: 0x0000930A
		public int screenHeight
		{
			get
			{
				return Screen.height;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000B111 File Offset: 0x00009311
		public float screenDpi
		{
			get
			{
				return Screen.dpi;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000B118 File Offset: 0x00009318
		public string screenOrientation
		{
			get
			{
				return Screen.orientation.ToString();
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000B138 File Offset: 0x00009338
		object IUtil.InitiateCoroutine(IEnumerator start)
		{
			return base.StartCoroutine(start);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000B141 File Offset: 0x00009341
		void IUtil.InitiateCoroutine(IEnumerator start, int delay)
		{
			this.DelayedCoroutine(start, delay);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000B14C File Offset: 0x0000934C
		public void RunOnMainThread(Action runnable)
		{
			List<Action> list = UnityUtil.s_Callbacks;
			lock (list)
			{
				UnityUtil.s_Callbacks.Add(runnable);
				UnityUtil.s_CallbacksPending = true;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000B198 File Offset: 0x00009398
		public object GetWaitForSeconds(int seconds)
		{
			return new WaitForSeconds((float)seconds);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000B1A1 File Offset: 0x000093A1
		private void Start()
		{
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000B1AE File Offset: 0x000093AE
		public static T FindInstanceOfType<T>() where T : MonoBehaviour
		{
			return (T)((object)Object.FindObjectOfType(typeof(T)));
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000B1C4 File Offset: 0x000093C4
		public static T LoadResourceInstanceOfType<T>() where T : MonoBehaviour
		{
			return ((GameObject)Object.Instantiate(Resources.Load(typeof(T).ToString()))).GetComponent<T>();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000B1E9 File Offset: 0x000093E9
		public static bool PcPlatform()
		{
			return UnityUtil.s_PcControlledPlatforms.Contains(Application.platform);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000B1FA File Offset: 0x000093FA
		private IEnumerator DelayedCoroutine(IEnumerator coroutine, int delay)
		{
			yield return new WaitForSeconds((float)delay);
			base.StartCoroutine(coroutine);
			yield break;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000B218 File Offset: 0x00009418
		private void Update()
		{
			if (!UnityUtil.s_CallbacksPending)
			{
				return;
			}
			List<Action> list = UnityUtil.s_Callbacks;
			Action[] array;
			lock (list)
			{
				if (UnityUtil.s_Callbacks.Count == 0)
				{
					return;
				}
				array = new Action[UnityUtil.s_Callbacks.Count];
				UnityUtil.s_Callbacks.CopyTo(array);
				UnityUtil.s_Callbacks.Clear();
				UnityUtil.s_CallbacksPending = false;
			}
			Action[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i]();
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000B2B4 File Offset: 0x000094B4
		public void AddPauseListener(Action<bool> runnable)
		{
			this.pauseListeners.Add(runnable);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public void OnApplicationPause(bool paused)
		{
			foreach (Action<bool> action in this.pauseListeners)
			{
				action(paused);
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000B318 File Offset: 0x00009518
		public bool IsClassOrSubclass(Type potentialBase, Type potentialDescendant)
		{
			return potentialDescendant.IsSubclassOf(potentialBase) || potentialDescendant == potentialBase;
		}

		// Token: 0x040001DF RID: 479
		private static readonly List<Action> s_Callbacks = new List<Action>();

		// Token: 0x040001E0 RID: 480
		private static volatile bool s_CallbacksPending;

		// Token: 0x040001E1 RID: 481
		private static readonly List<RuntimePlatform> s_PcControlledPlatforms = new List<RuntimePlatform>
		{
			RuntimePlatform.LinuxPlayer,
			RuntimePlatform.OSXEditor,
			RuntimePlatform.OSXPlayer,
			RuntimePlatform.WindowsEditor,
			RuntimePlatform.WindowsPlayer
		};

		// Token: 0x040001E2 RID: 482
		private readonly List<Action<bool>> pauseListeners = new List<Action<bool>>();
	}
}
