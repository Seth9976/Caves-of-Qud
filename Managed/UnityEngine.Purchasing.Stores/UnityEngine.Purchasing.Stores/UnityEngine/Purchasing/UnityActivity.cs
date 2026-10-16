using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000011 RID: 17
	internal class UnityActivity
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000024B8 File Offset: 0x000006B8
		private static AndroidJavaClass GetUnityPlayerClass()
		{
			if (UnityActivity.s_UnityPlayerClass == null)
			{
				UnityActivity.s_UnityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			}
			return UnityActivity.s_UnityPlayerClass;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static AndroidJavaObject GetCurrentActivity()
		{
			return UnityActivity.GetUnityPlayerClass().GetStatic<AndroidJavaObject>("currentActivity");
		}

		// Token: 0x04000010 RID: 16
		private const string k_AndroidClassName = "com.unity3d.player.UnityPlayer";

		// Token: 0x04000011 RID: 17
		private static AndroidJavaClass s_UnityPlayerClass;
	}
}
