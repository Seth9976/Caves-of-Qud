using System;
using System.Collections;
using UnityEngine;

namespace Uniject
{
	// Token: 0x02000006 RID: 6
	internal interface IUtil
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7
		RuntimePlatform platform { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8
		bool isEditor { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9
		string persistentDataPath { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10
		string cloudProjectId { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11
		string deviceUniqueIdentifier { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12
		string unityVersion { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13
		string userId { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14
		string gameVersion { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15
		ulong sessionId { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16
		DateTime currentTime { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17
		string deviceModel { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18
		string deviceName { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19
		DeviceType deviceType { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20
		string operatingSystem { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000015 RID: 21
		int screenWidth { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000016 RID: 22
		int screenHeight { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000017 RID: 23
		float screenDpi { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000018 RID: 24
		string screenOrientation { get; }

		// Token: 0x06000019 RID: 25
		T[] GetAnyComponentsOfType<T>() where T : class;

		// Token: 0x0600001A RID: 26
		object InitiateCoroutine(IEnumerator start);

		// Token: 0x0600001B RID: 27
		object GetWaitForSeconds(int seconds);

		// Token: 0x0600001C RID: 28
		void InitiateCoroutine(IEnumerator start, int delayInSeconds);

		// Token: 0x0600001D RID: 29
		void RunOnMainThread(Action runnable);

		// Token: 0x0600001E RID: 30
		void AddPauseListener(Action<bool> runnable);

		// Token: 0x0600001F RID: 31
		bool IsClassOrSubclass(Type potentialBase, Type potentialDescendant);
	}
}
