using System;
using System.Collections;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace LaundryBear.PlatformServices.None
{
	// Token: 0x02000005 RID: 5
	public class Platform : MonoBehaviour, IPlatform, IService, IStorageService
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E7 File Offset: 0x000002E7
		public string Name
		{
			get
			{
				return "None Platform";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020EE File Offset: 0x000002EE
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020F6 File Offset: 0x000002F6
		public ServiceLocator.ServiceInitializationStatus InitializationStatus { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020FF File Offset: 0x000002FF
		public bool SupportsAchievements
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002102 File Offset: 0x00000302
		public bool SupportsRichPresence
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002105 File Offset: 0x00000305
		public bool SupportsUsers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002108 File Offset: 0x00000308
		public bool AllowsUserWindowModification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000210B File Offset: 0x0000030B
		public bool SupportsQuit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000010 RID: 16 RVA: 0x00002110 File Offset: 0x00000310
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x00002148 File Offset: 0x00000348
		public event PlatformSuspendEventHandler SuspendEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000012 RID: 18 RVA: 0x00002180 File Offset: 0x00000380
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x000021B8 File Offset: 0x000003B8
		public event PlatformResumeEventHandler ResumeEvent;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000014 RID: 20 RVA: 0x000021F0 File Offset: 0x000003F0
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x00002228 File Offset: 0x00000428
		public event PlatformShutdownEventHandler ShutdownEvent;

		// Token: 0x06000016 RID: 22 RVA: 0x0000225D File Offset: 0x0000045D
		public void SetupRequiredData(object data)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000225F File Offset: 0x0000045F
		public void Quit()
		{
			PlatformShutdownEventHandler shutdownEvent = this.ShutdownEvent;
			if (shutdownEvent != null)
			{
				shutdownEvent();
			}
			Application.Quit();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002277 File Offset: 0x00000477
		public string GetSystemLanguage()
		{
			return CultureInfo.InstalledUICulture.TwoLetterISOLanguageName;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002283 File Offset: 0x00000483
		public IEnumerator Initialize(bool sync = false)
		{
			yield return null;
			this.InitializationStatus = ServiceLocator.ServiceInitializationStatus.Ready;
			yield break;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002292 File Offset: 0x00000492
		private void OnApplicationQuit()
		{
			PlatformShutdownEventHandler shutdownEvent = this.ShutdownEvent;
			if (shutdownEvent == null)
			{
				return;
			}
			shutdownEvent();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022A4 File Offset: 0x000004A4
		private void OnApplicationFocus(bool focus)
		{
			PlatformResumeEventHandler resumeEvent = this.ResumeEvent;
			if (resumeEvent == null)
			{
				return;
			}
			resumeEvent();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022B6 File Offset: 0x000004B6
		private void OnApplicationPause(bool pause)
		{
			PlatformSuspendEventHandler suspendEvent = this.SuspendEvent;
			if (suspendEvent == null)
			{
				return;
			}
			suspendEvent();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022C8 File Offset: 0x000004C8
		public bool SupportsPlayerPrefs
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022CB File Offset: 0x000004CB
		public bool RequiresAssociatedUser
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022CE File Offset: 0x000004CE
		public string Combine(params string[] paths)
		{
			return Path.Combine(paths);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022D6 File Offset: 0x000004D6
		public void InitializePlayerPrefs()
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022D8 File Offset: 0x000004D8
		public IEnumerator InitializePlayerPrefsAsync()
		{
			return this.InitializePlayerPrefsCoroutine();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022E0 File Offset: 0x000004E0
		public void OpenOrCreate(string root, OnCreateStorage callback)
		{
			if (false)
			{
				Debug.LogWarning("Using Async IO calls in editor not implemented anymore");
			}
			IStorage storage = new Storage(root);
			callback(StorageResult.Success, storage);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002309 File Offset: 0x00000509
		public void OpenOrCreate(IUser user, string name, OnCreateStorage callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002310 File Offset: 0x00000510
		private IEnumerator InitializePlayerPrefsCoroutine()
		{
			yield return null;
			yield break;
		}

		// Token: 0x04000005 RID: 5
		public const string UseAsyncIOCallsInEditorKey = "UseAsyncIOCallsInEditor";
	}
}
